using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    [SerializeField] GameObject water;
    [SerializeField] GameObject wave;
    [SerializeField] GameObject flame;
    [SerializeField] GameObject sun;
    [SerializeField] GameObject wood;
    [SerializeField] GameObject leaf;
    [SerializeField] GameObject air;
    [SerializeField] GameObject tornado;

    public List<GameObject> skills;
    private int width = 2;
    private int height = 4;
    private int[,] gridArray;
    private GameObject[,] skillArray;
    private GameObject objectToInstantiate;
    private GameObject instantiatedObject;
    private bool isObjectSpawned;
    private TileSystem tileSystem;

    public static List<GameObject> skillSelected;

    void Awake()
    {
        gridArray = new int[width, height];
        skillArray = new GameObject[width, height];
        skills = new List<GameObject>();
        skillSelected = new List<GameObject>();
        tileSystem = FindObjectOfType<TileSystem>();
        isObjectSpawned = false;
    }
    void Start()
    {
        skills.Add(water);
        skills.Add(wave);
        skills.Add(flame);
        skills.Add(sun);
        skills.Add(wood);
        skills.Add(leaf);
        skills.Add(air);
        skills.Add(tornado);

        var skillAdded = new List<GameObject>();
        //var pos = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.transform.position.x + 350, Camera.main.transform.position.y, 10));

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                foreach (GameObject skill in skills)
                {
                    if (gridArray[x, y] == 0 && !skillAdded.Contains(skill))
                    {
                        skillArray.SetValue(Instantiate(skill, new Vector3(x + 0.5f, y + 0.5f), Quaternion.identity, this.gameObject.transform), x, y);
                        skillAdded.Add(skill);
                        gridArray[x, y] = 1;
                    }
                }
            }
        }

        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.transform.position.x + 600, Camera.main.transform.position.y, 50));
    }

    // gridarray = 0 empty, gridarray = 1 filled
    void Update()
    {
        if (BattleController.battleState == BattleState.Battling)
        {
            //Checking if pressing left mouse button and is not dragging yet
            if (Input.GetMouseButtonDown(0) && !isObjectSpawned)
            {
                Vector3 worldPos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
                int x = Mathf.FloorToInt((worldPos - transform.position).x / 1f);
                int y = Mathf.FloorToInt((worldPos - transform.position).y / 1f);
                try
                {
                    if (gridArray[x, y] == 1)
                    {
                        objectToInstantiate = Instantiate(skillArray[x, y], new Vector3(worldPos.x, worldPos.y, 1), Quaternion.identity, this.gameObject.transform);
                        isObjectSpawned = true;
                    }
                }
                catch{}
            }
            //Check if is currently dragging
            else if (Input.GetMouseButton(0) && isObjectSpawned && objectToInstantiate)
            {
                objectToInstantiate.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 5));
            }
            //Check if left mouse button is released
            else if (Input.GetMouseButtonUp(0) && isObjectSpawned && objectToInstantiate)
            { 
                Destroy(objectToInstantiate);
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Tiles"));
                if (hit.collider != null)
                {
                    //Check if dropped on an incorrect tile
                    if (tileSystem.GetTileValue() == 0)
                    {
                        instantiatedObject = Instantiate(objectToInstantiate, new Vector2(tileSystem.GetPosInTiles().x + 0.5f, tileSystem.GetPosInTiles().y + 0.5f) + (Vector2)tileSystem.transform.position, Quaternion.identity, this.gameObject.transform);
                        tileSystem.SetTileValue(1);
                        skillSelected.Add(instantiatedObject);
                        
                        Debug.Log("Incorrect!" + tileSystem.GetTileValue());
                    }
                    //Check if dropped on a correct tile
                    else if (tileSystem.GetTileValue() == 2)
                    {
                        instantiatedObject = Instantiate(objectToInstantiate, new Vector2(tileSystem.GetPosInTiles().x + 0.5f, tileSystem.GetPosInTiles().y + 0.5f) + (Vector2)tileSystem.transform.position, Quaternion.identity, this.gameObject.transform);
                        tileSystem.SetTileValue(3);
                        skillSelected.Add(instantiatedObject);
                        Debug.Log("Correct!" + tileSystem.GetTileValue());
                    }
                }
                else{}

                
                isObjectSpawned = false;
            }
        }
    }
}
