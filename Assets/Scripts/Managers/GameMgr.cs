using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour
{
    public static float Timer       { get; private set; }
    public static float DeltaTime   { get; private set; }

    public static bool IsPaused { get; private set; }

    // ======================================================================================
    // PUBLIC MEMBERS
    // ======================================================================================
    public void Start()
    {
        for (int i = 0; i < GUIMgr.ExitButtons.Length; i++)
        {
            GUIMgr.ExitButtons[i].onClick.AddListener(QuitGame);
        }

        for (int i = 0; i < GUIMgr.RestartButtons.Length; i++)
        {
            GUIMgr.RestartButtons[i].onClick.AddListener(RestartGame);
        }

        Timer = 0f;

        GUIMgr.PauseMenu.SetActive(false);
        IsPaused = false;
    }

    // ======================================================================================
    void Update ()
    {
        if (!IsPaused)
        {
            DeltaTime = Time.deltaTime;
            Timer += DeltaTime;
        }
        else
        {
            DeltaTime = 0;
        }

		if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsPaused = !IsPaused;

            GUIMgr.PauseMenu.SetActive(IsPaused);

            SceneMgr.Pause(IsPaused);
        }
	}

    // ======================================================================================
    public void QuitGame()
    {
        Application.Quit();
    }

    // ======================================================================================
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
