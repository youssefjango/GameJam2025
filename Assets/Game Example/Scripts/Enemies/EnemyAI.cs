using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float roamChangeDirFloat = 2f;
    public LoadingScreenManager LoadingScreenScript;
    public GameObject LoadingScreenObject;
    private bool once;
    private enum State
    {
        Roaming
    }

    private State state;
    private EnemyPathfinding enemyPathfinding;

    private void Awake()
    {
        once = false;
        LoadingScreenObject = GameObject.Find("LoadScreenManager");
        LoadingScreenScript = LoadingScreenObject.GetComponent<LoadingScreenManager>();
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming;
    }

    private void Update()
    {
        if (LoadingScreenScript.gameStart && !once)
        {
            once = true;
            StartCoroutine(RoamingRoutine());
        }
    }

    private IEnumerator RoamingRoutine()
    {
        while (state == State.Roaming)
        {
            Vector2 roamPosition = GetRoamingPosition();
            enemyPathfinding.MoveTo(roamPosition);
            yield return new WaitForSeconds(roamChangeDirFloat);
        }
    }

    private Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("skyLight"))
        {
            if (!LoadingScreenScript.gameStart)
            {
                Destroy(gameObject);
            }
        }
    }
}
