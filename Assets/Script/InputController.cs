using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public bool isPause = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
            UIManager.Instance.ToggleUI("Collector");
        }
    }


    public void TogglePause()
    {
        isPause = !isPause;

        if (isPause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void OnPuase()
    {
        isPause = true;
        Time.timeScale = 0f;
    }

    public void OffPause()
    {
        isPause = false;
        Time.timeScale = 1f;
    }
}
