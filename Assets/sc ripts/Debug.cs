using UnityEngine;

// 添加到Enemy_DeathBringer上用于调试
public class DeathBringerDebug : MonoBehaviour
{
    private Enemy_DeathBringer deathBringer;
    
    void Start()
    {
        deathBringer = GetComponent<Enemy_DeathBringer>();
    }
    
    void OnGUI()
    {
        if (deathBringer == null) return;
        
        // 显示当前状态
        GUI.Box(new Rect(10, 10, 300, 200), "DeathBringer Debug");
        
        int y = 40;
        GUI.Label(new Rect(20, y, 280, 20), $"Current State: {deathBringer.stateMachine.currentState?.GetType().Name}");
        y += 25;
        
        GUI.Label(new Rect(20, y, 280, 20), $"Boss Fight Begun: {deathBringer.bossFightBegun}");
        y += 25;
        
        GUI.Label(new Rect(20, y, 280, 20), $"Facing Dir: {deathBringer.facingDir}");
        y += 25;
        
        GUI.Label(new Rect(20, y, 280, 20), $"Player Distance: {Vector2.Distance(transform.position, PlayerManager.instance.player.transform.position):F2}");
        y += 25;
        
        // 手动切换状态按钮
        if (GUI.Button(new Rect(20, y, 100, 20), "To Idle"))
        {
            deathBringer.stateMachine.ChangeState(deathBringer.idleState);
        }
        
        if (GUI.Button(new Rect(130, y, 100, 20), "To Attack"))
        {
            deathBringer.stateMachine.ChangeState(deathBringer.attackState);
        }
        y += 25;
        
        if (GUI.Button(new Rect(20, y, 100, 20), "To Teleport"))
        {
            deathBringer.stateMachine.ChangeState(deathBringer.teleportState);
        }
        
        if (GUI.Button(new Rect(130, y, 100, 20), "To Spell"))
        {
            deathBringer.stateMachine.ChangeState(deathBringer.spellCastState);
        }
    }
}