using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStatus : MonoBehaviour
{
    [Header("Tipo del Objeto")]
    public ObjectTypeEnum Type;

    [Header("Salud")]
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;

    [Header("Efectos Visuales")]
    [SerializeField] private GameObject fireEffectPrefab;
    [SerializeField] private GameObject iceEffectPrefab;

    private GameObject fireEffectInstance;
    private GameObject iceEffectInstance;

    public AudioClip iceEffectSound;
    public AudioClip iceEffectImpactSound;
    public AudioClip fireImpactSound;
    public AudioClip fireEffectSound;

    private List<StatusEffect> activeEffects = new List<StatusEffect>();

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        for (int i = activeEffects.Count - 1; i >= 0; i--)
        {
            activeEffects[i].Update(Time.deltaTime);
            if (activeEffects[i].IsComplete)
            {
                activeEffects[i].Remove();
                activeEffects.RemoveAt(i);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth == 0)
            Die();
    }

    public void ApplyEffect(StatusEffect effect)
    {
        effect.SetTarget(this);
        effect.Apply();
        activeEffects.Add(effect);
    }

    private void Die()
    {
        foreach(var effect in activeEffects)
            effect.Die();
            
        Debug.Log("c");
        switch (Type) {
            case ObjectTypeEnum.PhysicsObject:
                gameObject.layer = LayerMask.NameToLayer("Ground");
                Renderer renderer = gameObject.GetComponent<Renderer>();
                Material objMaterial = new Material(renderer.material);
                renderer.material = objMaterial;
                
                LeanTween.value(gameObject, -0.5f, 0.5f, 2f).setOnUpdate((val) =>
                {
                    objMaterial.SetFloat("_Disintegrate", val);
                }).setDestroyOnComplete(true);
                break;
            default:
                Destroy(gameObject);
                break;
        }
        
    }

    public void ShowFireEffect()
    {
        if (fireEffectPrefab != null && fireEffectInstance == null)
            fireEffectInstance = Instantiate(fireEffectPrefab, transform);
    }

    public void HideFireEffect()
    {
        if (fireEffectInstance != null)
            Destroy(fireEffectInstance);
    }

    public void ShowIceEffect()
    {
        if (iceEffectPrefab != null && iceEffectInstance == null)
            iceEffectInstance = Instantiate(iceEffectPrefab, transform);
    }

    public void HideIceEffect()
    {
        if (iceEffectInstance != null)
            Destroy(iceEffectInstance);
    }

    public bool HasEffect(Type statusType) {
        foreach (StatusEffect effect in activeEffects) {
            if (effect.GetType() == statusType) {
                return true;
            }
        }
        return false;
    }
}
