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
        for(int i = 0; i < Random.Range(3,6); i++)
        {
            this.transform.parent.gameObject.GetComponent<Enemy>().TakeDamage(5);
            yield return new WaitForSeconds(.5f);
        }

        yield return null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
