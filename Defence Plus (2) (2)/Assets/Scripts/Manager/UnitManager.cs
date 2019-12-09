using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class UnitManager : SingletonMonobehaviour<UnitManager>
{
    private Dictionary<UnitKind, UnitStatus> unitStatuses = new Dictionary<UnitKind, UnitStatus>();
    public List<UnitCallButton> UnitCallButtons;

    //public List<UnitCallOption> UnitCallOptions = new List<UnitCallOption>();

    private int sortingOrder = 1000;

    //private Dictionary<UnitKind, Func<bool>> unitCallConditions = new Dictionary<UnitKind, Func<bool>>();

    public int HeroCallCount { get; set; } = 1;


    void Start ()
    {
        InitStatusSet();
        AddEventToButtons();
        //AddUnitCallConditions();
    }

    private void InitStatusSet()
    {
        unitStatuses.Add(UnitKind.Knight, new KnightStatus());
        unitStatuses.Add(UnitKind.Archer, new ArcherStatus());
        unitStatuses.Add(UnitKind.Hero, new HeroStatus());
    }

    void Update ()
    {
        CheckUnitCallConditions();
    }

    private void CheckUnitCallConditions()
    {
        for (int i = 0; i < UnitCallButtons.Count; i++)
        {
            UnitCallButtons[i].Button.interactable = unitStatuses[UnitCallButtons[i].UnitType].CallCondition();
        }
    }

    private void AddEventToButtons()
    {
        foreach (var unitCallButton in UnitCallButtons)
        {
            unitCallButton.Button.onClick.AddListener(unitStatuses[unitCallButton.UnitType].CallMethod);
        }
    }

    //private void AddUnitCallConditions()
    //{
    //    unitCallConditions.Add(UnitKind.Knight,
    //        () =>
    //        {
    //            return ScoreManager.Instance.CanBuyUnit(UnitCallOptions[(int)UnitKind.Knight].UnitCost);
    //        });
    //    unitCallConditions.Add(UnitKind.Archer,
    //        () =>
    //        {
    //            return ScoreManager.Instance.CanBuyUnit(UnitCallOptions[(int)UnitKind.Archer].UnitCost);
    //        });
    //    unitCallConditions.Add(UnitKind.Hero,
    //        () =>
    //        {
    //            return ScoreManager.Instance.CanBuyUnit(UnitCallOptions[(int)UnitKind.Hero].UnitCost);

    //            int totalCost = UnitCallOptions[(int)UnitKind.Hero].UnitCost;

    //            if (ScoreManager.Instance.TotalPuzzleGold / totalCost == TotalCheckAmount
    //                && ScoreManager.Instance.TotalPuzzleGold % totalCost == 0)
    //            {
    //                return true;
    //            }
    //            return false;
    //        });
    //}

    public void CreateUnit(UnitStatus unitStatus)
    {
        GameObject obj = Instantiate(unitStatus.UnitObject, this.transform);
        obj.transform.localPosition = unitStatus.UnitSpawnPos;

        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        sr.sortingOrder = sortingOrder++;

        Unit unit = obj.GetComponent<Unit>();
        unit.Status = unitStatus;
    }
}
