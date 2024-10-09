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
        // ���� ���� �� ���� ���� ��ȯ ����
    }
}