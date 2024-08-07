using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBlockDoor : MonoBehaviour
{
    public GameObject Door;
    private AudioSource audioSource;
    public AudioClip bossMusic;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Door.SetActive(true);
		GameSession.instance.GetBossHealthBar().transform.parent.gameObject.SetActive(true);
        audioSource.clip = bossMusic;
        audioSource.Play();
        Destroy(gameObject);
	}

    public void HideDoor()
    {
        Door.SetActive(false);
        Destroy(gameObject);
    }
}
