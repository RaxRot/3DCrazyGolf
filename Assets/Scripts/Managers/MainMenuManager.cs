using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayMenuMusic();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(TagManager.CHOSE_LEVEL_SCENE_NAME);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
