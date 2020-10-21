using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberWizard : MonoBehaviour
{

    [SerializeField] int max;
    [SerializeField] int min;
    [SerializeField] TextMeshProUGUI guessText;
    [SerializeField] TextMeshProUGUI guessCountText;
    int guess;
    int guessCount;

    // Use this for initialization
    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        guess = (max + min) / 2;
        max = max + 1;
        guessCount = 10;
        guessCountText.text = guessCount.ToString();
        guessText.text = guess.ToString();
    }

    public void OnPressHigher()
    {
        min = guess;
        NextGuess();
    }

    public void OnPressLower()
    {
        max = guess;
        NextGuess();
    }

    void NextGuess()
    {
        guess = (max + min) / 2;
        guessCount--;
        guessCountText.text = guessCount.ToString();
        guessText.text = guess.ToString();
    }
}
