public class BurningEffect : StatusEffect
{
    private float tick = 1f;

    public BurningEffect(float duration) : base(duration) { }

    public override void Apply()
    {
        Target.ShowFireEffect();
    }

    public override void Update(float deltaTime)
    {
        Duration -= deltaTime;
        tick -= deltaTime;

        if (tick <= 0f)
        {
            Target.TakeDamage(1);
            tick = 1f;
        }
    }

    public override void Remove()
    {
        Target.HideFireEffect();
    }
}
