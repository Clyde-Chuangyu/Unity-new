using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemyBase;
    protected Rigidbody2D rb;

    protected bool triggerCalled;
    protected float stateTimer;

    private string animBoolName;

    public EnemyState(Enemy _enemyBase, EnemyStateMachine _stateMachine,string _animBoolName)
    {
        this.enemyBase= _enemyBase;
        this.stateMachine= _stateMachine;
        this.animBoolName= _animBoolName;  
    }
    public virtual void Enter() {
        rb = enemyBase.rb;
        triggerCalled = false;
        enemyBase.animator.SetBool(animBoolName,true);
        
    }
    public virtual void Update()
    {
        stateTimer-= Time.deltaTime;
    }
    public virtual void Exit()
    {
        enemyBase.animator.SetBool(animBoolName, false);
        enemyBase.AssignLastAnimName(animBoolName);
    }

    public void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }

}
