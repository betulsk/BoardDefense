using System;

public interface IHealthProvider
{
    Action<float> OnUpdated { get; set; }

    float GetMaxHealth();
    float GetCurHealth();
    float SetCurHealth(float health);
    public void TakeDamage(int damage);
    public void Die();
}
