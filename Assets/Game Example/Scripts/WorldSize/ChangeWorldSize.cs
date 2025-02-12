using System;
using UnityEngine;

public class ChangeWorldSize : MonoBehaviour
{
    [SerializeField] private GameObject twinkle;
    [SerializeField] private GameObject drip;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject enemy2;
    [SerializeField] private Transform SkyLight;
    public WalkerGenerator MapGenerator;
    public NodeFieldCreation NodeFieldCreation;
    private Vector2 mapSize;
    public Transform player;
    public PolygonCollider2D polygonCollider;

    void Awake()
    {
        mapSize = new Vector2(128,80);
        //enemy
        enemy.transform.position = new Vector2(mapSize.x / 2, (mapSize.y / 2)+15);
        enemy2.transform.position = new Vector2(mapSize.x / 2, (mapSize.y / 2) - 15);
        Debug.Log(mapSize);
        //SkyLight
        SkyLight.position = new Vector2(mapSize.x/2, mapSize.y/2);

        //NodeField
        //NodeFieldCreation.gridSizeX = mapSize;
        //NodeFieldCreation.gridSizeY = mapSize;

        // Access the current points of the collider
        Vector2[] points = polygonCollider.points;
        // Modify or set new points
        Vector2[] newPoints = new Vector2[]
        {
                new Vector2(1f, mapSize.y),
                new Vector2(mapSize.x, mapSize.y),
                new Vector2(mapSize.x, 1f),
                new Vector2(1f, 1f)
        };
        // Assign the new points to the polygon collider
        polygonCollider.points = newPoints;

        //player
        player.transform.position = new Vector2(mapSize.x / 2, mapSize.y / 2);

        //MapGenerator
        MapGenerator.MapHeight = Convert.ToInt32(mapSize.y);
        MapGenerator.MapWidth = Convert.ToInt32(mapSize.x);
        /*
        //Twinkle Effect
        if (twinkle != null)
        {
            ParticleSystem ps = twinkle.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                var shape = ps.shape;
                shape.position = new Vector3(mapSize/2, mapSize/2, 0);
            }
            else
            {
                Debug.LogWarning("No ParticleSystem component found on the target GameObject.");
            }
        }
        else
        {
            Debug.LogWarning("Target GameObject is not assigned.");
        }
        */
        //Drip Effect
        if (drip != null)
        {
            ParticleSystem ps = drip.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                var shape = ps.shape;
                shape.position = new Vector3(mapSize.x/2, mapSize.y/2+10, 0);
            }
            else
            {
                Debug.LogWarning("No ParticleSystem component found on the target GameObject.");
            }
        }
        else
        {
            Debug.LogWarning("Target GameObject is not assigned.");
        }
        
    }
}
