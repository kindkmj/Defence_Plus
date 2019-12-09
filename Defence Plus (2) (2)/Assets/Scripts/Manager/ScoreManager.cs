using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : SingletonMonobehaviour<ScoreManager>
{
    private int monsterKillPoint = 100;
    private int puzzleReward = 100;
    private int questionCost = 50;

    public Text ScoreLabel;
    public Text PuzzleGoldLabel;

    private int score = 0;
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            ScoreLabel.text = "점수 : " + score.ToString();
        }
    }

    private int totalPuzzleGold = 0;
    public int TotalPuzzleGold
    {
        get { return totalPuzzleGold; }
        set
        {
            totalPuzzleGold = value;
        }
    }

    private int puzzleGold = 1000;
    public int PuzzleGold
    {
        get { return puzzleGold; }
        set
        {
            puzzleGold = value;
            PuzzleGoldLabel.text = puzzleGold.ToString();
        }
    }


    public void GivePuzzleCoin()
    {
        PuzzleGold += puzzleReward;
        TotalPuzzleGold += puzzleReward;
    }

    public void BuyUnit(int cost)
    {
        PuzzleGold -= cost;
    }

    public void BuyQuestion()
    {
        if (PuzzleGold - questionCost >= 0)
        {
            PuzzleGold -= questionCost;
            PuzzleManager.Instance.GiveNewQuestion();
        }
        else
        {
            Debug.Log("골드가 부족합니다.");
        }
    }

    public bool CanBuyUnit(int cost)
    {
        return PuzzleGold - cost >= 0 ? true : false;
    }

    public void GetMonsterKillPoint()
    {
        Score += monsterKillPoint;
    }
}
