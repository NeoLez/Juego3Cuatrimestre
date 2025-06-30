using UnityEngine;

public class BurningEffect : StatusEffect
{
    //TP2 Enzo Francisco Melidoni
    private float tick = 1f;
    private AudioSource source;

    public BurningEffect(float duration) : base(duration) { }

    public override void Apply()
    {
        Target.ShowFireEffect();
        Renderer renderer = Target.GetComponent<Renderer>();
        Material objMaterial = new Material(renderer.material);
        renderer.material = objMaterial;

        float fireAmount = 1;
        var type = Target.GetComponent<ObjectStatus>();
        source = GameManager.AudioSystem.PlaySoundLooping(type.fireEffectSound, type.gameObject.transform.position);
        GameManager.AudioSystem.PlaySound(type.fireImpactSound);
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

        source.transform.position = Target.transform.position;

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
                break;
        }
        
        Target.HideFireEffect();
    }

    public override void Die() {
        LeanTween.value(source.gameObject, 1.0f, 0.0f, 1.5f).setOnUpdate((float val) => {
            source.volume = val;
        }).setOnComplete(_ => GameObject.Destroy(source.gameObject));
    }
}
