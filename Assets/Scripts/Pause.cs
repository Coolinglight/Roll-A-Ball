using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pausepanel;
    bool isPaused = false;

    private void Start()
    {
        pausepanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            pausepanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pausepanel.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
