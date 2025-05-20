using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSecondJumpState : PlayerState
{
    public PlayerSecondJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.jumpTimes = 0;
        player.SetVelocity(xInput * player.moveSpeed, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);
        if (rb.velocity.y < 0)
            stateMachine.ChangeState(player.AirState);
        if (player.IsWallDetected())
            stateMachine.ChangeState(player.WallSlideState);
    }
}
