using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestCode : MonoBehaviour
{
	public GameObject objectToIgnore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Mathf.PingPong(Time.time * 30, 180f) - 90f;

		Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.down;

		// Raycast để xác định điểm va chạm
		RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, direction);

		foreach(RaycastHit2D h in hit)
        {
            if(h.collider.gameObject == objectToIgnore)
            {
				continue;
			}
			else if(h.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
			{
				float distance = h.distance;
				transform.localScale = new Vector3(transform.localScale.x, distance, transform.localScale.z);
				transform.rotation = Quaternion.Euler(0, 0, angle);
			}
			else
			{
				continue;
			}
		
		}
	}
}
