using UnityEngine;

// 空闲状态
public class DeathBringerIdleState : EnemyState
{
    private Enemy_DeathBringer enemy;
    private Transform player;

    public DeathBringerIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_DeathBringer enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.idleTime;
        player = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (Vector2.Distance(player.transform.position, enemy.transform.position) < 7)
            enemy.bossFightBegun = true;

        // 调试用 - 移除这行
        // if (Input.GetKeyDown(KeyCode.V))
        //     stateMachine.ChangeState(enemy.teleportState);

        if (stateTimer < 0 && enemy.bossFightBegun)
            stateMachine.ChangeState(enemy.battleState);
    }
}

// 战斗状态
public class DeathBringerBattleState : EnemyState
{
    private Enemy_DeathBringer enemy;
    private Transform player;
    private int moveDir;

    public DeathBringerBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_DeathBringer _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;
        
        // 添加调试信息
        Debug.Log("DeathBringer entered Battle State");
    }

    public override void Update()
    {
        base.Update();

        var playerDetected = enemy.IsPlayerDetected();
        
        if (playerDetected)
        {
            stateTimer = enemy.battleTime;

            // 检查是否在攻击范围内
            if (playerDetected.distance < enemy.attackCheckDistance)
            {
                Debug.Log($"Player in attack range: {playerDetected.distance} < {enemy.attackCheckDistance}");
                
                if (CanAttack())
                {
                    Debug.Log("Changing to Attack State");
                    stateMachine.ChangeState(enemy.attackState);
                    return;
                }
                else
                {
                    Debug.Log("Cannot attack yet, going to Idle");
                    stateMachine.ChangeState(enemy.idleState);
                    return;
                }
            }
        }

        // 决定移动方向
        if (player.position.x > enemy.transform.position.x)
            moveDir = 1;
        else if (player.position.x < enemy.transform.position.x)
            moveDir = -1;

        // 如果已经很近了就不要移动
        if (playerDetected && playerDetected.distance < enemy.attackCheckDistance - .1f)
            return;

        // 移动
        enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("DeathBringer exited Battle State");
    }

    private bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttack + enemy.attackCD)
        {
            enemy.lastTimeAttack = Time.time;
            return true;
        }
        return false;
    }
}

// 攻击状态
public class DeathBringerAttackState : EnemyState
{
    private Enemy_DeathBringer enemy;

    public DeathBringerAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_DeathBringer _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.chanceToTeleport += 5;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.lastTimeAttack = Time.time; // 使用lastTimeAttack
    }

    public override void Update()
    {
        base.Update();
        enemy.SetZeroVelocity();

        if (triggerCalled)
        {
            if (enemy.CanTeleport())
                stateMachine.ChangeState(enemy.teleportState);
            else
                stateMachine.ChangeState(enemy.battleState);
        }
    }
}

// 死亡状态
public class DeathBringerDeadState : EnemyState
{
    private Enemy_DeathBringer enemy;

    public DeathBringerDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_DeathBringer enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.animator.SetBool(enemy.lastAnimBoolName, true); // 使用animator代替anim
        enemy.animator.speed = 0;
        enemy.capsule.enabled = false; // 使用capsule代替cd

        stateTimer = .15f;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer > 0)
            rb.velocity = new Vector2(0, 10);
    }
}

// 传送状态
public class DeathBringerTeleportState : EnemyState
{
    private Enemy_DeathBringer enemy;

    public DeathBringerTeleportState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_DeathBringer _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.stats.MakeInvincible(true);
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            if (enemy.CanDoSpellCast())
                stateMachine.ChangeState(enemy.spellCastState);
            else
                stateMachine.ChangeState(enemy.battleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        enemy.stats.MakeInvincible(false);
    }
}

// 施法状态
public class DeathBringerSpellCastState : EnemyState
{
    private Enemy_DeathBringer enemy;
    private int amountOfSpells;
    private float spellTimer;

    public DeathBringerSpellCastState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_DeathBringer _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        amountOfSpells = enemy.amountOfSpells;
        spellTimer = .5f;
    }

    public override void Update()
    {
        base.Update();
        spellTimer -= Time.deltaTime;

        if (CanCast())
            enemy.CastSpell();

        if (amountOfSpells <= 0)
            stateMachine.ChangeState(enemy.teleportState);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.lastTimeCast = Time.time;
    }

    private bool CanCast()
    {
        if (amountOfSpells > 0 && spellTimer < 0)
        {
            amountOfSpells--;
            spellTimer = enemy.spellCooldown;
            return true;
        }
        return false;
    }
}