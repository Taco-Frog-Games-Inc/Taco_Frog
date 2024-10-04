using UnityEngine;

public interface IDamager
{
    int DamageToApply { get; }
    void OnTriggerEnter(Collider other);
    void ApplyDamage(int damage);
}