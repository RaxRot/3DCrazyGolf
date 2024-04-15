 using System;
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    public static ShotController Instance;
    
    [Header("Ball Controller")]
    [SerializeField] private float maxShotPower;
    [SerializeField] private float powerChangeSpeed;
    private bool _powerGrowing;
    private bool _canShoot;
    private float _activeShotPower;
    private bool _inCup;

    private BallController _theBall;

    [Header("Events")] 
    public static Action OnShotsAdded;
    public static Action OnBalPositionSaved;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _canShoot = true;
        _theBall = FindFirstObjectByType<BallController>();
    }

    private void Update()
    {
        if (_canShoot)
        {
            SwitchPower();
            
            if (_powerGrowing)
            {
                ControlActivePower(maxShotPower);
            }
            else
            {
                ControlActivePower(0);
            }
            
            SetPowerBar(_activeShotPower,maxShotPower);
            
            if (Input.GetMouseButtonDown(0))
            {
               FireShot();
            }
        }
    }
    
    public void AllowShoot()
    {
        if (!_inCup)
        {
            _canShoot = true;
            
            UIManager.Instance.ShowPowerBar();
            CameraController.Instance.GetDirectionIndicator().SetActive(true);
        }
        
    }
    
    private void FireShot()
    {
        OnBalPositionSaved?.Invoke();
        
        _theBall.ShootBall(_activeShotPower);

        PreventShot();
        
        OnShotsAdded?.Invoke();
        
        AudioManager.Instance.PlaySFX(TagManager.BALL_HIT_SFX);
    }

    private void ControlActivePower(float target)
    {
        _activeShotPower = Mathf.MoveTowards
            (_activeShotPower, target, powerChangeSpeed * Time.deltaTime);
    }

    private void SetPowerBar(float activeShotPower, float maxShotPower)
    {
        UIManager.Instance.SetPowerBar(activeShotPower,maxShotPower);
    }

    private void SwitchPower()
    {
        if (_activeShotPower==maxShotPower)
        {
            _powerGrowing = false;
        }else if (_activeShotPower==0)
        {
            _powerGrowing = true;
        }
    }

    public void SetInCup()
    {
        _inCup = true;
        
        PreventShot();
    }

    private void PreventShot()
    {
        _canShoot = false;
        
        UIManager.Instance.HidePowerBar();
        
        CameraController.Instance.GetDirectionIndicator().SetActive(false);
    }
}
