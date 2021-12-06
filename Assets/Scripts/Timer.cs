using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    float currentTime = 0;
    float bestTime;
    bool timing = false;

    SceneController sceneController;

    [Header("UI Countdown Panel")]
    public GameObject countdownPanel;
    public Text countdownText;

    [Header("UI In Game Panel")]
    public TMP_Text timerText;

    [Header("UI Game Over Panel")]
    public GameObject timesPanel;
    public Text myTimeResult;
    public Text bestTimeResult;

    private void Awake()
    {
        sceneController = FindObjectOfType<SceneController>();
    }


    void Start()
    {   
        timesPanel.SetActive(false);
        countdownPanel.SetActive(false);
        timerText.text = "";
        
    }

    void Update()
    {
        if (timing)
        {
            currentTime += Time.deltaTime;
            timerText.text = "Time " + "<color=red>" + currentTime.ToString("F3");
        }
    }

    public IEnumerator StartCountdown()
    {
        bestTime = PlayerPrefs.GetFloat("BestTime" + sceneController.GetSceneName());
        if (bestTime == 0f) bestTime = 600f;

        yield return new WaitForEndOfFrame();

        countdownPanel.SetActive(true);
        countdownText.text = "3";
        yield return new WaitForSeconds(1);
        countdownText.text = "2";
        yield return new WaitForSeconds(1);
        countdownText.text = "1";
        yield return new WaitForSeconds(1);
        countdownText.text = "Go!";
        yield return new WaitForSeconds(1);
        StartTimer();
        countdownPanel.SetActive(false);
    }

    public void StartTimer()
    {
        currentTime = 0;
        timing = true;
    }

    public void StopTimer()
    {   
        timing = false;
        timesPanel.SetActive(true);
        myTimeResult.text = currentTime.ToString("F3");
        bestTimeResult.text = bestTime.ToString("F3");

        if (currentTime <= bestTime)
        {
            bestTime = currentTime;
            PlayerPrefs.SetFloat("BestTime" + sceneController.GetSceneName(), bestTime);
            bestTimeResult.text = bestTime.ToString("F3") + " !! NEW BEST !!";
        }
    }

    public bool IsTiming()
    {
        return timing;
    }
}
