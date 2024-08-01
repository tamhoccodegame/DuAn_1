using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBlockDoor : MonoBehaviour
{
    public GameObject Door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Door.SetActive(true);
	}

    public void HideDoor()
    {
        Door.SetActive(false);
        Destroy(gameObject);
    }
}
