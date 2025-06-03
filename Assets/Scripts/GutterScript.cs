using System.Collections;
using System.Collections.Generic;
using Meta.WitAi;
using UnityEngine;

public class GutterScript : MonoBehaviour
{
    [SerializeField] public NewPinManager pinManager;
    [SerializeField] public Object ball;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            pinManager.Gutter();
            Destroy(ball);
        }
    }
}
