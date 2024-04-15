using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupController : MonoBehaviour
{
    [SerializeField] private GameObject inCupFx;
    
    private bool _ballInCup;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.BALL_TAG))
        {
            //Maybe add another check for staying
            if (!_ballInCup)
            {
                _ballInCup = true;
                
                ShotController.Instance.SetInCup();
              
                HoleManager.Instance.BallInCup();

                Quaternion rotation = Quaternion.Euler(-90f, 0f, 0f);
                Instantiate(inCupFx, transform.position, rotation);
                
                AudioManager.Instance.PlaySFX(TagManager.IN_HOLE_SFX);
            }
        }
    }
}
