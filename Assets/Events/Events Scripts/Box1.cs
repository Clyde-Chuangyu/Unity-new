
public class Box1 : Chest 
{     
    protected override void Start()     
    {         
        base.Start();
        // 不要在这里播放音效
    }     
    
    protected override void Update()     
    {         
        base.Update();     
    }     
    
    protected override void Effect()     
    {         
        base.Effect();
        
        // 在箱子被打开时播放音效
        AudioManager.Instance?.PlayboxSound();
        
        Timer = 5f;         
        player.canUseSkill = true;         
        tip.SetActive(true);     
    } 
}