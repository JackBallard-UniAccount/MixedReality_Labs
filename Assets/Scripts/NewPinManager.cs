using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class NewPinManager : MonoBehaviour
{
    
    public static NewPinManager Instance;
    public BowlingGame bowlingLogic;

    private int knockedOverCount = 0;
    private List<BowlingPin> bowlingPins = new List<BowlingPin>();
    [SerializeField] public TextMeshPro scoreOutput;
    [SerializeField] public Collider gutter;
    [SerializeField] public ObjectSpawner spawner; 

    private bool timerStarted = false;
    private float timer = 0f;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        scoreOutput.text = bowlingLogic.GetScoreDisplay();
        if (timerStarted)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                DisableAllKnockedOverPins();
                timerStarted = false;
            }
        }
    }

    public void PinKnockedOver(BowlingPin pin)
    {
        knockedOverCount++;
        bowlingPins.Add(pin);
        timerStarted = true;
        timer = 5f;
        Debug.Log("Pin knocked over! Total: " + knockedOverCount);
    }

    public void Gutter()
    {
        timerStarted = true;
        timer = 5f;
    }

    public void ResetAllPins()
    {
        foreach (BowlingPin pin in bowlingPins)
        {
            pin.ResetPin();
        }
        
    }

    private void DisableAllKnockedOverPins()
    {
        foreach (BowlingPin pin in bowlingPins)
        {
            pin.Disable();
        }
        bowlingLogic.Roll(knockedOverCount);
        knockedOverCount = 0;

        bowlingPins.Clear();
        spawner.SpawnObject();
    }
}