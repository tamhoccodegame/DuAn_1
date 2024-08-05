using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingSceneText : MonoBehaviour
{
    public string[] sentences;
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        text.text = sentences[Random.Range(0, sentences.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
