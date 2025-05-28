using UnityEngine;

// 这个脚本应该放在Graphics子对象上（和Animator组件在一起）
public class DeathBringerAnimationTriggers : MonoBehaviour
{
    private Enemy_DeathBringer deathBringer;
    
    private void Start()
    {
        // 获取父对象上的Enemy_DeathBringer组件
        deathBringer = GetComponentInParent<Enemy_DeathBringer>();
        if (deathBringer == null)
        {
            Debug.LogError("DeathBringerAnimationTriggers: Cannot find Enemy_DeathBringer in parent!");
        }
    }
    
    // 通用的动画结束触发器
    private void AnimationFinishTrigger()
    {
        if (deathBringer != null)
            deathBringer.AnimationFinishTrigger();
    }
    
    // 为了兼容可能的不同命名，添加别名方法
    private void AnimationTrigger()
    {
        AnimationFinishTrigger();
    }
    
    // 攻击触发器 - 在攻击动画的伤害帧调用
    private void AttackTrigger()
    {
        if (deathBringer == null) return;
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(
            deathBringer.attackOccur.position, 
            deathBringer.attackOccurRedius,
            LayerMask.GetMask("Player")); // 确保你的玩家在Player层
        
        foreach (var hit in colliders)
        {
            Player player = hit.GetComponent<Player>();
            if (player != null)
            {
                CharacterStats playerStats = player.GetComponent<CharacterStats>();
                if (playerStats != null)
                {
                    deathBringer.stats.DoDamage(playerStats);
                }
            }
        }
    }
    
    // 传送相关的动画事件
    private void Relocate()
    {
        if (deathBringer != null)
            deathBringer.AnimationTriggerRelocate();
    }
    
    private void MakeInvisible()
    {
        if (deathBringer != null)
            deathBringer.AnimationTriggerMakeInvisible();
    }
    
    private void MakeVisible()
    {
        if (deathBringer != null)
            deathBringer.AnimationTriggerMakeVisible();
    }
    
    // 打开/关闭反击窗口
    private void OpenCounterWindow()
    {
        if (deathBringer != null)
            deathBringer.OpenCounterAttackWindow();
    }
    
    private void CloseCounterWindow()
    {
        if (deathBringer != null)
            deathBringer.CloseCounterAttackWindow();
    }
    
    // 调试用 - 如果动画事件仍然找不到方法，这会帮助诊断
    private void OnValidate()
    {
        if (Application.isPlaying)
        {
            Debug.Log("DeathBringerAnimationTriggers is attached to: " + gameObject.name);
        }
    }
}