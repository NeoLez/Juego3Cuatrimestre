public abstract class StatusEffect
{
    protected float Duration;
    protected ObjectStatus Target;

    public StatusEffect(float duration)
    {
        Duration = duration;
    }

    public void SetTarget(ObjectStatus obj)
    {
        Target = obj;
    }

    public abstract void Apply();
    public abstract void Update(float deltaTime);
    public abstract void Remove();

    public bool IsComplete => Duration <= 0f;
}
