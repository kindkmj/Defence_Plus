using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class Monster : MonoBehaviour
{
    public MonsterStatus Status;

    public MonsterState _state = MonsterState.None;
    private Animator anim;
    private Unit target;
    private Transform Goal;

    private bool _dieFlag = false;


    void Start()
    {
        anim = GetComponent<Animator>();
        Goal = GameObject.Find("Castle").transform;
        SetState(MonsterState.Walk);
    }

    void Update()
    {
        if (Status != null)
        {
            CheckHP();
            Targeting();
            MoveControl();
        }
    }

    private void CheckHP()
    {
        if (Status.HP <= 0)
        {
            SetState(MonsterState.Die);
        }

        if (_state == MonsterState.Die)
        {
            if (!_dieFlag)
            {
                _dieFlag = true;
                ScoreManager.Instance.GetMonsterKillPoint();
                transform.gameObject.layer = 0;
                Destroy(this.gameObject, 1f);
            }
        }
    }

    private void Targeting()
    {
        SearchTarget();

        if (_state == MonsterState.Walk)
        {
            if (target != null)
                SetState(MonsterState.Attack);
        }
        else if (_state == MonsterState.Attack)
        {
            if (target == null)
                SetState(MonsterState.Walk);
        }
    }

    private Ray2D ray;
    private RaycastHit2D hit;
    private Vector2 moveDirection = Vector2.left;
    private void SearchTarget()
    {
        ray = new Ray2D(transform.position, transform.TransformDirection(moveDirection));
        hit = Physics2D.Raycast(transform.position, ray.direction, Status.SearchDistance, 1 << LayerMask.NameToLayer("Unit"));

        if (hit.transform != null)
            target = hit.transform.gameObject.GetComponent<Unit>();
        else
            target = null;

        Debug.DrawRay(transform.position, ray.direction * Status.SearchDistance, Color.red);
    }

    private void MoveControl()
    {
        if (_state == MonsterState.Walk)
        {
            Vector2 dir = Goal.transform.position - transform.position;
            dir.Normalize();

            transform.Translate(dir * Status.Speed * Time.deltaTime);
        }
    }

    #region Animation Event
    public void AttackEvent()
    {
        if (target != null)
        {
            target.Status.HP -= Status.ATK;
        }
    } 
    #endregion

    public void SetState(MonsterState state)
    {
        if (_state != state)
        {
            _state = state;
            anim.SetTrigger(_state.ToString());
        }
    }
}
