using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Paralax : MonoBehaviour
{
    Transform cam;
    Vector3 CamstartPos;
    float distance;
    GameObject[] backgrounds;
    Material[] mat;
    float [] backspeed;
    float fathersBack;
    [Range(0.0001f, 0.05f)]
    public float parallaxspeed;
    void Start()
    {
        cam = Camera.main.transform;
        CamstartPos = cam.position;
        int backCount = transform.childCount;
        mat = new Material[backCount];
        backspeed = new float[backCount];
        backgrounds = new GameObject[backCount];
        for (int i = 0; i < backCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            mat[i] = backgrounds[i].GetComponent<Renderer>().material;
        }
        BackSpeedCaculate(backCount);
    }
    void BackSpeedCaculate(int backCount)
    {
        for (int i = 0; i < backCount; i++)
        {
            if ((backgrounds[i].transform.position.z - cam.position.z) > fathersBack)
            {
                fathersBack = backgrounds[i].transform.position.z - cam.position.z;
            }
        }
        for (int i = 0; i < backCount; i++)
        {
            backspeed[i] = 1 - (backgrounds[i].transform.position.z - cam.position.z);
        }


    }
    private void LateUpdate()
    {
        distance = cam.position.x - CamstartPos.x;
        transform.position = new Vector3(cam.position.x, transform.position.y, 0);
        for(int i = 0; i< backgrounds.Length; i++)
        {
            float speed = backspeed[i] * parallaxspeed;
            mat[i].SetTextureOffset("_MainTex", new Vector2(distance, 0)*speed);
        }
    }
}

