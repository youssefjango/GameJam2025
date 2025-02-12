using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NPC_Controller : MonoBehaviour
{

    public LoadingScreenManager LoadingScreenScript;
    public GameObject LoadingScreenObject;
    public Node currentNode;
    //public GameObject nodeObject;
    public List<Node> path;
    public Node[] allNodes; // Array of all available nodes
    public Animator animator;
    public GameObject playerObject;
    public PlayerController player;
    [SerializeField] private float patrolSpeed = 3;
    [SerializeField] private float chaseSpeed = 3;
    [SerializeField] private float runSpeed = 3;
    private float StateSpeed;
    public bool touchedByLight;
    [SerializeField] float range = 5.0f;
    AudioManager audioManager;
    private AudioSource audioSource;
    public enum Statemachine
    {
        Patrol,
        Engage,
        Attack,
        Evade
    }

    public Statemachine currentState;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<PlayerController>();
        LoadingScreenObject = GameObject.Find("LoadScreenManager");
        LoadingScreenScript = LoadingScreenObject.GetComponent<LoadingScreenManager>();
        //nodeObject = GameObject.Find("AstarManager");
        //currentNode = nodeObject.GetComponent<Node>();

        PopulateAllNodes();
        currentState = Statemachine.Patrol;


        //path = currentNode.connections;
        StartCoroutine(InitializeNPC());
    }
    private IEnumerator InitializeNPC()
    {
        // Wait until loading is complete
        while (!LoadingScreenScript.gameStart)
        {
            yield return null; // Wait for the next frame
        }

        PopulateAllNodes();
        currentState = Statemachine.Patrol;
    }
    void PopulateAllNodes()
    {
        allNodes = FindObjectsOfType<Node>(); // Find all active nodes in the scene
        Debug.Log($"Found {allNodes.Length} nodes in the scene.");
        FindClosestNode();
    }
    private void Update()
    {

        if (LoadingScreenScript.gameStart)
        {
            FindClosestNode();
            switch (currentState)
            {
                case Statemachine.Patrol:
                    Patrol();
                    break;
                case Statemachine.Engage:
                    Engage();
                    break;
                case Statemachine.Attack:
                    Attack();
                    break;
                case Statemachine.Evade:
                    Evade();
                    break;
            }

            bool playerSeen = Vector2.Distance(transform.position, player.transform.position) < range;
            bool playerClose = Vector2.Distance(transform.position, player.transform.position) < 0.5;
            if (playerSeen == false && currentState != Statemachine.Patrol)
            {
                currentState = Statemachine.Patrol;
            }
            else if (touchedByLight && currentState != Statemachine.Evade)
            {
                currentState = Statemachine.Evade;
            }
            else if (!touchedByLight && playerClose == true && currentState != Statemachine.Attack)
            {
                currentState = Statemachine.Attack;
            }
            else if (!touchedByLight && playerSeen == true && currentState != Statemachine.Engage)
            {
                currentState = Statemachine.Engage;
            }


            CreatePath();
        }
    }

    void Patrol()
    {
        audioSource.Stop();
        animator.SetBool("IsChasing", false);
        StateSpeed = patrolSpeed;
        if (path.Count == 0)
        {
            path = AStarManager.instance.GeneratePath(currentNode, AStarManager.instance.NodesInScene()[Random.Range(0, AStarManager.instance.NodesInScene().Length)]);
        }
    }

    void Engage()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        animator.SetBool("IsChasing", true);
        StateSpeed = chaseSpeed;
        if (path.Count == 0)
        {
            path = AStarManager.instance.GeneratePath(currentNode, AStarManager.instance.FindNearestNode(player.transform.position));
        }
    }
    void Attack()
    {
        animator.SetTrigger("Attack");
        StateSpeed = chaseSpeed;
        if (path.Count == 0)
        {
        path = AStarManager.instance.GeneratePath(currentNode, AStarManager.instance.FindNearestNode(player.transform.position));
        }
    }
    void Evade()
    {
        StateSpeed = runSpeed;
        if (path.Count == 0)
        {
        path = AStarManager.instance.GeneratePath(currentNode, AStarManager.instance.FindFurthestNode(player.transform.position));
        }
    }

    void CreatePath()
    {
        if (path == null)
        {
            path = new List<Node>();
        }
        Debug.Log(path);
        
        Debug.Log(currentNode);
        Debug.Log(path.Count);  
        if (path.Count > 0)
        {
            int x = 0;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(path[x].transform.position.x, path[x].transform.position.y, -2), StateSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, path[x].transform.position) < 0.1f)
            {
                currentNode = path[x];
                path.RemoveAt(x);
            }
        }
    }

    void FindClosestNode()
    {
        if (allNodes == null || allNodes.Length == 0) return; // Safety check

        float closestDistance = Mathf.Infinity; // Start with a very large distance
        Node closestNode = null;

        // Iterate through all nodes to find the closest
        foreach (Node node in allNodes)
        {
            float distance = Vector3.Distance(transform.position, node.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestNode = node;
            }
        }

        // Update the current node to the closest one
        currentNode = closestNode;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerLight"))
        {
            touchedByLight = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerLight"))
        {
            touchedByLight = false;
        }
    }
}