using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImageSprite : MonoBehaviour
{
    [SerializeField]
    private float activeTime = 0.1f;
    private float timeActivated;
    private float alpha;
    [SerializeField]
    private float alphaSet = 0.8f;
    private float alphaMultiplier = 0.85f;

    private Transform playerTransform;

    private SpriteRenderer spriteRenderer;
    private SpriteRenderer playerSpriteRenderer;

    private Color color;

    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerTransform = GameObject.Find("Player").transform;
        playerSpriteRenderer = playerTransform.GetComponent<SpriteRenderer>();

        alpha = alphaSet;
        spriteRenderer.sprite = playerSpriteRenderer.sprite;
        transform.position = playerTransform.position;
        transform.rotation = playerTransform.rotation;
        timeActivated = Time.time;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        alpha*=alphaMultiplier;
        color = new Color(1f, 1f, 1f, alpha);
        spriteRenderer.color = color;

        if (Time.time >= (timeActivated + activeTime))
        {
            //Add back to pool
            PlayerAfterImagePool.Instance.AddToPool(gameObject);
        }
    }
}
