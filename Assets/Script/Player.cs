using UnityEngine;

public class Player : MonoBehaviour, IDamageTaker
{
    public int Health { get; set; }
    public void TakeDamage(int damage)
    {
        Debug.Log("Player is taking damage - Ouch! " + damage);
    }

}
