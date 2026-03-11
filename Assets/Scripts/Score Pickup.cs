using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScorePickup : MonoBehaviour
{
    public int scoreValue = 10;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered with: " + other.name);

        if (other.CompareTag("Player"))
        {
            ScoreManager.Instance.AddScore(scoreValue);

            gameObject.SetActive(false);
            Invoke(nameof(Respawn), 20f);
        }
    }

    void Respawn()
    {
        gameObject.SetActive(true);
    }
}
