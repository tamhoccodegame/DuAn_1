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
    public int coin = 0;
    public Text coin_Text;

    public GameObject pauseMenuUI;
    private bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        playerlives_Text.text = "LIVES: "+playerlives.ToString();
        score_Text.text = "SCORES: "+score.ToString();
        coin_Text.text="COINS: "+coin_Text.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        OnPressESC();
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
        UpdateLiveAmount();
        
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

    public void UpdateLiveAmount()
    {
        playerlives_Text.text = "LIVES: " + playerlives.ToString();
    }

    public void AddCoin(int num)
    {
        coin += num;
        coin_Text.text = "COINS: " + coin.ToString();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    void OnPressESC()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else { Pause(); }
        }
    }
}
