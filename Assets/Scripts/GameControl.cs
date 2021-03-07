using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class GameControl : MonoBehaviour
{
    int score;
    public GameObject[] upSpikes;
    public GameObject[] downSpikes;
    public GameObject player;
    private Player playerScript;
    int numOfSpikes;
    Text scoreText;
    private Text _bestScore;
    [SerializeField] private Canvas PauseCanvas;
    [SerializeField] private GameObject blur;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject deathImage;
    private Color[] colors;
    
    
    private void Start()
    {
        colors = new[]
        {
            new Color(0, 0.5f, 1),
            new Color(0.5f, 0, 1),
            new Color(1, 0, 0.5f),
            Color.red, 
            Color.cyan,
            new Color(0, 1, 0),
            new Color(1, 0.5f, 0f),
        };
        deathImage.SetActive(false);
        Time.timeScale = 0;
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        _bestScore = GameObject.Find("BestScore").GetComponent<Text>();
        _bestScore.text = PlayerPrefs.GetString("BestScore", "0");
        playerScript = player.GetComponent<Player>();
        score = 0;
        numOfSpikes = 3;
        foreach (var spike in downSpikes)
        {
            spike.SetActive(false);
        }
        foreach (var spike in upSpikes)
        {
            spike.SetActive(false);
        }
        PauseCanvas.gameObject.SetActive(false);
        blur.SetActive(false);
    }

    public IEnumerator Restart()
    {
        scoreText.color = new Color(0.8f, 0.6f, 0.3f);
        if (score > Int32.Parse(_bestScore.text))
            _bestScore.text = scoreText.text;
        PlayerPrefs.SetString("BestScore", _bestScore.text);
        deathImage.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ChangeSpikes(bool up)
    {
        Color tempColor = colors[Random.Range(0, colors.Length)];
        List<int> s =  RandomList(numOfSpikes, new List<int>() {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, 0);
        if (up)
        {
            for (int i = 0; i < s.Count; i++)
            {
                if (s[i] == 1) downSpikes[i].SetActive(true);
                downSpikes[i].GetComponent<SpriteRenderer>().color = tempColor;
            }
            foreach (var spike in upSpikes)
            {
                spike.SetActive(false);
            }
        }
        else
        {
            foreach (var spike in downSpikes)
            {
                spike.SetActive(false);
            }
            for (int i = 0; i < s.Count; i++)
            {
                if (s[i] == 1) upSpikes[i].SetActive(true);
                upSpikes[i].GetComponent<SpriteRenderer>().color = tempColor;
            }
        }
    }

    List<int> RandomList(int limit, List<int> mass, int c)
    {
        int count = c;
        List<int> list = mass;
        for(int i = 0; i < list.Count; i++)
        {
            if (Random.Range(1, 5) == 2 && count < limit)
            {
                list[i] = 1;
                ++count;
            }
        }
        if (count < limit)
            list = RandomList(limit, list, count);
        return list;
    }

    public void AddScore()
    {
        score += 1;
        scoreText.text = score.ToString();
        if (score % 3 == 0 && numOfSpikes < 17) numOfSpikes += 1;
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }
    
    // Pause
    public void Pause()
    {
        Time.timeScale = 0;
        blur.SetActive(true);
        PauseCanvas.gameObject.SetActive(true);
    }

    public void Resume()
    {
        blur.SetActive(false);
        if (!startButton.activeSelf)
        Time.timeScale = 1;
        PauseCanvas.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        startButton.SetActive(false);
    }
    
    public void Exit()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetString("BestScore", _bestScore.text);
        Application.Quit();
    }
}
