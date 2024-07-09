using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTimer : MonoBehaviour
{
    public float PortalTimer;
    public float PortalTotalTimer;
    public bool PortalIsActive;
    // Start is called before the first frame update
    void Start()
    {
        PortalTimer = PortalTotalTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (PortalIsActive == true)
        {
            PortalTimer -= Time.deltaTime;
        }
        if(PortalTimer <= 0)
        {
            PortalTimer = PortalTotalTimer;
            PortalIsActive = false;
        }
    }
}
