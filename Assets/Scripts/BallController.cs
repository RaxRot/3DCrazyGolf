using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public static BallController Instance;
    
    [Header("Elements")]
    private Rigidbody _rb;
   
    [Header("Movement")]
    [SerializeField] private float hitPower;
    [SerializeField] private float stopCutoff;
    [SerializeField] private float stopSpeed;
    private float _finalStopSpeed = 0.01f;
    private bool _isOutOfBounds;

    [SerializeField] private float boundsSideTreshold = 0.5f;
    private Vector3 _lastVelocity;
    
    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        
        GettingComponents();
    }

    private void GettingComponents()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
       
        float speed = _rb.velocity.magnitude;
        if (_rb.velocity.y>-0.01f)
        {
            if (speed<stopCutoff)
            {
                _rb.velocity *= stopSpeed;
                if (speed<_finalStopSpeed)
                {
                    _rb.velocity=Vector3.zero;
                    _rb.angularVelocity=Vector3.zero;

                    if (_isOutOfBounds)
                    {
                        return;
                    }
                    
                    ShotController.Instance.AllowShoot();
                }
            }
        }

        if (Vector3.Magnitude(_rb.velocity-_lastVelocity)>boundsSideTreshold)
        {
            AudioManager.Instance.PlaySFX(TagManager.BALL_HIT_SFX);
        }
        _lastVelocity = _rb.velocity;
    }

    public void ShootBall(float shotForce)
    {
        _rb.velocity = CameraController.Instance.transform.forward * shotForce;
    }

    public void InBounds()
    {
        _isOutOfBounds = false;
    }

    public Rigidbody GetRigidBody()
    {
        return _rb;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.OUT_OF_BOUNDS_TAG) && !_isOutOfBounds)
        {
            _isOutOfBounds = true;
          
            HoleManager.Instance.OutOfBounds();
        }
    }
}
