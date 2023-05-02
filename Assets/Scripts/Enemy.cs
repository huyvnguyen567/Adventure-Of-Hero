using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Animator enemyAnim;
    [Header("Health")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Slider healthSlider;
    private int currentHealth;

    [Header("Loot Table")]
    [SerializeField] private GameObject item1Drop;
    [SerializeField] private float item1DropChance; 
    [SerializeField] private GameObject item2Drop;
    [SerializeField] private float item2DropChance;

    [SerializeField] private bool isBoss;
    [SerializeField] private GameObject exitLevel;
    void Awake()
    {
        enemyAnim = GetComponent<Animator>();
    }
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealth();
    }
    void Update()
    {
        UpdateHealth();
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Current health of enemy: " + currentHealth);
        enemyAnim.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            Die();
            if(isBoss == true)
            {
                exitLevel.SetActive(true);
            }
        }
    }

    private void Die()
    {
        Debug.Log("Enemy die");
        enemyAnim.SetBool("IsDied", true);
        AudioController.Instance.EnemyDeathSFX(3);
        //GetComponent<BoxCollider2D>().enabled = false;
        //this.enabled = false;
        Destroy(gameObject, 1);
        if(Random.Range(0f,100f)< item1DropChance)
        {
            Instantiate(item1Drop, transform.position, transform.rotation);
        }
        if(Random.Range(0f,100f)< item2DropChance)
        {
            Instantiate(item2Drop, transform.position, transform.rotation);
        }
    }
    private void UpdateHealth()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }
}
