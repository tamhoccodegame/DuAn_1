using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FindObjectOfType<GameSession>().AddCoin(1);
            FindObjectOfType<GameSession>().AddScore(1);
            //var go = Instantiate(coinEffect, transform.position, transform.rotation);
            //FindObjectOfType<SoundManager>().PlayAudio("Collect_Coin");
            Destroy(gameObject, 0.1f);


        }
    }
}
