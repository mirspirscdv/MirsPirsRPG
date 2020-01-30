using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC
{
    [SerializeField]
    private CanvasGroup healthGroup;

    private IState currentState;

    [SerializeField]
    private int damage;
    public float MyAttackRange
    {
        get;
        set;
    }

    public float MyAttackTime
    {
        get;
        set;
    }

    [SerializeField]
    private float initAggroRange;

    public float MyAggroRange
    {
        get;
        set;
    }

    public bool InRange
    {
        get
        {
            return Vector2.Distance(transform.position, MyTarget.position) < MyAggroRange;
        }
    }

    protected void Awake()
    {
        MyAggroRange = initAggroRange;
        MyAttackRange = 0.5f;
        ChangeState(new IdleState());
    }

  

    protected override void Update()
    {
        if(IsAlive)
        {
            if (!IsAttacking)
            {
                MyAttackTime += Time.deltaTime;
            }
            currentState.Update();
        }
        base.Update();
    }

    public override Transform Select()
    {
        healthGroup.alpha = 1;
        return base.Select();
    }
    public override void DeSelect()
    {
        healthGroup.alpha = 0;
        base.DeSelect();
    }
    public override void TakeDamage(float damage, Transform source)
    {
        SetTarget(source);
        base.TakeDamage(damage, source);
        OnHealthChanged(health.MyCurrentValue);
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;

        currentState.Enter(this);
    }

    public void DoDamage()
    {
        
    }
    public void SetTarget(Transform target)
    {
        if (MyTarget == null)
        {
            float distance = Vector2.Distance(transform.position, target.position);
            MyAggroRange = initAggroRange;
            MyAggroRange += distance;
            MyTarget = target;
        }
    }
    public void Reset()
    {
        this.MyTarget = null;
        this.MyAggroRange = initAggroRange;
        this.MyHealth.MyCurrentValue = this.MyHealth.MyMaxValue;
        OnHealthChanged(health.MyCurrentValue);
    }

}
