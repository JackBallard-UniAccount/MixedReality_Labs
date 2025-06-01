using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PinManager : MonoBehaviour
{
    [SerializeField] private OVRInput.RawButton _scoreCollect = OVRInput.RawButton.A;
    public TextMeshPro MaskDepthBiasText;
    private List<int> rolls = new List<int>();
    public List<GameObject> pins = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(_scoreCollect))
        {
            CollectScores();
        }
    }

    public void CollectScores()
    {
        //rolls.add(int)
        int score = 0;
        foreach (GameObject pin in pins)
        {
            PinDetection detection = pin.GetComponent<PinDetection>();
            if (detection != null && detection.Score())
            {
                score++;
            }
        }
        rolls.Add(score);
        score = GetScore();
        Debug.Log(score);
        MaskDepthBiasText.text = "Score " + score.ToString();
    }

    public int GetScore()
    {
        int score = 0;
        int rollIndex = 0;

        for (int frame = 0; frame < 10; frame++)
        {
            if (IsStrike(rollIndex)) // Strike
            {
                score += 10 + StrikeBonus(rollIndex);
                rollIndex += 1;
            }
            else if (IsSpare(rollIndex)) // Spare
            {
                score += 10 + SpareBonus(rollIndex);
                rollIndex += 2;
            }
            else // Open frame
            {
                score += SumOfBallsInFrame(rollIndex);
                rollIndex += 2;
            }
        }

        return score;
    }

    // Helpers
    private bool IsStrike(int rollIndex)
    {
        return rollIndex < rolls.Count && rolls[rollIndex] == 10;
    }

    private bool IsSpare(int rollIndex)
    {
        return rollIndex + 1 < rolls.Count &&
               rolls[rollIndex] + rolls[rollIndex + 1] == 10;
    }

    private int StrikeBonus(int rollIndex)
    {
        if (rollIndex + 2 < rolls.Count)
            return rolls[rollIndex + 1] + rolls[rollIndex + 2];
        return 0; // Incomplete bonus, return 0 or partial if desired
    }

    private int SpareBonus(int rollIndex)
    {
        if (rollIndex + 2 < rolls.Count)
            return rolls[rollIndex + 2];
        return 0;
    }

    private int SumOfBallsInFrame(int rollIndex)
    {
        if (rollIndex + 1 < rolls.Count)
            return rolls[rollIndex] + rolls[rollIndex + 1];
        else if (rollIndex < rolls.Count)
            return rolls[rollIndex];
        return 0;
    }

}
