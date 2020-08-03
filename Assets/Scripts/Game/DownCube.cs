using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownCube : MonoBehaviour, IHealth
{
    public float maxHealth;
    public event System.Action<Unit> damaged;
    public float speed;

    [SerializeField]
    private float currentHealth;
    private Rigidbody rb;
    private bool isDie;
    public float CurrentHealth
    {
        get => currentHealth;
        set
        {
            currentHealth = value;
            if (currentHealth < 0)
            {
                currentHealth = 0;
                OnDie();
            }
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }

    public void Start()
    {
        Vector3 target = UnitManager.instance.hero.transform.position;
        Vector3 force = (target - transform.position).normalized * rb.mass * speed;
        rb.AddForce(force, ForceMode.Force);
    }

    public void TakeDamage(Damage damage)
    {
        CurrentHealth -= damage.damageValue;
    }

    private void OnDie()
    {
        currentHealth = 0;
        isDie = true;
        Destroy(gameObject);
    }


    private void OnCollisionEnter(Collision other)
    {
        IHealth enemy = other.collider.GetComponent<IHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(new Damage() { damageValue = 1f });
            TakeDamage(new Damage() { damageValue = 1f });
        }
    }

}
