using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OnFire : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(OnFireDamaging());
    }

    private IEnumerator OnFireDamaging()
    {
        Player_Health player = this.transform.parent.GetComponent<Player_Health>();
        Enemy enemy = this.transform.parent.GetComponent<Enemy>();

        for (int i = 0; i < Random.Range(3,6); i++)
        {
            if(player != null)
            {
                player.TakeDamage(5);
            }
            else if(enemy != null)
            {
                enemy.TakeDamage(5);
            }

            yield return new WaitForSeconds(.5f);
        }

        yield return null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
