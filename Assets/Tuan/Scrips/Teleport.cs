using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Teleport : MonoBehaviour
{
    public GameObject portal;
    private GameObject player;
    TeleportTimer teleportTimer;
    // Start is called before the first frame update
    void Start()
    {
        teleportTimer = FindObjectOfType<TeleportTimer>();
        player = GameObject.FindWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && teleportTimer.PortalTimer == teleportTimer.PortalTotalTimer && teleportTimer.PortalIsActive == false)
        {
           player.transform.position = new Vector2(portal.transform.position.x, portal.transform.position.y);
            teleportTimer.PortalIsActive = true;
        }
    }
    


}
