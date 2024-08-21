using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBG : MonoBehaviour
{
    public GameObject bg;
    public GameObject bgbefore;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bg.SetActive(true);

        }
        bgbefore.SetActive(false);
       
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bg.SetActive(false);

        }
    }
}
