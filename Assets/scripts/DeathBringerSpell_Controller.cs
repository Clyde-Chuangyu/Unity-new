using UnityEngine;

public class DeathBringerSpell_Controller : MonoBehaviour
{
    [SerializeField] private Transform check;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private LayerMask whatIsPlayer;

    [Header("Auto Destroy Settings")]
    [SerializeField] private float lifeTime = 3f; // 添加生命周期，防止法术永远不消失

    private CharacterStats myStats;
    private bool hasTriggered = false; // 防止重复触发

    private void Start()
    {
        // 添加安全销毁机制，防止法术永远不消失
        Destroy(gameObject, lifeTime);

        // 调试信息
        Debug.Log($"DeathBringer spell created. LifeTime: {lifeTime}s");
    }

    public void SetupSpell(CharacterStats _stats) => myStats = _stats;

    // 这个方法应该被动画事件调用
    private void AnimationTrigger()
    {
        if (hasTriggered) return; // 防止重复触发
        hasTriggered = true;

        Debug.Log("Spell AnimationTrigger called!");

        // 如果没有设置检测层，使用默认的Player层
        LayerMask playerLayer = whatIsPlayer;
        if (playerLayer.value == 0)
        {
            playerLayer = LayerMask.GetMask("Player");
            Debug.LogWarning("whatIsPlayer LayerMask not set, using default 'Player' layer");
        }

        Collider2D[] colliders = Physics2D.OverlapBoxAll(check.position, boxSize, 0, playerLayer);
        Debug.Log($"Found {colliders.Length} colliders in spell range");

        foreach (var hit in colliders)
        {
            Player player = hit.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("Hit player with spell!");

                // 设置击退方向
                Vector2 knockbackDir = (hit.transform.position - transform.position).normalized;
                player.rb.velocity = new Vector2(knockbackDir.x * 10, 12); // 直接设置击退

                // 造成伤害
                CharacterStats targetStats = hit.GetComponent<CharacterStats>();
                if (targetStats != null && myStats != null)
                {
                    Debug.Log("Dealing spell damage to player");
                    myStats.DoDamage(targetStats);
                }
                else
                {
                    Debug.LogError("Cannot deal damage - missing stats component");
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (check != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(check.position, boxSize);
        }
    }

    // 动画事件调用此方法来销毁法术
    private void SelfDestroy()
    {
        Debug.Log("Spell self-destroying");
        Destroy(gameObject);
    }

    // 添加备用的销毁方法，以防动画事件失效
    public void ForceDestroy()
    {
        SelfDestroy();
    }

    // 如果动画事件调用失败，可以通过延迟调用来销毁
    private void DestroyAfterAnimation()
    {
        // 延迟销毁，给动画足够时间播放
        Invoke(nameof(SelfDestroy), 1f);
    }
    private void Update()
{
    // 临时测试 - 按T键手动触发伤害
    if (Input.GetKeyDown(KeyCode.T))
    {
        Debug.Log("Manual trigger test!");
        AnimationTrigger();
    }
}
}