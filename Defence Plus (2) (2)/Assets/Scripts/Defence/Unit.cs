using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Unit : MonoBehaviour 
{
    public UnitStatus Status;
    
    public UnitState _state = UnitState.None;
    private Animator anim;
    private Monster target;
    
    private bool _dieFlag = false;


    void Start()
    {
        anim = GetComponent<Animator>();
        SetState(UnitState.Walk);
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
            SetState(UnitState.Die);
        }

        if (_state == UnitState.Die)
        {
            if (!_dieFlag)
            {
                _dieFlag = true;
                transform.gameObject.layer = 0;
                Destroy(this.gameObject, 1f);
            }
        }
    }

    private void Targeting()
    {
        SearchTarget();

        if (_state == UnitState.Idle || _state == UnitState.Walk)
        {
            if (target != null)
                SetState(UnitState.Attack);
        }
        else if (_state == UnitState.Attack)
        {
            if (target == null)
                SetState(UnitState.Idle);
        }
    }

    private Ray2D ray;
    private RaycastHit2D hit;
    private Vector2 moveDirection = Vector2.right;
    private void SearchTarget()
    {
        ray = new Ray2D(transform.position, transform.TransformDirection(moveDirection));
        hit = Physics2D.Raycast(transform.position, ray.direction, Status.SearchDistance, 1 << LayerMask.NameToLayer("Monster"));

        if (hit.transform != null)
            target = hit.transform.gameObject.GetComponent<Monster>();
        else
            target = null;

        Debug.DrawRay(transform.position, ray.direction * Status.SearchDistance, Color.red);
    }

    private void MoveControl()
    {
        if (_state == UnitState.Idle || _state == UnitState.Walk)
        {
            Vector2 dir = Status.UnitGoalPos - (Vector2)transform.position;
            dir.Normalize();

            if (Vector2.Distance(transform.position, Status.UnitGoalPos) < 0.1f)
                SetState(UnitState.Idle);
            else
                SetState(UnitState.Walk);


            if (_state == UnitState.Walk)
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

    public void SetState(UnitState state)
    {
        if (_state != state)
        {
            _state = state;
            anim.SetTrigger(_state.ToString());
        }
    }
}
