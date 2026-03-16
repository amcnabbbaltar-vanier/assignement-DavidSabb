using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;

    private TextMeshProUGUI timerText;

    private float timeElapsed = 0f;

    private float levelStartTime = 0f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
        UpdateTimerUI();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        timerText = GameObject.Find("TimerText")?.GetComponent<TextMeshProUGUI>();
        SaveLevelStartTime();
        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        if (timerText == null) return;

        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);

        timerText.text = "Time: " + minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    public void ResetTimer()
    {
        timeElapsed = 0f;
        UpdateTimerUI();
    }

    public void SaveLevelStartTime()
    {
        levelStartTime = timeElapsed;
    }

    public void RestoreLevelStartTime()
    {
        timeElapsed = levelStartTime;
        UpdateTimerUI();
    }
}