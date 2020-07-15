using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    int score;
    public GameObject[] upSpikes;
    public GameObject[] downSpikes;
    public GameObject player;
    public Player playerScript;
    int numOfSpikes;
    Text scoreText;
    

    private void Start()
    {
        scoreText = GameObject.Find("Score").GetComponent<Text>();
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
    }

    public IEnumerator Restart()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ChangeSpikes(bool up)
    {
        List<int> s =  RandomList(numOfSpikes, new List<int>() {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, 0);
        if (up)
        {
            
            for (int i = 0; i < s.Count; i++)
            {
                if (s[i] == 1) downSpikes[i].SetActive(true);
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
}
