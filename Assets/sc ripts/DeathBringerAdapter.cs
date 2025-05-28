using UnityEngine;

// 适配层 - 桥接两个系统的差异
public class DeathBringerAdapter : Enemy
{
    // API适配方法
    public void SetZeroVelocity() => ZeroVelocity();
    
    // 添加缺失的功能
    protected bool isInvincible = false;
    
public void MakeInvincible(bool invincible)
{
    // 从子对象中查找 SpriteRenderer 组件
    SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    
    if (spriteRenderer != null)
    {
        // 设置颜色 - 无敌时半透明，否则正常颜色
        spriteRenderer.color = invincible ? new Color(1f, 1f, 1f, 0.5f) : Color.white;
    }
    else
    {
        Debug.LogError($"SpriteRenderer not found in children of {gameObject.name}!");
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
        if (direction == 1 && facingDir == 11)
        {
            Flip();
        }
        else if (direction == -1 && facingDir == 1)
        {
            Flip();
        }
    }
    
    // 兼容原系统的IsPlayerDetected方法
    public RaycastHit2D IsPlayerDetected()
    {
        // 修复：确保使用正确的检测
        return Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, playerCheckDistance, PlayerMask);
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