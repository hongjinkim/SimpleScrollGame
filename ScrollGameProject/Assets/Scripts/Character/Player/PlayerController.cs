using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float attackDamage = 100f;
    public float attackInterval = 1f;

    private float lastAttackTime;

    private void Update()
    {
        if (Time.time - lastAttackTime >= attackInterval)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    private void Attack()
    {
        
    }
}
