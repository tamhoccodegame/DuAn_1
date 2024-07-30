using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Vector2 CheckpointPos;
    void Start()
    {
        CheckpointPos = transform.position;
    }
    public void UpdateCheckpoint(Vector2 pos)
    {
        CheckpointPos = pos;
    }
    IEnumerator Respawn(float duration)
    {
        yield return new WaitForSeconds(duration);
        transform.position = CheckpointPos;
    }
}
