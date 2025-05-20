using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEditor;
using UnityEngine;

public class Slime : Enemy
{
    [SerializeField]public float jumpForce;
    #region States
    public SlimeIdleState idleState { get; private set; }
    public SlimeJumpState jumpState { get; private set; }
    public SlimeStunnedState stunnedState { get; private set; }
    public SlimeDeadState deadState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        idleState = new SlimeIdleState(this, stateMachine, "Idle", this);
        jumpState = new SlimeJumpState(this, stateMachine, "Jump", this);
        stunnedState = new SlimeStunnedState(this, stateMachine, "Stunned", this);
        deadState = new SlimeDeadState(this, stateMachine, "Jump", this);
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialized(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            stateMachine.ChangeState(stunnedState);
            return true;
        }
        return false;
    }
    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }
}
