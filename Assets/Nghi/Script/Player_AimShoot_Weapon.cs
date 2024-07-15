using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AimShoot_Weapon : MonoBehaviour
{
    private Transform aimTranform;

    public GameObject arm;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 20f;
    public Transform gunPivot; //Diem neo cho sung, vi tri goc co dinh cho nhan vat cam sung
    private Camera mainCamera;
    public bool isAiming;
    public float fireRate = 0.1f;
    private float fireTimer;
    

    // Start is called before the first frame update
    void Start()
    {
        arm.SetActive(false);
        mainCamera = Camera.main;
        isAiming = false;
        fireTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMouseAiming();
        HandleShooting();



    }

    public void HandleMouseAiming()
    {
        if(Input.GetMouseButtonDown(1))
        {
            arm.SetActive(true);
            isAiming=true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            arm.SetActive(false);
            isAiming=false;
        }

        if (isAiming)
        {
            //aimTranform = transform.Find("Aim");
            //Tinh toan toa do con tro chuot
            Vector3 mousePosition = GetMouseWorldPosition();
            //Tinh toan vector huong tu diem neo den vi tri con tro chuot
            Vector3 aimDirection = (mousePosition - transform.position).normalized;
            //Tinh toan goc hien tai cua sung bang cach su dung Mathf.Atan2 va chuyen doi sang do Mathf.Rad2Deg
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            //Gioi han goc quay 
            aimTranform.eulerAngles = new Vector3(0, 0, angle);
            //Debug.Log(angle);

            ////Tinh toan toa do con tro chuot
            //Vector3 mousePosition = Input.mousePosition;
            //mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
            //mousePosition.z = 0f;

            ////Tinh toan vector huong tu diem neo den vi tri con tro chuot
            //Vector2 direction = mousePosition - gunPivot.position;

            ////Tinh toan goc hien tai cua sung bang cach su dung Mathf.Atan2 va chuyen doi sang do Mathf.Rad2Deg
            //float angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;

            ////Gioi han goc quay tu -45 do toi 90 do bang Mathf.Clamp
            //angle = Mathf.Clamp(angle, 0, 0);//(angle, -1f, 0f)

            ////Dat huong quay cua sung bang cach su dung Quaternion.Euler
            //gunPivot.rotation = Quaternion.Euler(0, 0, angle);

        }
    }

    public void HandleShooting()
    {
        //Kiem tra neu dang nham ban thi tang bo dem thoi gian
        if (isAiming)
        {
            //Tang bo dem thoi gian
            fireTimer += Time.deltaTime;
            if( fireTimer > fireRate)
            {
                //Neu thoi gian giua cac lan ban da du thi goi ham Shoot de cho phep ban dan va reset bo dem 
                //thoi gian
                Shoot();
                fireTimer = 0f;
            }
        }
    }

    public void Shoot()
    {
        //Tao mot vien dan moi tu prefab
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        Rigidbody2D bulletRig = bullet.GetComponent<Rigidbody2D>();
        //Dat toc do cua dan theo huong cua vi tri ban
        bulletRig.velocity = bulletSpawn.right * bulletSpeed;
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
}
