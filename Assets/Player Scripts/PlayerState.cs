using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected Rigidbody2D rb;
    private string animBoolName;

    protected float xInput;
    protected float yInput;
    protected float stateTimer;
    protected bool triggerCalled;

    //这个函数的作用是让每一个状态都能获取到玩家和状态机的实例，然后为每个状态的动画条件名赋值，
    //方便状态改变玩家的属性、被状态机转化及开关动画播放
    public PlayerState(Player _player,PlayerStateMachine _stateMachine,string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        player.animator.SetBool(animBoolName, true);
        rb = player.rb;
        triggerCalled = false;
    }
    public virtual void Exit()
    {
        player.animator.SetBool(animBoolName,false);
    }
    public virtual void Update()
    {
        
        stateTimer -= Time.deltaTime;
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        player.animator.SetFloat("y.velocity", rb.velocity.y);

    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
