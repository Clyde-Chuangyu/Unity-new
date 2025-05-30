using UnityEngine;

// DeathBringer专用的Stats类，处理无敌状态
public class DeathBringerStats : EnemyStats
{
    private DeathBringerAdapter deathBringer;

    protected override void Start()
    {
        base.Start();
        deathBringer = GetComponent<DeathBringerAdapter>();
    }

    public override void TakeDamage(int damage)
    {
        // 如果处于无敌状态，不受伤害
        if (deathBringer != null && deathBringer.IsInvincible())
        {
            // 可选：添加无敌时的反馈效果
            Debug.Log("DeathBringer is invincible!");
            return;
        }

        base.TakeDamage(damage);
    }
}