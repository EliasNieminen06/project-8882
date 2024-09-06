using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSound : MonoBehaviour
{
    public string soundName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.Play(soundName);
            if (EnemyAI.instance != null)
            {
                EnemyAI.instance.Chase(transform.position);
            }
        }
    }
}
