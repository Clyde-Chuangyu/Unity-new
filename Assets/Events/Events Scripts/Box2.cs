

public class Box2 : Chest
{
    
    protected override void Start()
    {
        base.Start();
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
        stats.damage.AddModifier(10);
        Timer = 3f;
        tip.SetActive(true);        
    }
}
