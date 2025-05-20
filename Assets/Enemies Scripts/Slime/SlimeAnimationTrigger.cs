using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAnimationTrigger : MonoBehaviour
{
    private Slime slime => GetComponentInParent<Slime>();
    private void AnimationTrigger()
    {
        slime.AnimationFinishTrigger();
    }
    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(slime.attackOccur.position, slime.attackOccurRedius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                PlayerStats playerStats = hit.GetComponent<PlayerStats>();
                slime.stats.DoDamage(playerStats);
            }
        }
    }
}
