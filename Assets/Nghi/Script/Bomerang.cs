using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomerang : MonoBehaviour
{
    public float speed = 10f;
    public float maxDistance = 20f;
    private Transform playerPosition;
    private bool isReturn = false;
    private Vector2 startPosition;
    private Vector2 direction;
    //private Bomerang_Pool bomerang_Pool;

    public GameObject bomerangShootEffect;
    private void Start()
    {
        startPosition = transform.position;
        playerPosition = GameObject.Find("Aim").transform.Find("Arm").transform.Find("firePoint").GetComponent<Transform>();
        //bomerang_Pool = GameObject.Find("Bomerang_Pool").GetComponent<Bomerang_Pool>();
    }

    private void Update()
    {
        if (!isReturn)
        {
            transform.Translate(direction * speed * Time.deltaTime);
            if (Vector2.Distance(startPosition, transform.position) > maxDistance)
            {
                isReturn = true;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPosition.position, speed * Time.deltaTime);
            if(Vector2.Distance(transform.position, playerPosition.position) < 0.1f)
            {
                gameObject.SetActive(false);
                isReturn = false;
            }
        }
    }

    public void ActivateBomerang(Vector2 startPos, Vector2 targetPos)
    {
        startPosition = startPos;
        transform.position = startPosition;
        
        direction = (targetPos - startPos).normalized;
        isReturn = false;
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            //bomerangShootEffect.Play();
            GameObject shootEffect = Instantiate(bomerangShootEffect, transform.position, Quaternion.identity);
        }
    }

}
