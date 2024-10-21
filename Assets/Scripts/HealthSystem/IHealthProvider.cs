using System;

public interface IHealthProvider
{
    Action<float> OnUpdated { get; set; }

    public int GetMaxHealth();
    public int GetCurHealth();
    public int SetCurHealth(int health);
    public void TakeDamage(int damage);
    public void Die();
}
