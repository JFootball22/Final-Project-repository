using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text restartText;
    public Text winText;
    public Text hardModeText;

    private bool gameOver;
    private bool restart;
    private bool hardMode;
    private int score;
    private Done_BGScroller BGScroller;
    private Done_BGScroller Background;

    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;

    void Start()
    {
        gameOver = false;
        restart = false;
        hardMode = false;
        restartText.text = "";
        hardModeText.text = "";
        winText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());

        GameObject BGScrollerObject = GameObject.FindWithTag("GameController");
        if (BGScrollerObject != null)
        {
            BGScroller = BGScrollerObject.GetComponent<Done_BGScroller>();
        }
        if (BGScroller == null)
        {
            Debug.Log("Cannot find 'BGScroller' script");
        }

        GameObject BackgroundObject = GameObject.FindWithTag("Background");
        if (BackgroundObject != null)
        {
            Background = BackgroundObject.GetComponent<Done_BGScroller>();
        }
        if (Background == null)
        {
            Debug.Log("Cannot find 'BGScroller' script");
        }
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                SceneManager.LoadScene("SampleScene");
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                SceneManager.LoadScene("SampleScene(HardMode)");
            }
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'W' for Restart";
                hardModeText.text = "Press 'X' for Hard Mode";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Points: " + score;
        if (score >= 100)
        {
            winText.text = "You win! Game created by John Keith!";
            gameOver = true;
            restart = true;
            musicSource.clip = musicClipOne;
            musicSource.Play();
            musicSource.loop = true;
            BGScroller.scrollSpeed = 15.0f;
            Background.scrollSpeed = -10.0f;
        }
    }

    public void GameOver()
    {
        winText.text = "Game Over! Game created by John Keith!";
        gameOver = true;
        musicSource.clip = musicClipTwo;
        musicSource.Play();
        musicSource.loop = true;
    }
}