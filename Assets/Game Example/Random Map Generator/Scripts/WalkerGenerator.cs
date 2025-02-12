using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.UI;


public class WalkerGenerator : MonoBehaviour
{
    public enum Grid
    {
        GRASS,
        WALL,
        FLOOR,
        EMPTY
    }
    public LoadingScreenManager loadingScreenManager;
    public GameObject loadingScreen;
    public GameObject sliderObject;
    public UnityEngine.UI.Slider sliderScript;
    //Variables
    public Grid[,] gridHandler;
    public List<WalkerObject> Walkers;
    public Tilemap floorTileMap;
    public Tilemap grassTileMap;
    public Tilemap wallTileMap;
    public TileBase Floor;
    public TileBase Grass1;
    public TileBase Grass2;
    public TileBase Grass3;
    public TileBase Grass4;
    public TileBase Grass5;
    public GameObject tree1Prefab;
    public GameObject tree2Prefab;
    public GameObject bush1Prefab;
    public GameObject barrel1Prefab;
    public GameObject crate1Prefab;
    public GameObject crate2Prefab;
    public GameObject enemyPrefab;
    public GameObject torchPrefab;
    public GameObject batteryPrefab;
    public GameObject cristalPrefab;
    public GameObject magicCirclePrefab;
    public TileBase Wall;
    public int MapWidth = 30;
    public int MapHeight = 30;

    public int MaximumWalkers = 10;
    public int TileCount = default;
    public float FillPercentage = 0.4f;
    public float WaitTime = 0f;
    private Vector3Int Last_position;
    void Awake()
    {
        loadingScreenManager.RegisterTask();
        if (loadingScreen == null)
        {
            loadingScreen = GameObject.Find("LoadingScreen");
        }
        if (sliderScript == null)
        {
            sliderScript = sliderObject.GetComponent<UnityEngine.UI.Slider>();
        }
        loadingScreen.SetActive(true);
        sliderScript.value = 0;
    }
    void Start()
    {
        loadingScreenManager.RegisterTask();
        InitializeGrid();
    }
    private void Update()
    {
        sliderScript.value = ((float)TileCount / (float)gridHandler.Length) * 165;
    }
    void InitializeGrid()
    {
        GameObject magicCircle = Instantiate(magicCirclePrefab, new Vector2(MapWidth / 2, MapHeight / 2), Quaternion.identity);
        magicCircle.transform.SetParent(transform, false);
        gridHandler = new Grid[MapWidth, MapHeight];

        for (int x = 0; x < gridHandler.GetLength(0); x++)
        {
            for (int y = 0; y < gridHandler.GetLength(1); y++)
            {
                gridHandler[x, y] = Grid.EMPTY;
            }
        }

        Walkers = new List<WalkerObject>();

        Vector3Int TileCenter = new Vector3Int(gridHandler.GetLength(0) / 2, gridHandler.GetLength(1) / 2, 0);

        WalkerObject curWalker = new WalkerObject(new Vector2(TileCenter.x, TileCenter.y), GetDirection(), 0.5f);
        gridHandler[TileCenter.x, TileCenter.y] = Grid.GRASS;
        grassTileMap.SetTile(TileCenter, Grass1);
        Walkers.Add(curWalker);

        TileCount++;

        StartCoroutine(CreateFloors());
    }

    Vector2 GetDirection()
    {
        int choice = Mathf.FloorToInt(UnityEngine.Random.value * 3.99f);

        switch (choice)
        {
            case 0:
                return Vector2.down;
            case 1:
                return Vector2.left;
            case 2:
                return Vector2.up;
            case 3:
                return Vector2.right;
            default:
                return Vector2.zero;
        }
    }

    IEnumerator CreateFloors()
    {

        while ((float)TileCount / (float)gridHandler.Length < FillPercentage)
        {

            bool hasCreatedFloor = false;
            foreach (WalkerObject curWalker in Walkers)
            {
                Vector3Int curPos = new Vector3Int((int)curWalker.Position.x, (int)curWalker.Position.y, 0);

                if (gridHandler[curPos.x, curPos.y] != Grid.GRASS)
                {
                    switch (UnityEngine.Random.Range(1, 15))
                    {
                        case 1:
                            grassTileMap.SetTile(curPos, Grass1);
                            break;
                        case 2:
                            grassTileMap.SetTile(curPos, Grass2);
                            break;
                        case 3:
                            grassTileMap.SetTile(curPos, Grass3);
                            break;
                        case 4:
                            grassTileMap.SetTile(curPos, Grass4);
                            break;
                        default:
                            grassTileMap.SetTile(curPos, Grass5);
                            break;
                    }
                    switch (UnityEngine.Random.Range(1, 40))
                    {
                        case 1:
                            GameObject tree1 = Instantiate(tree1Prefab, curPos, Quaternion.identity);
                            tree1.transform.SetParent(transform, false);

                            break;
                        case 2:
                            GameObject tree2 = Instantiate(tree2Prefab, curPos, Quaternion.identity);
                            tree2.transform.SetParent(transform, false);
                            break;
                        case 3:
                            GameObject bush1 = Instantiate(bush1Prefab, curPos, Quaternion.identity);
                            bush1.transform.SetParent(transform, false);
                            break;
                        case 4:
                            GameObject barrel1 = Instantiate(barrel1Prefab, curPos, Quaternion.identity);
                            barrel1.transform.SetParent(transform, false);
                            break;
                        case 5:
                            GameObject crate1 = Instantiate(crate1Prefab, curPos, Quaternion.identity);
                            crate1.transform.SetParent(transform, false);
                            break;
                        case 6:
                            GameObject crate2 = Instantiate(crate2Prefab, curPos, Quaternion.identity);
                            crate2.transform.SetParent(transform, false);
                            break;
                    }
                    switch (UnityEngine.Random.Range(1, 25))
                    {
                        case 1:
                            GameObject cristal1 = Instantiate(cristalPrefab, curPos, Quaternion.identity);
                            cristal1.transform.SetParent(transform, false);
                            break;
                        case 2:
                            GameObject battery1 = Instantiate(batteryPrefab, curPos, Quaternion.identity);
                            battery1.transform.SetParent(transform, false);
                            break;
                    }

                    if (UnityEngine.Random.Range(1, 15) == 1)
                    {
                        GameObject torch = Instantiate(torchPrefab, curPos, Quaternion.identity);
                        torch.transform.SetParent(transform, false);
                    }
                    TileCount++;
                    gridHandler[curPos.x, curPos.y] = Grid.GRASS;
                    hasCreatedFloor = true;
                    Last_position = curPos;
                }
            }

            //Walker Methods
            ChanceToRemove(Last_position);
            ChanceToRedirect();
            ChanceToCreate();
            UpdatePosition();

            if (hasCreatedFloor)
            {
                yield return new WaitForSeconds(WaitTime);
            }
        }

        StartCoroutine(CreateWalls());
    }

    void ChanceToRemove(Vector3Int position)
    {
        int updatedCount = Walkers.Count;
        for (int i = 0; i < updatedCount; i++)
        {
            if (UnityEngine.Random.value < Walkers[i].ChanceToChange*1.5f && Walkers.Count > 1)
            {
                if (UnityEngine.Random.Range(1, 10) == 1)
                {
                    Instantiate(enemyPrefab, position, Quaternion.identity);
                }
                Walkers.RemoveAt(i);
                break;
            }
        }
    }

    void ChanceToRedirect()
    {
        for (int i = 0; i < Walkers.Count; i++)
        {
            if (UnityEngine.Random.value < Walkers[i].ChanceToChange)
            {
                WalkerObject curWalker = Walkers[i];
                curWalker.Direction = GetDirection();
                Walkers[i] = curWalker;
            }
        }
    }

    void ChanceToCreate()
    {
        int updatedCount = Walkers.Count;
        for (int i = 0; i < updatedCount; i++)
        {
            if (UnityEngine.Random.value < (Walkers[i].ChanceToChange) && Walkers.Count < MaximumWalkers)
            {
                Vector2 newDirection = GetDirection();
                Vector2 newPosition = Walkers[i].Position;

                WalkerObject newWalker = new WalkerObject(newPosition, newDirection, 0.5f);
                Walkers.Add(newWalker);
            }
        }
    }

    void UpdatePosition()
    {
        for (int i = 0; i < Walkers.Count; i++)
        {
            WalkerObject FoundWalker = Walkers[i];
            FoundWalker.Position += FoundWalker.Direction;
            FoundWalker.Position.x = Mathf.Clamp(FoundWalker.Position.x, 2, gridHandler.GetLength(0) - 3);
            FoundWalker.Position.y = Mathf.Clamp(FoundWalker.Position.y, 2, gridHandler.GetLength(1) - 3);
            Walkers[i] = FoundWalker;
        }
    }

    IEnumerator CreateWalls()
    {
        int batchSize = 100; // Number of tiles to process per frame
        int count = 0;

        // First pass: Fill empty tiles with walls
        for (int x = 0; x < gridHandler.GetLength(0); x++)
        {
            for (int y = 0; y < gridHandler.GetLength(1); y++)
            {
                if (gridHandler[x, y] == Grid.EMPTY)
                {
                    // Fill the empty tile with a wall
                    wallTileMap.SetTile(new Vector3Int(x, y, 0), Wall);
                    gridHandler[x, y] = Grid.WALL; // Mark it as a wall
                    count++;
                    // Yield after processing a batch
                    if (count >= batchSize)
                    {
                        count = 0;
                        yield return null; // Pause for one frame
                    }
                }
            }

        }

        // Second pass: Fill all tiles with floor
        count = 0; // Reset count for the next batch
        for (int x = 0; x < gridHandler.GetLength(0); x++)
        {
            for (int y = 0; y < gridHandler.GetLength(1); y++)
            {
                floorTileMap.SetTile(new Vector3Int(x, y, 0), Floor);
                gridHandler[x, y] = Grid.FLOOR; // Mark it as a floor
                count++;
                // Yield after processing a batch
                if (count >= batchSize)
                {
                    count = 0;
                    yield return null; // Pause for one frame
                }
            }
        }
        loadingScreenManager.TaskCompleted();
    }


}