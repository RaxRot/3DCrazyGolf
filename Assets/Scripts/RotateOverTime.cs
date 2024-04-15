using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOverTime : MonoBehaviour
{
    [Header("Rotation Path")]
    [SerializeField] private bool rotateXAxis;
    [SerializeField] private bool rotateYAxis;
    [SerializeField] private bool rotateZAxis;

    [Header("Rotation Settings")]
    [SerializeField] private float rotateSpeed;
    private float _currentRotation;

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        _currentRotation += rotateSpeed * Time.deltaTime;

        if (rotateXAxis)
        {
            transform.localRotation=Quaternion.Euler(_currentRotation,transform.localRotation.y,transform.localRotation.z);
        }
        if (rotateYAxis)
        {
            transform.localRotation=Quaternion.Euler(transform.localRotation.x,_currentRotation,transform.localRotation.z);
        }

        if (rotateZAxis)
        {
            transform.localRotation=Quaternion.Euler(transform.localRotation.x,transform.localRotation.y,_currentRotation);
        }
    }
}
