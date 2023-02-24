using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    private float maxHealth = 10f;
    private float criticalHealthRatio = 0.3f;

    public UnityAction<float, GameObject> OnDamaged;
    public UnityAction<float> OnHealed;
    public UnityAction OnDie;

    public float CurrentHealth { get; set; }
    public bool Invincible { get; set; }
    public bool CanPickupHealth() => CurrentHealth < maxHealth;
    public float GetRatio() => CurrentHealth / maxHealth;
    public bool IsCritical() => GetRatio() <= criticalHealthRatio;

    bool isDead;

    private void Start()
    {
        CurrentHealth = maxHealth;
    }

    public void Heal(float healAmount)
    {
        float healthBefore = CurrentHealth;
        CurrentHealth += healAmount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, maxHealth);

        float trueHealAmount = CurrentHealth - healthBefore;
        if (trueHealAmount > 0f)
        {
            OnHealed?.Invoke(trueHealAmount);
        }

    }

    public void TakeDamage(float damage, GameObject damageSource)
    {
        if (Invincible)
            return;

        float healthBefore = CurrentHealth;
        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, maxHealth);

        float trueDamageAmount = healthBefore - CurrentHealth;
        if (trueDamageAmount > 0f)
        {
            OnDamaged?.Invoke(trueDamageAmount, damageSource);
        }

        HandleDeath();
    }

    public void Kill()
    {
        CurrentHealth = 0f;

        OnDamaged?.Invoke(maxHealth, null);

        HandleDeath();
    }

    void HandleDeath()
    {
        if (isDead)
            return;

        if (CurrentHealth <= 0f)
        {
            isDead = true;
            OnDie?.Invoke();
        }
    }
}
