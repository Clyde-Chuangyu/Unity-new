using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    private bool wasInAir = false;

    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (player.IsGroundDetected())
        {
            // 如果之前在空中，播放落地音效
            if (wasInAir)
            {
                AudioManager.Instance?.PlayLandSound();
                wasInAir = false;
            }
            
            player.jumpTimes = 1;
            player.haveDashed = false;
        }
    }

    public override void Exit()
    {
        base.Exit();
        // 标记离开地面
        if (!player.IsGroundDetected())
        {
            wasInAir = true;
        }
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
            stateMachine.ChangeState(player.JumpState);
        if (rb.velocity.y < 0)
        {
            wasInAir = true;
            stateMachine.ChangeState(player.AirState);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
            stateMachine.ChangeState(player.PrimaryAttack);
        if (Input.GetKeyDown(KeyCode.F))
            stateMachine.ChangeState(player.CounterAttack);
        if (player.canUseSkill)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) && HasNoSword())
                stateMachine.ChangeState(player.aimSword);
        }
    }

    private bool HasNoSword()
    {
        if (!player.sword)
        {
            return true;
        }
        player.sword.GetComponent<Sword_Skill_Controller>().ReturnSword();
        return false;
    }
}