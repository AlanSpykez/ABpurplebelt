using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;


public class GameManager : MonoBehaviour
{
    public GameObject title;
    private Spawner spawner;
    private Vector2 screenBounds;
    public GameObject splash;
    // Start is called before the first frame update
    [Header("Player")]
    public GameObject playerPrefab;
    private GameObject player;
    private bool gameStarted = false;
    [Header("Score")]
    public TMP_Text scoreText;
    public int pointsWorth = 1;
    private int score;
    private int bestScore = 0;
    public TMP_Text bestScoreText;
    private bool beatBestScore;
    public Color normalColor;
    public Color bestScoreColor;

    private bool smokeCleared = true;
    private void Awake()
    {
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        scoreText.enabled = false;

        bestScoreText.enabled = false;
    }
    void Start()
    {
        spawner.active = false;
        title.SetActive(true);
        splash.SetActive(false);

        bestScore = PlayerPrefs.GetInt("BestScore");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            spawner.active = true;
            title.SetActive(false);
        }
        var nextBomb = GameObject.FindGameObjectsWithTag("Bomb");
        foreach (GameObject bombObject in nextBomb)
        {
            if (bombObject.transform.position.y < (-screenBounds.y - 12))
            {
                if (gameStarted)
                {
                    score += pointsWorth;
                    scoreText.text = "Score: " + score.ToString();
                    bestScoreText.text = "Best Score: " + bestScore.ToString();
                }
                Destroy(bombObject);
            }
        }
        if (!gameStarted)
        {
            if (Input.anyKeyDown && smokeCleared)
            {
                smokeCleared = false;
                ResetGame();
            }
        }
        else
        {
            if (!player)
            {
                OnPlayerKilled();
            }
        }

    }

    void ResetGame()
    {
        bestScoreText.color = normalColor;
        spawner.active = true;
        title.SetActive(false);
        splash.SetActive(false);
        scoreText.enabled = true;
        score = 0;

        beatBestScore = false;
        bestScoreText.enabled = true;

        scoreText.text = "Score: " + score.ToString();
        player = Instantiate(playerPrefab, new Vector3(0, 0, 0), playerPrefab.transform.rotation);
        gameStarted = true;
    }
    void OnPlayerKilled()
    {
        bestScoreText.enabled = true;
        spawner.active = false;
        gameStarted = false;
        splash.SetActive(true);

        Invoke("SplashScreen", 2);

        if(score > bestScore)
        {
            bestScoreText.color = bestScoreColor;
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
            beatBestScore = true;
            bestScoreText.text = "Best Score: " + bestScore.ToString();
        }
    }
    void SplashScreen()
    {
        smokeCleared = true;
        splash.SetActive(true);
    }
}
