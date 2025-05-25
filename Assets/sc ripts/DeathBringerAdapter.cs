using UnityEngine;

// 适配层 - 桥接两个系统的差异
public class DeathBringerAdapter : Enemy
{
    // API适配方法
    public void SetZeroVelocity() => ZeroVelocity();
    
    // 添加缺失的功能
    protected bool isInvincible = false;
    
    public virtual void MakeInvincible(bool invincible)
    {
        isInvincible = invincible;
        // 可选：添加视觉反馈
        if (invincible)
        {
            // 例如改变透明度或颜色
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
    
    // 获取无敌状态
    public bool IsInvincible()
    {
        return isInvincible;
    }
    
    // 设置默认朝向
    public void SetupDefailtFacingDir(int direction)
    {
        // 由于facingDir的setter是private的，我们通过Flip来设置朝向
        if (direction == -1 && facingDir == 1)
        {
            Flip();
        }
        else if (direction == 1 && facingDir == -1)
        {
            Flip();
        }
    }
    
    // 兼容原系统的IsPlayerDetected方法
    public RaycastHit2D IsPlayerDetected()
    {
        return isPlayerDetected();
    }
    
    // 为传送功能添加的辅助方法
    public void MakeTransparent(bool transparent)
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (transparent)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0);
        }
        else
        {
            spriteRenderer.color = new Color(1, 1, 1, 1);
        }
    }
}

// 用于替代原有CharacterStats功能的扩展
public static class CharacterStatsExtensions
{
    public static void MakeInvincible(this CharacterStats stats, bool invincible)
    {
        var adapter = stats.GetComponent<DeathBringerAdapter>();
        if (adapter != null)
        {
            adapter.MakeInvincible(invincible);
        }
    }
}