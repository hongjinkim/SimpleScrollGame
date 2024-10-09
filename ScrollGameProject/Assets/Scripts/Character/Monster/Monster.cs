using UnityEngine;

public class Monster : MonoBehaviour
{
    public float maxHealth = 500f;
    public float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // 몬스터 제거 및 다음 몬스터 소환 로직
    }
}