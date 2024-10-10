using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool debug;

    public LayerMask enemyCollisionLayer;

    [Header("Stats")]
    public float attackDamage = 100f;
    public float attackInterval = 1f;
    public float attackRange = 10f;

    [SerializeField] private bool enemyInRange;

    private float lastAttackTime;

    public ObjectPoolBehaviour objectPool;

    public Transform attackPoint;
    private GameObject arrow;

    private void Update()
    {
        DetectEnemies();
        if (enemyInRange)
        { 
            if (Time.time - lastAttackTime >= attackInterval)
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }
    }

    private void DetectEnemies()
    {
        Collider2D hitColliders = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + 3f), attackRange, enemyCollisionLayer);
        if (hitColliders != null)
            enemyInRange = true;
        else
            enemyInRange = false;
    }

    private void OnDrawGizmosSelected()
    {
        if(debug)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + new Vector3(0, 3f, 0), attackRange);
        }
    }

    private void Attack()
    {
        arrow = objectPool.GetPooledObject();
        arrow.transform.position = attackPoint.position;
        arrow.GetComponent<Arrow>().Setup(attackDamage, enemyCollisionLayer);
        arrow.SetActive(true);
    }
}
