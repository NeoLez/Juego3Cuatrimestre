using UnityEngine;

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
            case ObjectTypeEnum.PhysicsObject:
                Target.gameObject.layer = LayerMask.NameToLayer("Ground");
                Target.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                break;
        }
    }

    public override void Update(float deltaTime)
    {
        Duration -= deltaTime;
    }

    public override void Remove()
    {
        Target.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        Target.HideIceEffect();
    }
}