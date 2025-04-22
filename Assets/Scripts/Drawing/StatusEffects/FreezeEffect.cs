public class FrozenEffect : StatusEffect
{
    public FrozenEffect(float duration) : base(duration) { }

    public override void Apply()
    {
        Target.ShowIceEffect();

        var type = Target.GetComponent<ObjectStatus>();
        switch (type.Type)
        {
            case ObjectTypeEnum.MovingObject:
                Target.GetComponent<MovingObstacle>().Freeze(0.3f, Duration); // Reduce a 30% de la velocidad
                break;
        }
    }

    public override void Update(float deltaTime)
    {
        Duration -= deltaTime;
    }

    public override void Remove()
    {
        Target.HideIceEffect();
    }
}