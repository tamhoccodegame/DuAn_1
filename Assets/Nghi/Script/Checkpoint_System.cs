using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint_System : MonoBehaviour
{
    public static Checkpoint_System Instance { get; private set; }

    private Vector3 lastCheckpointPosition;
    public Transform respawnPoint;
    public Vector3 lastestCheckpointPosition;
    //private Vector3 respawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        //Dat vi tri checkpoint ban dau la vi tri ban dau cua nhan vat
        lastestCheckpointPosition = transform.position;
        //respawnPosition = transform.position;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetLastCheckpointPosition(Vector3 position)
    {
        lastCheckpointPosition = position;
    }

    public Vector3 GetLastCheckpointPosition()
    {
        return lastCheckpointPosition;
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
            FindObjectOfType<SoundManager>().PlayAudio("Checkpoint");
            checkpointAnimation.SetTrigger("Appear");
            SetLastCheckpointPosition(collision.transform.position);
            //lastestCheckpointPosition = collision.transform.position;
            //respawnPosition = collision.transform.position;
            //}

        }
    }

    public void Respawn(Transform player)//Ham duoc goi khi nhan vat chet va duoc hoi sinh tai vi tri checkpoint gan nhat
    {
        player.position = GetLastCheckpointPosition();
        //transform.position = Checkpoint_System.Instance.GetLastCheckpointPosition();
        //transform.position = lastestCheckpointPosition;
        //transform.position= respawnPosition;
    }
}
