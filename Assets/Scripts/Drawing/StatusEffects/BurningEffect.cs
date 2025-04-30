using UnityEngine;

public class BurningEffect : StatusEffect
{
    private float tick = 1f;

    public BurningEffect(float duration) : base(duration) { }

    public override void Apply()
    {
        Target.ShowFireEffect();
        Target.gameObject.layer = LayerMask.NameToLayer("Ground");
        Renderer renderer = Target.GetComponent<Renderer>();
        Material objMaterial = new Material(renderer.material);
        renderer.material = objMaterial;

        float fireAmount = 1;
        LeanTween.value(Target.gameObject, fireAmount, -1, 1).setOnUpdate((float val) =>
        {
            fireAmount = val;
            objMaterial.SetFloat("_FireTransition", fireAmount);
        });
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
        var type = Target.GetComponent<ObjectStatus>();
        switch (type.Type) {
            case ObjectTypeEnum.PhysicsObject:
                Renderer renderer = Target.GetComponent<Renderer>();
                Material objMaterial = new Material(renderer.material);
                renderer.material = objMaterial;

                float fireAmount = -1;
                LeanTween.value(Target.gameObject, fireAmount, 1, 1).setOnUpdate((float val) =>
                {
                    fireAmount = val;
                    objMaterial.SetFloat("_FireTransition", fireAmount);
                });
                break;
        }
        
        Target.HideFireEffect();
    }
}
