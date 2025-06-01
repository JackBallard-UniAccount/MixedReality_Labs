using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class PinDetection : MonoBehaviour
{

    public GameObject Pin;
    private bool isHit = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == Pin)
        {
            isHit = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == Pin)
        {
            isHit = true;
        }
    }

    public void ResetPins()
    {
        Destroy(Pin);
        Instantiate(Pin, transform.position, transform.rotation);
    }

    public bool Score()
    {
        if (isHit)
        {
            Destroy(Pin);
            return true;
        }

        return false; // No score
    }
}
