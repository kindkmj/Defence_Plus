using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


//[Serializable]
public interface UnitStatus
{
    UnitKind UnitKind { get; }
    GameObject UnitObject { get; }

    int HP { get; set; }
    int ATK { get; }
    float Speed { get; }

    Vector2 UnitSpawnPos { get; }
    float SearchDistance { get; }
    Vector2 UnitGoalPos { get; }

    int UnitCost { get; }

    void CallMethod();
    bool CallCondition();
}

//public interface UnitCallOption
//{
//    UnitKind UnitKind { get; }
//    int UnitCost { get; }

//    Button UnitCallButton { get; set; }
//    UnityEvent ButtonEvent { get; }

//    bool CallCondition();
//}

//[Serializable]
public interface MonsterStatus
{
    MonsterKind MonsterKind { get; }
    GameObject MonsterObject { get; }

    int HP { get; set; }
    int ATK { get; }
    float Speed { get; }

    Vector2 MonsterSpawnPos { get; }
    float SearchDistance { get; }

    int KillPoint { get; }
}
