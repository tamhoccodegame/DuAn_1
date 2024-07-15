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
    private Bomerang_Pool bomerang_Pool;

    
    private void Start()
    {
        startPosition = transform.position;
        playerPosition = GameObject.FindGameObjectWithTag("FirePoint").transform;
        bomerang_Pool = GameObject.Find("Bomerang_Pool").GetComponent<Bomerang_Pool>();
    }

    private void Update()
    {
        if (!isReturn)
        {
            

            transform.Translate(Vector2.right * speed * Time.deltaTime);
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

    public void ActivateBomerang(Vector2 startPos)
    {
        startPosition = startPos;
        transform.position = startPosition;
        
        isReturn = false;
        gameObject.SetActive(true);
    }

}
