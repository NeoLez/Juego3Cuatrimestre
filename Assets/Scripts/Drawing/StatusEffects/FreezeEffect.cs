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
                Target.GetComponent<MovingObstacle>().Freeze(0f, Duration); // Reduce a 30% de la velocidad
                break;
            case ObjectTypeEnum.PhysicsObject:
                Target.gameObject.layer = LayerMask.NameToLayer("Ground");
                Target.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                Renderer renderer = Target.GetComponent<Renderer>();
                Material objMaterial = new Material(renderer.material);
                renderer.material = objMaterial;

                float frostAmount = 1;
                LeanTween.value(Target.gameObject, frostAmount, -1, 1).setOnUpdate((float val) =>
                {
                    frostAmount = val;
                    objMaterial.SetFloat("_IceTransition", frostAmount);
                });
                break;
        }
    }

    public override void Update(float deltaTime)
    {
        Duration -= deltaTime;
    }

    public override void Remove()
    {
        var type = Target.GetComponent<ObjectStatus>();
        switch (type.Type) {
            case ObjectTypeEnum.PhysicsObject:
                Target.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                Renderer renderer = Target.GetComponent<Renderer>();
                Material objMaterial = new Material(renderer.material);
                renderer.material = objMaterial;

                float frostAmount = -1;
                LeanTween.value(Target.gameObject, frostAmount, 1, 1).setOnUpdate((float val) =>
                {
                    frostAmount = val;
                    objMaterial.SetFloat("_IceTransition", frostAmount);
                });
                break;
        }
        
        Target.HideIceEffect();
    }
}