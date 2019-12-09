using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class PuzzleManager : SingletonMonobehaviour<PuzzleManager>
{
    private const int MAX_ROW = 5;
    private const int MAX_COL = 5;
    private const int MIN_QUESTION_NUM = 3;  //문제의 최소치
    private const int MAX_QUESTION_NUM = 8; //문제의 최대치

    public GameObject Puzzle;

    public Transform PuzzlePanel;
    public Text QuestionText;

    public List<int> Percentages = new List<int>();

    public List<GameObject> Particles = new List<GameObject>();
    public List<Sprite> ButtonImages;
    public List<Material> PopMaterials = new List<Material>();

    public PuzzleManagerState _state = PuzzleManagerState.Waiting;

    private int question = 0;
    public int Question
    {
        get { return question; }
        set
        {
            question = value;
            QuestionText.text = question.ToString();
        }
    }

    private List<Puzzle> activePuzzles = new List<Puzzle>();
    public List<Puzzle> selectedPuzzles = new List<Puzzle>();

    //없어진 퍼즐 보강
    private List<Puzzle> removedRows = new List<Puzzle>();


    void Start ()
    {
        InitPuzzleGame();
    }

    private void InitPuzzleGame()
    {
        for (int i = 1; i <= MAX_ROW; i++)
        {
            for (int j = 1; j <= MAX_COL; j++)
            {
                CreatePuzzle(j, i);
            }
        }

        GiveNewQuestion();
    }

    private void CreatePuzzle(int x, int y)
    {
        GameObject button = Instantiate(Puzzle, PuzzlePanel);

        Puzzle pzl = button.GetComponent<Puzzle>();
        pzl.x = x;
        pzl.y = y;
        int number = CreateByPercentages();
        pzl.puzzleImage = ButtonImages[number - 1];
        pzl.number = number;
        pzl.materials.Add(PopMaterials[number - 1]);

        activePuzzles.Add(pzl);
    }
    
    //확률에 따라 숫자 선택
    private int CreateByPercentages()
    {
        int max = Percentages.Sum(x => x);
        int number = 0;
        int num = Random.Range(1, max+1);

        for (int i = 0; i < Percentages.Count; i++)
        {
            number += Percentages[i];
            if (num <= number)
            {
                return i+1;
            }
        }

        return -1;
    }

    #region Puzzle Trigger Method
    public void AddToAnswer(Puzzle pzl)
    {
        selectedPuzzles.Add(pzl);
        CheckAnswer();
    } 
    #endregion

    private void CheckAnswer()
    {
        int sum = 0;

        foreach (Puzzle pzl in selectedPuzzles)
        {
            sum += pzl.number;
        }

        //정답을 맞췄을 경우
        if (sum == Question)
        {
            RemovePuzzles();
            GiveNewQuestion();
            ScoreManager.Instance.GivePuzzleCoin();
            _state = PuzzleManagerState.BoardIsChanging;
        }
        else if (sum > Question)
        {
            CancleAnswer();
        }     
    }

    public void GiveNewQuestion()
    {
        Question = Random.Range(MIN_QUESTION_NUM, MAX_QUESTION_NUM+1);
    }
    
    private void RemovePuzzles()
    {
        for (int i = selectedPuzzles.Count-1; i >= 0; i--)
        {
            Puzzle pzl = selectedPuzzles[i];

            removedRows.Add(pzl);

            selectedPuzzles.Remove(pzl);
            activePuzzles.Remove(pzl);

            pzl.SetPuzzle(PuzzleState.Pop);
            Destroy(pzl.gameObject, 0.5f);
            Instantiate(Particles[pzl.number - 1], pzl.transform);
        }

        StartCoroutine("ShiftPuzzleDown");
    }

    IEnumerator ShiftPuzzleDown()
    {
        yield return new WaitForSeconds(0.5f);

        float delayTime = 0.07f;

        //linq를 통해 지워진 퍼즐을 x그룹으로 묶어 받아옴
        var query = (from x in removedRows
            group x by x.x
            into g
            select new
            {
                key = g.Key,
                Count = g.Count(),
                MaxValue = g.OrderByDescending(gx => gx.y).First().y
            });

        //x좌표마다 지워진 퍼즐들 중 가장 밑에 위치한 퍼즐
        Dictionary<int, int> removedPoses = query.ToDictionary(g => g.key, g => g.MaxValue);
        //x좌표마다 지워진 퍼즐의 양
        Dictionary<int, int> removedAmounts = query.ToDictionary(g => g.key, g => g.Count);

        for (int i = 1; i <= activePuzzles.Count; i++)
        {
            if (!removedAmounts.ContainsKey(i)) continue;

            for (int j = 0; j < removedAmounts[i]; j++)
                CreatePuzzle(i, 0 - j);
        }

        int max_amount = removedAmounts.Max(x => x.Value);
         
        var query2 = from x in activePuzzles
            where removedPoses.ContainsKey(x.x)
            select x;

        List<Puzzle> upPuzzles = query2.Where(x => x.y < removedPoses[x.x]).ToList();
        

        for (int i = 1; i <= max_amount; i++)
        {
            yield return new WaitForSeconds(delayTime);
            foreach (Puzzle pzl in upPuzzles)
            {
                if (removedAmounts[pzl.x] >= i)
                {
                    if (PuzzleCanMoveToDown(pzl))
                    {
                        pzl.y++;
                        pzl.SetPositionByPos();
                    }
                }
            }
        }

        removedRows.Clear();
        _state = PuzzleManagerState.Waiting;
    }

    private bool PuzzleCanMoveToDown(Puzzle pzl)
    {
        if (pzl.y == 5) return false;

        int i = 1;
        while (pzl.y + i <= 5)
        {
            var query = from x in activePuzzles
                where x.x == pzl.x & x.y == pzl.y + i
                select x;

            if (query.Count() == 0)
                return true;

            i++;
        }

        return false;
    }


    //selectedPuzzles 안에 있는 리스트 클리어시킴
    public void CancleAnswer()
    {
        _state = PuzzleManagerState.Waiting;

        foreach (var pzl in selectedPuzzles)
        {
           pzl.SetPuzzle(PuzzleState.Normal);
        }
        selectedPuzzles.Clear();
    }

    public void SetState(PuzzleManagerState state)
    {
        _state = state;
    }

    
    #region Mouse Event
    void OnMouseExit()
    {
        CancleAnswer();
    } 
    #endregion
}
