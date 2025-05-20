using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        player.jumpTimes = 1;
        player.haveDashed = false;
    }

    public override void Exit()
    {
        base.Exit();
       
    }

    public override void Update()
    {
        base.Update();
        rb.velocity=new Vector2(0,0.75f*rb.velocity.y);
        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.IdleState);
        if(xInput==0)
            stateMachine.ChangeState(player.IdleState);
        if(!player.IsWallDetected())
            stateMachine.ChangeState(player.IdleState);
        if(Input.GetKeyDown(KeyCode.Space))
            stateMachine.ChangeState(player.WallJumpState); 
    }
}
