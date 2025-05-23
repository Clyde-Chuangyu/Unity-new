using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 0.25f;
        player.SetVelocity(-player.facingDir * player.moveSpeed, player.jumpForce);
        
        // 播放墙跳音效
        AudioManager.Instance?.PlayWallJumpSound();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);
        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.IdleState);
        if (player.IsWallDetected() && xInput == player.facingDir)
            stateMachine.ChangeState(player.WallSlideState);
    }
}
