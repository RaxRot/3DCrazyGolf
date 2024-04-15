using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleManager : MonoBehaviour
{
    public static HoleManager Instance;

     private int _shotsTaken;
     private Vector3 _lastBallPosition;

     [Header("Controls")]
     [SerializeField] private int par;
     [SerializeField] private float waitAfterBallInCup;
     [SerializeField] private float waitOutOfBounds=2f;

     [Header("Events")] 
     public static Action OnBallInHole;
     
     private void OnEnable()
     {
         ShotController.OnShotsAdded += AddShot;
         ShotController.OnBalPositionSaved += SetBallPosition;
     }

     private void OnDisable()
     {
         ShotController.OnShotsAdded -= AddShot;
         ShotController.OnBalPositionSaved -= SetBallPosition;
     }

     private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }
     
     private void Start()
     { 
         UIManager.Instance.SetParText(par);
         SetBallPosition();
         
         AudioManager.Instance.PlayLevelMusic();
     }

    private void AddShot()
    {
        _shotsTaken++;
        
        UIManager.Instance.UpdateShotsText(_shotsTaken);
    }

    public void BallInCup()
    {
        UIManager.Instance.DisplayInHole();

        int finalScore = _shotsTaken - par;
        string finalResult = "";
        finalResult = _shotsTaken==1 ? "Hole-In-One!!!" : CalculateFinalScore(finalScore);
        EndHole(finalResult);
    }

    private static string CalculateFinalScore(int finalScore)
    {
        string finalResult;
        switch (finalScore)
        {
            case -4:
                finalResult = "Condor (-4)";
                break;
            case -3:
                finalResult = "Albatross (-3)";
                break;
            case -2:
                finalResult = "Eagle (-2)";
                break;
            case -1:
                finalResult = "Birdie (-1)";
                break;
            case 0:
                finalResult = "Par";
                break;
            case 1:
                finalResult = "Bogey (+1)";
                break;
            case 2:
                finalResult = "Double Bogey (+2)";
                break;
            case 3:
                finalResult = "Triple Bogey (+3)";
                break;

            default:
                if (finalScore > 0)
                {
                    finalResult = "+" + finalScore;
                }
                else
                {
                    finalResult = finalScore.ToString();
                }
                break;
        }
        
        return finalResult;
    }

    private void EndHole(string scoreResult)
    {
        StartCoroutine(EndHoleCo(scoreResult));
    }

    private IEnumerator EndHoleCo(string scoreResult)
    {
        yield return new WaitForSeconds(waitAfterBallInCup);
        
        UIManager.Instance.ShowEndScreen(scoreResult);
        
        OnBallInHole?.Invoke();
    }

    private void SetBallPosition()
    {
        _lastBallPosition = BallController.Instance.transform.position;
    }

    public void OutOfBounds()
    {
        StartCoroutine(nameof(_OutOfBoundsCo));
    }

    private IEnumerator _OutOfBoundsCo()
    {
        AddShot();
        
        
        UIManager.Instance.ShowOutOfBounds();
        
        yield return new WaitForSeconds(waitOutOfBounds);
        
        BallController.Instance.GetRigidBody()
            .Move(_lastBallPosition,BallController.Instance.transform.rotation);
        BallController.Instance.GetRigidBody()
            .velocity=Vector3.zero;
        BallController.Instance.GetRigidBody()
            .angularVelocity = Vector3.zero;
        
        BallController.Instance.InBounds();
        
        ShotController.Instance.AllowShoot();
        UIManager.Instance.HideOutOfBounds();
    }
}
