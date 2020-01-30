﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class Character : MonoBehaviour
{
    [SerializeField]
    protected float Speed;
    public Animator MyAnimator { get; set; }

    private Vector2 direction;

    private Rigidbody2D myRigidbody;

    public bool IsAttacking { get; set; }

    protected Coroutine attackRoutine;

    [SerializeField]
    protected Transform hitBox;

    [SerializeField] 
    protected Stat health;

    public Transform MyTarget
    {
        get;

        set;
    }

    public Stat MyHealth
    {
        get { return health; }
    }

    [SerializeField]
    private float initHealth;

    public bool IsMoving
    {
        get
        {
            return direction.x != 0 || direction.y != 0;
        }
    }

    public Vector2 Direction
    {
        get 
        {
            return direction;
        }

        set
        {
            direction = value;
        }
    }

    public float speed
    {
        get
        {
            return Speed;
        }

        set
        {
            Speed = value;
        }
    }

    public bool IsAlive
    {
        get 
        {
            return health.MyCurrentValue > 0;
        }
    }

    protected virtual void Start()
    {
        health.Initialize(initHealth, initHealth);
        myRigidbody = GetComponent<Rigidbody2D>();
        MyAnimator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        HandleLayers();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        if (IsAlive)
        {
            myRigidbody.velocity = direction.normalized * Speed;
        }
    }

    public void HandleLayers()
    {
        if (IsAlive)
        {
            if (IsMoving)
            {
                ActivateLayer("Walk");
                MyAnimator.SetFloat("x", direction.x);
                MyAnimator.SetFloat("y", direction.y);
                MyAnimator.SetLayerWeight(0, 0);

            }
            else if (IsAttacking)
            {
                ActivateLayer("AttackLayer");
            }
            else
            {
                ActivateLayer("Stay");
            }
        }
        else
        {
            ActivateLayer("DeathLayer");
        }
    }


    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < MyAnimator.layerCount; i++)
        {
            MyAnimator.SetLayerWeight(i, 0);
        }
        MyAnimator.SetLayerWeight(MyAnimator.GetLayerIndex(layerName), 1);
    }


    public virtual void TakeDamage(float damage, Transform source)
    {
        health.MyCurrentValue -= damage;

        if (health.MyCurrentValue <= 0)
        {
            Direction = Vector2.zero;
            myRigidbody.velocity = Direction;
            MyAnimator.SetTrigger("die");
        }
    }
}
