using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoseLevelManager : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayMenuMusic();
    }
    
    public void OnButtonClick(string buttonName)
    {
        SceneManager.LoadScene(TagManager.EMPTY_LEVEL_NAME + buttonName);
    }
}
