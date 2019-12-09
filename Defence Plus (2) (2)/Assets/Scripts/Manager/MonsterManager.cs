using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    //private List<int> intervals = new List<int>()
    //{
    //    10,1,0,0,1,12,0,12,12
    //};

    // 시간이 지날수록 점점 빨라진다.
    // 몇 초 : 몇 명
    
    //public List<MonsterStatus> MonsterStatuses = new List<MonsterStatus>();
    //public List<MonsterCallOption> MonsterCallOptions = new List<MonsterCallOption>();

    //public MonsterCallOption MonsterCallOption;

    private int sortingOrder = 0; 


    void Start()
    {
        Invoke("Create", 30f);
    }

    private void Create()
    {
        MonsterStatus MonsterStatus = new TrollStatus();

        GameObject obj = Instantiate(MonsterStatus.MonsterObject, this.transform);
        obj.transform.localPosition = MonsterStatus.MonsterSpawnPos;

        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        sr.sortingOrder = sortingOrder++;

        Monster mon = obj.GetComponent<Monster>();
        mon.Status = MonsterStatus;

        Invoke("Create", 4f);
    }
}
