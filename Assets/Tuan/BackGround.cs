using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public Transform maincam;
    public Transform midbg;
    public Transform sidebg;
    public float lenght;

    // Update is called once per frame
    void Update()
    {
        if (maincam.position.x > midbg.position.x)
        {
            UpdateBackgroundPosition(Vector3.right);
        }
        else if(maincam.position.x < midbg.position.x)
        {
            UpdateBackgroundPosition(Vector3.left);
        }
    }
    void UpdateBackgroundPosition(Vector3 direction)
    {
        sidebg.position = midbg.position + direction;
        Transform temp = midbg;
        midbg = sidebg;
        sidebg   = temp;
    }
}
