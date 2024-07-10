using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAfterImage : MonoBehaviour
{
    [SerializeField]
    public GameObject afterImagePrefab;  // Prefab của after image
    public float afterImageLifetime = 0.5f;  // Thời gian tồn tại của after image
    public float spawnInterval = 0.1f;  // Khoảng thời gian giữa các after image

    private bool isDashing;
    private Coroutine afterImageCoroutine;

    // Gọi hàm này khi nhân vật bắt đầu dash
    public void StartDashing()
    {
        isDashing = true;
        afterImageCoroutine = StartCoroutine(SpawnAfterImages());
    }

    // Gọi hàm này khi nhân vật kết thúc dash
    public void StopDashing()
    {
        isDashing = false;
        if (afterImageCoroutine != null)
        {
            StopCoroutine(afterImageCoroutine);
        }
    }

    private IEnumerator SpawnAfterImages()
    {
        while (isDashing)
        {
            GameObject afterImage = Instantiate(afterImagePrefab, transform.position, transform.rotation);
            afterImage.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
            Destroy(afterImage, afterImageLifetime);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
