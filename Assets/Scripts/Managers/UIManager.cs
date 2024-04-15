using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    [Header("Game Elements")]
    [SerializeField] private Slider powerBar;
    [SerializeField] private GameObject InHoleText;

    [Header("Shots Info")]
    [SerializeField] private TMP_Text shotsText;
    [SerializeField] private TMP_Text parText;

    [Header("End Panel")]
    [SerializeField] private GameObject endScreen;
    [SerializeField] private TMP_Text endScreenScoreText;
    [SerializeField] private GameObject outOfBoundsText;
    [SerializeField] private string nextHoleName;

    [Header("Resume Panel")]
    [SerializeField]private GameObject resumePanel;
    private bool _isOpen;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Time.timeScale = 1f;
    }

    private void Update()
    {
        ControlResumePanel();
    }

    private void ControlResumePanel()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResumePanel();
        }
    }

    private void ResumePanel()
    {
        _isOpen = !_isOpen;
        Time.timeScale = _isOpen ? 0f : 1f;
        resumePanel.SetActive(_isOpen);
    }

    public void ShowPowerBar()
    {
        powerBar.gameObject.SetActive(true);
    }

    public void SetPowerBar(float power,float maxPower)
    {
        powerBar.maxValue = maxPower;
        powerBar.value = power;
    }

    public void HidePowerBar()
    {
        powerBar.gameObject.SetActive(false);
    }

    public void DisplayInHole()
    {
        InHoleText.SetActive(true);
    }

    public void UpdateShotsText(int currentShots)
    {
        shotsText.text = "Shots: " + currentShots;
    }

    public void SetParText(int currentPar)
    {
        parText.text = "Par: " + currentPar;
    }

    public void ShowEndScreen(string scoreResult)
    {
        endScreenScoreText.text = scoreResult;
        endScreen.gameObject.SetActive(true);
    }

    public void ShowOutOfBounds()
    {
        outOfBoundsText.SetActive(true);
    }

    public void HideOutOfBounds()
    {
        outOfBoundsText.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(TagManager.MAIN_MENU_SCENE_NAME);
    }

    public void NextHole()
    {
        SceneManager.LoadScene(nextHoleName);
    }
}
