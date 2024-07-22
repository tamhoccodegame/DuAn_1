using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AimShoot_Weapon : MonoBehaviour
{
    private Transform aimTranform;

    public GameObject arm;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 10f;
    private Camera mainCamera;
    public bool isAiming;
    public float fireRate = 2f;
    //private float fireTimer;

    //public LineRenderer lineRenderer;
    public int lineSegmentCount = 20;
    private Vector3 startPosition;
    private Vector3 aimPosition;
    private Vector3 aimDirection;
    private Bomerang_Pool bomerang_Pool;


    // Start is called before the first frame update
    void Start()
    {
        arm.SetActive(false);
        mainCamera = Camera.main;
        isAiming = false;
        //fireTimer = 0f;
        //lineRenderer.positionCount = lineSegmentCount;
        //lineRenderer = GetComponent<LineRenderer>();
        bomerang_Pool = GameObject.Find("Bomerang_Pool").GetComponent<Bomerang_Pool>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMouseAiming();
    }

    public void HandleMouseAiming()
    {
        if(Input.GetMouseButtonDown(1))
        {
            arm.SetActive(true);
            isAiming=true;
            startPosition = bulletSpawn.position;
            //DrawLine();
        }
        if (Input.GetMouseButtonUp(1))
        {
            arm.SetActive(false);
            isAiming=false;
            Shoot();
            //HandleShooting();
        }

        if (isAiming)
        {
            //Tinh toan toa do con tro chuot
            Vector3 mousePosition = GetMouseWorldPosition();
            //Tinh toan vector huong tu diem neo den vi tri con tro chuot
            Vector3 aimDirection = (mousePosition - transform.position).normalized; //Vector3
            //Tinh toan goc hien tai cua sung bang cach su dung Mathf.Atan2 va chuyen doi sang do Mathf.Rad2Deg
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            //Gioi han goc quay 
            aimTranform.eulerAngles = new Vector3(0, 0, angle);
            //Debug.Log(angle);
            

            Vector3 aimLocalScale = Vector3.one;
            if (angle > 90 || angle < -90)
            {
                aimLocalScale.y = -1f;
            }
            else
            {
                aimLocalScale.y = 1f;
            }

            aimTranform.localScale = aimLocalScale;

        }
        else
        {
            //lineRenderer.positionCount = 0;
        }
    }

    public void DrawLine()
    {
        Vector3[] points = new Vector3[lineSegmentCount];
        Vector3 lineDirection = aimPosition - startPosition;
        float segmentLength = lineDirection.magnitude / lineSegmentCount;

        for (int i = 0; i < lineSegmentCount; i++)
        {
            float time = (i * segmentLength) / bulletSpeed;
            Vector3 gravityEffect = 0.5f * Physics2D.gravity * (time * time);
            points[i] = startPosition + lineDirection.normalized * (bulletSpeed * time) + new Vector3(gravityEffect.x, gravityEffect.y, 0f);
        }

        //lineRenderer.positionCount = points.Length;
        //lineRenderer.SetPositions(points);
    }

    public void Shoot()
    {
        //Tao mot vien dan moi tu prefab
        //GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        //Rigidbody2D bulletRig = bullet.GetComponent<Rigidbody2D>();
        ////Dat toc do cua dan theo huong cua vi tri ban
        //bulletRig.velocity = bulletSpawn.right * bulletSpeed;
        //Vector2 direction = (aimPosition - startPosition).normalized;
        //bulletRig.velocity = direction * bulletSpeed;

        GameObject boomerang = bomerang_Pool.GetPooledObject();
        if (boomerang != null)
        {
            Bomerang boomerangScript = boomerang.GetComponent<Bomerang>();
            boomerangScript.ActivateBomerang(bulletSpawn.position);
        }
    }

    private void Awake()
    {
        aimTranform = transform.Find("Aim");
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.WorldToScreenPoint(screenPosition);
        return worldPosition;
    }

    //public void HandleShooting()
    //{
    //    Vector3 mousePosition = GetMouseWorldPosition();
    //    //Kiem tra neu dang nham ban thi tang bo dem thoi gian
    //    if (isAiming)
    //    {

    //        //Tang bo dem thoi gian
    //        fireTimer += Time.deltaTime;
    //        if (fireTimer >= fireRate)
    //        {
    //            //Neu thoi gian giua cac lan ban da du thi goi ham Shoot de cho phep ban dan va reset bo dem 
    //            //thoi gian
    //            Shoot();
    //            fireTimer = 0f;
    //        }
    //    }
    //}
}
