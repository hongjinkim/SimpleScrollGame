using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using static Utils;

public class Monster : MonoBehaviour
{
    public SampleMonsterData monsterData;

    [Header("Info")]
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
    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;

    public Button monsterButton;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        monsterButton.onClick.AddListener(OnMonsterClick);

        healthBar = healthBarObject.GetComponent<Slider>();
    }


    private void Update()
    {
        if (isDead)
        {
            return;
        }
        if (rigidbody != null)
            rigidbody.velocity = direction *_speed;
        healthBarObject.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 5.5f, 0));
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
        var entry = monsterData.entries[idx];
        _name = entry.Name;
        _grade = entry.Grade;
        _speed = entry.Speed;
        _health = (float)entry.Health;

        isDead = false;
        currentHealth = _health;

        UpdateHealthBar();
    }

    private void OnMonsterClick()
    {
        EventManager.TriggerEvent(EventType.MonsterSelected, new Dictionary<string, object>() {
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