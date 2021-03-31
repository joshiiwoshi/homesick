using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TileSystem : MonoBehaviour
{
    private int width = 8;
    private int height = 4;
    private float cellSize = 1f;
    private int[,] gridArray;
    private List<Vector3> clickedTiles;
    private GameObject[,] tiles;
    private bool outOfBounds;
    public static int maxCorrectTiles;
    public static float showTileTime;


    [SerializeField] GameObject tile;
    [SerializeField] Sprite correctTile;
    [SerializeField] Sprite emptyTile;
    [SerializeField] GameObject skillTile;

    void Awake()
    {
        gridArray = new int[width, height];
        clickedTiles = new List<Vector3>();
        tiles = new GameObject[width, height];
        outOfBounds = false;
        showTileTime = 3f;
    }
    void Start()
    {
    }

    //0 = incorrect empty tile, 1 = incorrect filled tile, 2 = correct empty tile, 3 = filled correct tile

    void Update()
    {
    }
    public void ResetTiles()
    {
        Array.Clear(tiles, 0, tiles.Length);
        Array.Clear(gridArray, 0, gridArray.Length);
        foreach (GameObject obj in SkillSystem.skillSelected)
        {
            Destroy(obj);
        }
        SkillSystem.skillSelected.Clear();
    }
    public IEnumerator SetTiles()
    {
        var cameraBottomLeft = transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 10));

        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                Destroy(tiles[x, y]);
            }
        }

        ResetTiles();
        BattleController.battleState = BattleState.PreBattle;

        //Set correct and incorrect
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                tiles.SetValue(Instantiate(tile, new Vector3(x + 0.5f, y + 0.5f) + cameraBottomLeft, Quaternion.identity, this.gameObject.transform), x, y);
                tiles[x, y].gameObject.tag = "IncorrectTile";
            }
        }
        for (int i = 0; i < maxCorrectTiles; i++)
        {
            int randX = UnityEngine.Random.Range(0, width - 1);
            int randY = UnityEngine.Random.Range(0, height - 1);
            if (gridArray[randX, randY] != 2)
            {
                gridArray[randX, randY] = 2;
                tiles[randX, randY].gameObject.tag = "CorrectTile";
            }
            else
            {
                i--;
                continue;
            }
        }



        //Show Correct
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                if (gridArray[x, y] == 2)
                {
                    tiles[x, y].gameObject.GetComponent<SpriteRenderer>().sprite = correctTile;
                }
            }
        }

        //Hide Correct
        yield return new WaitForSeconds(showTileTime);
        BattleController.battleState = BattleState.Battling;
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                if (gridArray[x, y] == 2)
                {
                    tiles[x, y].gameObject.GetComponent<SpriteRenderer>().sprite = emptyTile;
                }
            }
        }

    }

    public Vector2 GetPosInTiles()
    {
        var worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int x = Mathf.FloorToInt((worldPos - transform.position).x / cellSize);
        int y = Mathf.FloorToInt((worldPos - transform.position).y / cellSize);

        return new Vector2(x, y);
    }

    public int GetTileValue()
    {
        var worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int x = Mathf.FloorToInt((worldPos - transform.position).x / cellSize);
        int y = Mathf.FloorToInt((worldPos - transform.position).y / cellSize);

        return gridArray[x, y];
    }

    public void SetTileValue(int value)
    {
        var worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int x = Mathf.FloorToInt((worldPos - transform.position).x / cellSize);
        int y = Mathf.FloorToInt((worldPos - transform.position).y / cellSize);

        gridArray[x, y] = value;
    }

    public int GetIncorrectCount()
    {
        var incorrectCount = new List<int>();
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                if (gridArray[x, y] == 1)
                {
                    incorrectCount.Add(gridArray[x, y]);
                }
            }
        }
        return incorrectCount.Count;
    }

    public int GetCorrectCount()
    {
        var correctCount = new List<int>();
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                if (gridArray[x, y] == 3)
                {
                    correctCount.Add(gridArray[x, y]);
                }
            }
        }
        return correctCount.Count;
    }
}
