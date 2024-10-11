using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using static Utils;

public class Monster : MonoBehaviour
{
    public SampleMonsterData monsterData;
    public LayerMask levelCollisionLayer;

    [Header("Info")]
    [SerializeField] private int _idx;
    [SerializeField] private string _name;
    [SerializeField] private string _grade;
    [SerializeField] private float _speed;
    [SerializeField] private float _health;

    

    [Header("Status")]
    public float currentHealth;
    public bool isDead;

    private Slider healthBar;
    public GameObject healthBarObject;

    private Vector2 direction = new Vector2(-1, 0);
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        healthBar = healthBarObject.GetComponent<Slider>();
    }


    private void Update()
    {
        if (isDead)
        {
            return;
        }
        if (rb != null)
            rb.velocity = direction *_speed;
        healthBarObject.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 5.5f, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsLayerMatched(levelCollisionLayer.value, collision.gameObject.layer))
        {
            Die();
        }
    }

        public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        gameObject.SetActive(false);
        EventManager.TriggerEvent(EventType.MonsterDied, null);
    }

    public void Setup(int idx)
    {
        _idx = idx;
        var entry = monsterData.entries[idx];
        _name = entry.Name;
        _grade = entry.Grade;
        _speed = entry.Speed;
        _health = (float)entry.Health;

        isDead = false;
        currentHealth = _health;

        spriteRenderer.sprite = GameDataManager.instance.MonsterSprites[idx];

        UpdateHealthBar();
    }

    public void OnMonsterClicked()
    {
        EventManager.TriggerEvent(EventType.MonsterSelected, new Dictionary<string, object>() {
            { "idx", _idx },
            { "name", _name },
            { "grade", _grade },
            { "speed", _speed },
            { "health", _health } 
        });
    }

    private void UpdateHealthBar()
    {
        healthBar.value = currentHealth / _health;
    }
}