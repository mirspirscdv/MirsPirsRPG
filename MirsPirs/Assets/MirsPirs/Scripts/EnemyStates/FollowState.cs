﻿using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Text;

class FollowState : IState
{
    private Enemy parent;

    public void Exit()
    {
        parent.Direction = Vector2.zero;
    }

    public void Enter(Enemy parent)
    {
        this.parent = parent;
    }

    public void Update()
    {
        Debug.Log("Follow");
        if (parent.MyTarget != null)
        {
            parent.Direction = (parent.MyTarget.transform.position - parent.transform.position).normalized;

            parent.transform.position = Vector2.MoveTowards(parent.transform.position, parent.MyTarget.position, parent.speed * Time.deltaTime);

            float distance = Vector2.Distance(parent.MyTarget.position, parent.transform.position);

            if (distance <= parent.MyAttackRange)
            {
                parent.ChangeState(new AttackState());
            }
        }
        if(!parent.InRange)
        {
            parent.ChangeState(new IdleState());
        }
    }
}