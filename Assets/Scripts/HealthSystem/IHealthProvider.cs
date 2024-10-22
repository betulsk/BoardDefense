using System;

public interface IHealthProvider
{
    Action<int> OHealthUpdated { get; set; }

    public int GetMaxHealth();
    public int GetCurHealth();
    public int SetCurHealth(int health);
    public void TakeDamage(int damage);
    public void Die();
}
