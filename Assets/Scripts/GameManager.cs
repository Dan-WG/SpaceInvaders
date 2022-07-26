using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int score = 0;
    public TextMeshProUGUI ScoreText;
    public GameObject Player;
    public static int levelcount = 0;

    public static int enemycount;

    [SerializeField]
    GameObject EnemyPrefab;

    JSONReader JSONInfo;

    //JSON variables
    public static string type;
    public static int rows;

    Vector3 SpawnPoint = new Vector3(2.7f,0, 0);
    Vector3 NextRow = new Vector3(0f, -1.9f, 0f);
    Vector3 brickOffset = new Vector3(1f, 0, 0);

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "Game")
        {
            StartCoroutine(ReadJSON());
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
    }

    void UpdateScore()
    {
        if(ScoreText != null)
        ScoreText.text = score.ToString();
    }

    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
        levelcount = 0;
        score = 0;
    }

    
    public void Exit()
    {
        Application.Quit();
    }

    IEnumerator ReadJSON()
    {
        yield return new WaitForSeconds(1f);


        if (type == "grid")
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    GameObject enemyInstance = Instantiate(EnemyPrefab);
                    enemyInstance.transform.position += (SpawnPoint * j);
                    enemyInstance.transform.position += (NextRow * i);
                    enemycount++;
                }

            }
        }
        else if (type == "brick")
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (i % 2 == 0)
                    {
                        GameObject enemyInstance = Instantiate(EnemyPrefab);
                        enemyInstance.transform.position += (SpawnPoint * j);
                        enemyInstance.transform.position += (NextRow * i);
                        enemycount++;
                    }
                    else
                    {
                        GameObject enemyInstance = Instantiate(EnemyPrefab);
                        enemyInstance.transform.position += ((SpawnPoint * j) + brickOffset);
                        enemyInstance.transform.position += (NextRow * i);
                        enemycount++;

                    }

                }

            }
        }
    }
}
