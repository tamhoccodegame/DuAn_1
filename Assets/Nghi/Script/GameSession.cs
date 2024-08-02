using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    public static GameSession instance;

    [Header("Portal")]
    public int desPortalID;

	[Header("Checkpoint")]
	public float playerPositionX;
    public float playerPositionY;
    public string mapName;

	[Header("Coins")]
	public int coin = 0;
    public Text coin_Text;

	[Header("Pause Menu")]
	public GameObject pauseMenuUI;
    private bool isPaused = false;

    private Inventory inventory;
    private Equipment equipment;

	[Header("UI")]
	[SerializeField] private UI_Inventory uiInventory;
	[SerializeField] private UI_Market[] uiMarkets;
	[SerializeField] private LockSlotUI uiLockSlotUI;
	[SerializeField] private UI_Equipment[] uiEquipments;
    [SerializeField] private Player_HealthBar player_HealthBar;
    [SerializeField] private Slider bossHealthBar;


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

    public Player_HealthBar GetPlayer_HealthBar()
    {
        return player_HealthBar;
    }

    public Slider GetBossHealthBar()
    {
        return bossHealthBar;
    }

    public void UpdateCheckpoint(float _posX, float _posY, string _mapName)
    {
        playerPositionX = _posX;
        playerPositionY = _posY;
        mapName = _mapName;
    }

    public void Respawn()
    {
        StartCoroutine(LoadCheckpointCoroutine(mapName));
    }


    public IEnumerator LoadCheckpointCoroutine(string _mapName)
    {
        yield return SceneManager.LoadSceneAsync("Loading Scene");
        yield return new WaitForSeconds(8f);
        yield return SceneManager.LoadSceneAsync(_mapName);
        Transform player = GameObject.Find("Player").transform;
		player.position = new Vector2(playerPositionX, playerPositionY);
	}
    public void LoadScene(string _mapName)
    {
        StartCoroutine(LoadSceneCoroutine(_mapName));
    }

    public IEnumerator LoadSceneCoroutine(string _mapName)
    {
		yield return SceneManager.LoadSceneAsync("Loading Scene");
        yield return new WaitForSeconds(5f);
        yield return SceneManager.LoadSceneAsync(_mapName);
        Transform player = GameObject.Find("Player").transform;
        
        //Tim tat ca cac PORTAL trong scene moi
        Portal[] portals = FindObjectsOfType<Portal>();
        foreach(Portal portal in portals)
        {
            Debug.Log(portal.name);
            if(portal.portalID == desPortalID)
            {
                player.transform.position = portal.spawnPoint.position;
                break;
            }
        }

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
		//inventory.AddRune(new Rune(Rune.RuneType.Damage));
		//inventory.AddRune(new Rune(Rune.RuneType.DoubleJump));
		//inventory.AddRune(new Rune(Rune.RuneType.Fire));
  //      inventory.AddRune(new Rune(Rune.RuneType.Dash));

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
