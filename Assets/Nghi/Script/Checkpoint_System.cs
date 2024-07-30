using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint_System : MonoBehaviour
{
    public Transform respawnPoint;
    public Vector3 lastestCheckpointPosition;

    // Start is called before the first frame update
    void Start()
    {
        //Dat vi tri checkpoint ban dau la vi tri ban dau cua nhan vat
        lastestCheckpointPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)//Ham duoc goi khi nhan va va cham voi Checkpoint
    {
        if (collision.CompareTag("Checkpoint"))
        {
            Animator checkpointAnimation = collision.GetComponent<Animator>();
            //if (checkpointAnimation != null)
            //{
            checkpointAnimation.SetTrigger("Appear");
            lastestCheckpointPosition = collision.transform.position;
            //}

        }
    }

    public void Respawn()//Ham duoc goi khi nhan vat chet va duoc hoi sinh tai vi tri checkpoint gan nhat
    {
        transform.position = lastestCheckpointPosition;
    }
}
