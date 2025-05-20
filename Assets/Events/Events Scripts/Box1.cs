

public class Box1 : Chest
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
        Timer = 5f;
        player.canUseSkill = true;
        tip.SetActive(true);
    }
}
