using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState
{
    Moving,
    Battling,
    Finished
}

public class GameController : MonoBehaviour
{
    static GameController instance;
    static GameState gameState;
    static int distance;
    static Text text;
    //static bool running;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        text = FindObjectOfType<Text>();
        gameState = GameState.Moving;
        SceneManager.sceneLoaded += OnSceneLoaded;
        
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        instance.StopAllCoroutines();
        instance.StartCoroutine(Moving());

        if (scene.name == "RestartScene")
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Destroy(gameObject);
        }
    }
    void Start()
    {
        distance = 700;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = distance.ToString() + "m";
        if(distance <= 0)
        {
            SceneManager.LoadScene("Finished");
        }
    }

    IEnumerator Moving()
    {
        while (gameState == GameState.Moving)
        {
            yield return new WaitForSeconds(0.1f);
            distance -= 1;
            var rand = Random.Range(0, 50);
            if (rand == 25)
            {
                SceneManager.LoadScene("MainScene");
                gameState = GameState.Battling;
                break;
            }
        }
    }
}
