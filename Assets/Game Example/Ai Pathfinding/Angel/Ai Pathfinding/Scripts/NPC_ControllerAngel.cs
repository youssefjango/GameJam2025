using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_ControllerAngel : MonoBehaviour
{
    public LoadingScreenManager LoadingScreenScript;
    public GameObject LoadingScreenObject;
    public Node currentNode;
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
    [SerializeField] float range = 30.0f;
    private AudioSource audioSource;
    public AudioSource attack;
    public enum Statemachine
    {
        Patrol,
        Engage,
        Attack,
        Freeze
    }

    public Statemachine currentState;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        LoadingScreenObject = GameObject.Find("LoadScreenManager");
        LoadingScreenScript = LoadingScreenObject.GetComponent<LoadingScreenManager>();
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<PlayerController>();
    }
    private void Start()
    {
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
        if (allNodes == null || allNodes.Length == 0)
        {
            allNodes = FindObjectsOfType<Node>(); // Find all active nodes in the scene
        }
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
                case Statemachine.Freeze:
                    Freeze();
                    break;
            }

            bool playerSeen = Vector2.Distance(transform.position, player.transform.position) < range;
            bool playerClose = Vector2.Distance(transform.position, player.transform.position) < 1.5;
            if (playerSeen == false && currentState != Statemachine.Patrol)
            {
                currentState = Statemachine.Patrol;
            }
            else if (touchedByLight && currentState != Statemachine.Freeze)
            {
                currentState = Statemachine.Freeze;
            }
            else if (!touchedByLight && playerClose == true && currentState != Statemachine.Attack)
            {
                currentState = Statemachine.Attack;
            }
            else if (!touchedByLight && playerSeen == true && currentState != Statemachine.Engage)
            {
                currentState = Statemachine.Engage;
            }
        }

            CreatePath();
    }

    void Patrol()
    {
        audioSource.Stop();
        animator.SetBool("IsChasing", false);
        animator.SetBool("IsFrozen", false);
        StateSpeed = patrolSpeed;
        if(path.Count == 0)
        {
            path = AStarManager.instance.GeneratePath(currentNode, AStarManager.instance.NodesInScene()[Random.Range(0, AStarManager.instance.NodesInScene().Length)]);
        }
    }

    void Engage()
    {
        attack.Stop();
        float playerdistance = Vector2.Distance(transform.position, player.transform.position);
        audioSource.volume = 0.3f - (playerdistance / 12);
        audioSource.pitch = 0.5f + (playerdistance / 15);
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        animator.SetBool("IsChasing", true);
        animator.SetBool("IsFrozen", false);
        StateSpeed = chaseSpeed;
        if (path.Count == 0)
        {
            path = AStarManager.instance.GeneratePath(currentNode, AStarManager.instance.FindNearestNode(player.transform.position));
        }
    }
    void Attack()
    {
        if (!attack.isPlaying)
        {
            attack.Play();
        }
        animator.SetTrigger("Attack");
        StateSpeed = chaseSpeed;
        if (path.Count == 0)
        {
            path = AStarManager.instance.GeneratePath(currentNode, AStarManager.instance.FindNearestNode(player.transform.position));
        }
    }
    void Freeze()
    {
        animator.SetBool("IsFrozen", true); 
        animator.SetBool("IsChasing", false);
        StateSpeed = 0;
        
    }

    private int pathIndex = 0; // Track the current index instead of removing elements

    void CreatePath()
    {
        if (path == null || path.Count == 0) return;

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(path[pathIndex].transform.position.x, path[pathIndex].transform.position.y, -2), StateSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, path[pathIndex].transform.position) < 0.1f)
        {
            currentNode = path[pathIndex];
            pathIndex++;

            if (pathIndex >= path.Count)
            {
                path.Clear();
                pathIndex = 0;
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
        if(collision.CompareTag("PlayerLight"))
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

