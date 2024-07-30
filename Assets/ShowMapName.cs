using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShowMapName : MonoBehaviour
{
    private Text mapName;
    // Start is called before the first frame update
    void Start()
    {
        mapName = GameObject.Find("mapName").GetComponent<Text>();
        StartCoroutine(ShowMapNameCoroutine());
	}

    private IEnumerator ShowMapNameCoroutine()
    {
        mapName.text = SceneManager.GetActiveScene().name;
        Animator mapAnim = GetComponent<Animator>();
		yield return new WaitForSeconds(mapAnim.GetCurrentAnimatorClipInfo(0).Length);
		mapName.gameObject.SetActive(false);
	}
	// Update is called once per frame
	void Update()
    {
        
    }
}
