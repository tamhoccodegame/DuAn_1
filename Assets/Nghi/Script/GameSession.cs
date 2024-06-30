using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    public int playerlives = 3;
    public Text playerlives_Text;
    public int score = 0;
    public Text score_Text;
    // Start is called before the first frame update
    void Start()
    {
        score_Text.text = score.ToString();
        playerlives_Text.text = playerlives.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        int number = FindObjectsOfType<GameSession>().Length;
        if (number > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayerDeath()
    {
        if (playerlives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    public void TakeLife()
    {
        playerlives--;

        //SceneManager.LoadScene("Map1_2D");
        //lay index cua scene hien tai
        int currentsceneindex = SceneManager.GetActiveScene().buildIndex;
        //load lai scene hien tai
        SceneManager.LoadScene(currentsceneindex);
        //slider.value = playerlives;
        //liveSlider.value = health.maxHealth;
    }

    public void ResetGameSession()
    {

        SceneManager.LoadScene(1);
        Destroy(gameObject); //destroy GameSession
        Time.timeScale = 1;
    }

    public void AddScore(int num)
    {
        score += num;
        score_Text.text = "SCORES: " + score.ToString();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
