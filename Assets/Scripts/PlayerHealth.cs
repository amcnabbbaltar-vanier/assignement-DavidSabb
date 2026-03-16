using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public TextMeshProUGUI healthText;

    public float fallThreshold = -10f;

    public float invincibilityDuration = 2f;
    private bool isInvincible = false;

    private Vector3 spawnPosition;

    void Start()
    {
        currentHealth = maxHealth;
        spawnPosition = transform.position;
        UpdateHealthUI();
    }

    void Update()
    {
        CheckFall();
    }

    void CheckFall()
    {
        if (transform.position.y < fallThreshold && !isInvincible)
        {
            TakeDamage(1);
            transform.position = spawnPosition;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        currentHealth -= damage;

        UpdateHealthUI();

        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            RestartLevel();
        }
        else
        {
            StartCoroutine(InvincibilityFrames());
        }
    }

    void RespawnPlayer()
    {
        transform.position = spawnPosition;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
            rb.velocity = Vector3.zero;
    }

    IEnumerator InvincibilityFrames()
    {
        isInvincible = true;

        yield return new WaitForSeconds(invincibilityDuration);

        isInvincible = false;
    }

    void RestartLevel()
    {
        ScoreManager.Instance.RestoreLevelStartScore();
        TimerManager.Instance.RestoreLevelStartTime();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void UpdateHealthUI()
    {
        string healthDisplay = "";

        for (int i = 0; i < currentHealth; i++)
        {
            healthDisplay += "● ";
        }

        healthText.text = "Health: " + healthDisplay;
    }
}