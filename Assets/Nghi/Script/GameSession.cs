using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Playables;
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

    [Header("Dialogue Canvas")]
    public GameObject dialoguePanel;
    public Text dialogueText;

    [Header("Map")]
    public GameObject map;

    private Inventory inventory;
    private Equipment equipment;

	[Header("UI")]
	[SerializeField] private UI_Inventory uiInventory;
	[SerializeField] private UI_Market[] uiMarkets;
	[SerializeField] private LockSlotUI uiLockSlotUI;
	[SerializeField] private UI_Equipment[] uiEquipments;
    [SerializeField] private Player_HealthBar player_HealthBar;
    [SerializeField] private Player_StaminaBar player_StaminaBar;
	[SerializeField] private Slider player_SkillBar;
	[SerializeField] private Slider bossHealthBar;

    [Header("TransitionImage")]
    [SerializeField] private Animator transitionAnimator;


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

    public Player_StaminaBar GetPlayer_StaminaBar()
    {
        return player_StaminaBar;
    }

    public Slider GetPlayer_SkillBar()
    {
        return player_SkillBar;
    }

    public Slider GetBossHealthBar()
    {
        return bossHealthBar;
    }

    public Text GetDialogueText()
    {
        return dialogueText;
    }

    public int GetCoin()
    {
        return coin;
    }
    public void UpdateCheckpoint(float _posX, float _posY, string _mapName)
    {
        playerPositionX = _posX;
        playerPositionY = _posY;
        mapName = _mapName;
		Debug.Log($"Checkpoint updated: {_mapName}, Position: ({_posX}, {_posY})");
	}

	public void LoadCheckpoint()
	{
		StartCoroutine(LoadCheckpointCoroutine());
	}

	public IEnumerator LoadCheckpointCoroutine()
    {
		transitionAnimator.SetTrigger("FadeOut");
		yield return new WaitForSeconds(3f);

		foreach (Transform child in transform)
		{
            if(transitionAnimator.transform.IsChildOf(child))
                continue;
			child.gameObject.SetActive(false);
		}
       
		yield return SceneManager.LoadSceneAsync("Loading Scene");
		transitionAnimator.gameObject.SetActive(true);
		transitionAnimator.SetTrigger("FadeIn");
		Debug.Log("Loading Scene Complete");
        yield return new WaitForSeconds(3f);


		Debug.Log("Ready to load " + mapName);
		transitionAnimator.SetTrigger("FadeOut");
		yield return new WaitForSeconds(3f);


		yield return SceneManager.LoadSceneAsync(mapName);
		foreach (Transform child in transform)
		{
            if (child.gameObject.name == "Canvas Variant")
            {
                child.gameObject.SetActive(true);

                foreach (Transform grandchild in child)
                {
                    if (grandchild.gameObject.name == "BossHealthBar")
                    {
                        grandchild.gameObject.SetActive(false);
                        break;
                    }
                    else continue;
                }
                
            }
            else if (child.gameObject.name == "DialogueCanvas" || child.gameObject.name == "UI_Shops")
            {
                child.gameObject.SetActive(true);
                foreach (Transform grandchild in child)
                {
                    grandchild.gameObject.SetActive(false);
                }
            }
            else continue;
		}
		Debug.Log($"Load {mapName} succecssful");
		Transform player = GameObject.Find("Player").transform;
		Debug.Log("Find player successful");
		player.position = new Vector2(playerPositionX, playerPositionY);
		Debug.Log("Player position Set!");

		transitionAnimator.SetTrigger("FadeIn");
		yield return new WaitForSeconds(3f);

	
		
	}
    public void LoadScene(string _mapName)
    {
        StartCoroutine(LoadSceneCoroutine(_mapName));
    }

    public IEnumerator LoadSceneCoroutine(string _mapName)
    {
		transitionAnimator.SetTrigger("FadeOut");
		yield return new WaitForSeconds(3f);

		foreach (Transform child in transform)
		{
			if (transitionAnimator.transform.IsChildOf(child))
				continue;
			child.gameObject.SetActive(false);
		}


		yield return SceneManager.LoadSceneAsync("Loading Scene");
		transitionAnimator.SetTrigger("FadeIn");
		yield return new WaitForSeconds(5f);

		yield return SceneManager.LoadSceneAsync(_mapName);


		foreach (Transform child in transform)
		{
			if (child.gameObject.name == "Canvas Variant")
			{
				child.gameObject.SetActive(true);

				foreach (Transform grandchild in child)
				{
					if (grandchild.gameObject.name == "BossHealthBar")
					{
						grandchild.gameObject.SetActive(false);
						break;
					}
					else continue;
				}
				
			}
			else if (child.gameObject.name == "DialogueCanvas")
			{
				child.gameObject.SetActive(true);
				foreach (Transform grandchild in child)
				{
					grandchild.gameObject.SetActive(false);
				}
			}
            else if (child.gameObject.name == "UI_Shops")
            {
                Debug.Log(child.gameObject.name);
				child.gameObject.SetActive(true);
				foreach (Transform grandchild in child)
				{
					grandchild.gameObject.SetActive(false);
				}
			}
			else continue;
		}

		Transform player = GameObject.Find("Player").transform;
        
        //Tim tat ca cac PORTAL trong scene moi
        Portal[] portals = FindObjectsOfType<Portal>();
        foreach(Portal portal in portals)
        {
            if(portal.portalID == desPortalID)
            {
                player.transform.position = portal.spawnPoint.position;
                UpdateCheckpoint(portal.spawnPoint.position.x, portal.spawnPoint.position.y, _mapName);
                break;
            }
        }

		transitionAnimator.SetTrigger("FadeIn");
		yield return new WaitForSeconds(3f);

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
        dialoguePanel.SetActive(false);
        //bossHealthBar.transform.parent.gameObject.SetActive(false);
    }


	// Update is called once per frame
	void Update()
	{
		OnPressESC();

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            map.gameObject.SetActive(true);
        }
        else if(Input.GetKeyUp(KeyCode.Tab))
        {
            map.gameObject.SetActive(false);
        }
	}


    public void AddCoin(int num)
    {
        coin += num;
        coin_Text.text = coin.ToString("000000");
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
