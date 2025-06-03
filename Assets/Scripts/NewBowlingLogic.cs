using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class BowlingGame : MonoBehaviour
{
    [SerializeField] public NewPinManager pinManager;
    private List<int> rolls = new List<int>();


    public void Roll(int pins)
    {
        if (pins < 0 || pins > 10)
            throw new ArgumentException("Pins must be between 0 and 10.");

        rolls.Add(pins);
    }

    public string GetScoreDisplay()
    {
        StringBuilder framesLine = new StringBuilder();
        StringBuilder scoresLine = new StringBuilder();

        int rollIndex = 0;
        int cumulativeScore = 0;

        for (int frame = 0; frame < 10; frame++)
        {
            if (rollIndex >= rolls.Count)
            {
                framesLine.Append("0-0 ".PadRight(5));
                scoresLine.Append($"{cumulativeScore}".PadRight(5));
                continue;
            }

            if (IsStrike(rollIndex))
            {
                framesLine.Append("X-  ".PadRight(5));
                if (rollIndex + 2 < rolls.Count)
                {
                    cumulativeScore += 10 + rolls[rollIndex + 1] + rolls[rollIndex + 2];
                }
                scoresLine.Append($"{cumulativeScore}".PadRight(5));
                rollIndex += 1;
                pinManager.ResetAllPins();
            }
            else if (IsSpare(rollIndex))
            {
                framesLine.Append($"{rolls[rollIndex]}-/ ".PadRight(5));
                if (rollIndex + 2 < rolls.Count)
                {
                    cumulativeScore += 10 + rolls[rollIndex + 2];
                }
                scoresLine.Append($"{cumulativeScore}".PadRight(5));
                rollIndex += 2;
                pinManager.ResetAllPins();
            }
            else
            {
                if (rollIndex + 1 >= rolls.Count)
                {
                    framesLine.Append($"{rolls[rollIndex]}-0 ".PadRight(5));
                    cumulativeScore += rolls[rollIndex];
                    scoresLine.Append($"{cumulativeScore}".PadRight(5));
                    rollIndex += 2; // <- Prevent re-accessing incomplete frame
                    continue;       // <- Skip the rest of the logic
                }

                int sum = rolls[rollIndex] + rolls[rollIndex + 1];
                framesLine.Append($"{rolls[rollIndex]}-{rolls[rollIndex + 1]} ".PadRight(5));
                cumulativeScore += sum;
                scoresLine.Append($"{cumulativeScore}".PadRight(5));
                rollIndex += 2;
                pinManager.ResetAllPins();
            }
        }

        return framesLine.ToString().TrimEnd() + "\n" + scoresLine.ToString().TrimEnd();
    }

    private bool IsStrike(int rollIndex)
    {
        return rollIndex < rolls.Count && rolls[rollIndex] == 10;
    }

    private bool IsSpare(int rollIndex)
    {
        return rollIndex + 1 < rolls.Count &&
               rolls[rollIndex] + rolls[rollIndex + 1] == 10;
    }
}
/*
public class Program
{
    public static void Main(string[] args)
    {
        BowlingGame game = new BowlingGame();

        Console.WriteLine("Enter your rolls as space-separated integers (e.g., 10 3 7 6 1 ...):");
        Console.Write("> ");
        string input = Console.ReadLine();
        string[] tokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        foreach (var token in tokens)
        {
            if (int.TryParse(token, out int pins))
            {
                try
                {
                    game.Roll(pins);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Invalid roll '{pins}': {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Invalid input: {token} is not a number.");
            }
        }

        Console.WriteLine("\nSCORE DISPLAY:");
        Console.WriteLine(game.GetScoreDisplay());
    }

}*/