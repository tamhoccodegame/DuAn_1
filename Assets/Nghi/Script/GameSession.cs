using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    public static GameSession instance;
    
    public int playerlives = 3;
    public Text playerlives_Text;
    public int score = 0;
    public Text score_Text;
    public int coin = 0;
    public Text coin_Text;

    public GameObject pauseMenuUI;
    private bool isPaused = false;

    private Inventory inventory;
    private Equipment equipment;

	[SerializeField] private UI_Inventory uiInventory;
	[SerializeField] private UI_Market[] uiMarkets;
	[SerializeField] private LockSlotUI uiLockSlotUI;
	[SerializeField] private UI_Equipment[] uiEquipments;

	// Start is called before the first frame update

	public Inventory GetInventory()
    {
        return inventory;
    }

    public Equipment GetEquipment()
    {
        return equipment;
    }

    public UI_Inventory GetUI_Inventory()
    {
        return uiInventory;
    }

    public UI_Market[] GetUI_Market()
    {
        return uiMarkets;
    }

    public LockSlotUI GetLockSlotUI()
    {
        return uiLockSlotUI;
    }

    public UI_Equipment[] GetUI_Equipment()
    {
        return uiEquipments;
    }

    IEnumerator LoadSceneTest()
    {
        string savedSceneName = SceneManager.GetActiveScene().name;

		yield return SceneManager.LoadSceneAsync("Land Of Holy");
        yield return new WaitForSeconds(15f);
        yield return SceneManager.LoadSceneAsync(savedSceneName);
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
			DontDestroyOnLoad(gameObject);
        }

		inventory = new Inventory();
		inventory.AddRune(new Rune(Rune.RuneType.Damage));
		inventory.AddRune(new Rune(Rune.RuneType.DoubleJump));
		inventory.AddRune(new Rune(Rune.RuneType.Fire));

		equipment = new Equipment();
	}

	void Start()
	{
        pauseMenuUI.SetActive(false);
    }


	// Update is called once per frame
	void Update()
	{
		OnPressESC();
		if (Input.GetKeyDown(KeyCode.P))
		{
			StartCoroutine(LoadSceneTest());
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
