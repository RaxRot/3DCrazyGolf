using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    
    [Header("Movement")]
    [SerializeField] private Transform target;
    [SerializeField] private float rotationSpeed;

    [Header("Rotation")]
    [SerializeField] private Transform verticalPoint;
    [SerializeField] private float minRotationVertical = 0;
    [SerializeField] private float maxRotationVertical = 75;
    private float _verticalRotation;
    private float _rotation;
    private bool _canMove;

    [Header("Aim")] 
    [SerializeField] private GameObject directionIndicator;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        HoleManager.OnBallInHole += PreventMovement;
    }

    private void OnDisable()
    {
        HoleManager.OnBallInHole -= PreventMovement;
    }

    private void Start()
    {
        _verticalRotation = verticalPoint.localRotation.eulerAngles.x;
        _canMove = true;
    }

    private void LateUpdate()
    {
        ControlRotation();
    }

    private void ControlRotation()
    {
        if (!_canMove)
        {
            return;
        }
        
        transform.position = target.position;
        
        HorizontalRotation();
        
        VerticalRotation();
    }

    private void HorizontalRotation()
    {
        _rotation += Input.GetAxisRaw(TagManager.HORIZONTAL_AXIS)*rotationSpeed*Time.deltaTime;
        transform.rotation=Quaternion.Euler(0f,_rotation,0f);
    }

    private void VerticalRotation()
    {
        _verticalRotation += Input.GetAxisRaw(TagManager.VERTICAL_AXIS) * rotationSpeed * Time.deltaTime;
        _verticalRotation = Math.Clamp(_verticalRotation, minRotationVertical, maxRotationVertical);
        verticalPoint.localRotation=Quaternion.Euler(_verticalRotation,0,0);
    }
    
    public GameObject GetDirectionIndicator()
    {
        return directionIndicator;
    }

    private void PreventMovement()
    {
        _canMove = false;
    }
}
