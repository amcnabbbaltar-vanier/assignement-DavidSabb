using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoost : MonoBehaviour
{
    public float duration = 30f;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterMovement player = other.GetComponent<CharacterMovement>();

            if (player != null)
            {
                player.ActivateJumpBoost(duration);
            }

            gameObject.SetActive(false);
            Invoke(nameof(Respawn), 30f);
        }
    }

    void Respawn()
    {
        gameObject.SetActive(true);
    }
}
