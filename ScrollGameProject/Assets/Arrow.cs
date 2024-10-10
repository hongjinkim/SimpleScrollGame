using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;

public class Arrow : MonoBehaviour
{
    [SerializeField]private LayerMask levelCollisionLayer;
    private LayerMask enemyCollisionLayer;

    [SerializeField] private float arrowSpeed = 20f;
    [SerializeField] private float arrowDamage = 100f;
    private bool isHit = false;

    private Vector2 direction = new Vector2(1, 0);
    private Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        isHit = false;
    }

    private void Update()
    {
        if (isHit)
        {
            return;
        }
        if(rigidbody != null)
            rigidbody.velocity = direction * arrowSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsLayerMatched(levelCollisionLayer.value, collision.gameObject.layer))
        {
            DestroyProjectile();
        }
        else if (IsLayerMatched(enemyCollisionLayer.value, collision.gameObject.layer))
        {
            DestroyProjectile();
            var enemy = collision.GetComponent<Monster>();
            if (enemy != null)
            {
                enemy.TakeDamage(arrowDamage);
            }
        }
    }

    public void Setup(float damage, LayerMask targetMask)
    {
        SetDamage(damage);
        SetTarget(targetMask);
    }

    private void SetDamage(float damage)
    {
        arrowDamage = damage;
    }

    private void SetTarget(LayerMask targetMask)
    {
        enemyCollisionLayer = targetMask;
    }

    


    private void DestroyProjectile()
    {
        isHit = true;
        gameObject.SetActive(false);
    }

}
