using UnityEngine;

public class DeathBringerSpell_Controller : MonoBehaviour
{
    [SerializeField] private Transform check;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private LayerMask whatIsPlayer;

    private CharacterStats myStats;

    public void SetupSpell(CharacterStats _stats) => myStats = _stats;

    // 这个方法应该被动画事件调用
    private void AnimationTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(check.position, boxSize, 0, whatIsPlayer);

        foreach (var hit in colliders)
        {
            Player player = hit.GetComponent<Player>();
            if (player != null)
            {
                // 设置击退方向
                Vector2 knockbackDir = (hit.transform.position - transform.position).normalized;
                player.rb.velocity = new Vector2(knockbackDir.x * 10, 12); // 直接设置击退
                
                // 造成伤害
                CharacterStats targetStats = hit.GetComponent<CharacterStats>();
                if (targetStats != null)
                {
                    myStats.DoDamage(targetStats);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (check != null)
            Gizmos.DrawWireCube(check.position, boxSize);
    }

    // 动画事件调用此方法来销毁法术
    private void SelfDestroy() => Destroy(gameObject);
}