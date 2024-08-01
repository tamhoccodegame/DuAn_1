using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TriggerCutscene : MonoBehaviour
{
    public PlayableDirector cutScene;
    void Start()
    {
		cutScene.stopped += CutScene_stopped;
    }

	private void CutScene_stopped(PlayableDirector obj)
	{
        Destroy(cutScene.gameObject,1f);
        Destroy(gameObject,1f);
	}

	// Update is called once per frame
	void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.GetComponent<Player>())
        {
            cutScene.Play();
        }
	}
}
