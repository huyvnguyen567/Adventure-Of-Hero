using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [Header("Health")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    [SerializeField] private bool isDeath;
    public int MaxHealth { get { return maxHealth; } set { maxHealth = value; } }
    public int CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }

    [Header("Health UI")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;

    [Header("Movement")]
    [SerializeField] private float runSpeed = 5.0f;
    private Animator playerAnim;
    private Rigidbody2D playerRb;
    private SpriteRenderer playerSprite;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool doDoubleJump;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject jumpEffect;

    [Header("Attack")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private int attackDamage = 20;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private float attackRate = 2f;
    [SerializeField] private float nextAttackTime = 0f;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Range Attack")]
    [SerializeField] private PlayerBullet bulletPref;
    [SerializeField] private Transform shootingPoint;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponentInChildren<Animator>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();

        currentHealth = maxHealth;
        UpdateHealth();
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        UpdateHealth();
        if (!isDeath)
        {
            playerRb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * runSpeed, playerRb.velocity.y);

            if (playerRb.velocity.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (playerRb.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

            if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || doDoubleJump))
            {
                if (isGrounded)
                {
                    doDoubleJump = true;
                    AudioController.Instance.PlayerSFX(7);
                    Instantiate(jumpEffect, transform.position, transform.rotation);
                }
                else
                {
                    doDoubleJump = false;
                    AudioController.Instance.PlayerSFX(6);
                    playerAnim.SetTrigger("Double Jump");
                    Instantiate(jumpEffect, transform.position, transform.rotation);
                }
                playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
            }

            if (Time.time >= nextAttackTime)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    MeleeAttack();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }

            playerAnim.SetBool("IsGrounded", isGrounded);
            playerAnim.SetFloat("Speed", Mathf.Abs(playerRb.velocity.x));

            if (Input.GetMouseButtonDown(1))
            {
                var bullet = Instantiate(bulletPref, shootingPoint.position, shootingPoint.rotation);
                bullet.MoveDirection = new Vector2(transform.localScale.x, 0);
                AudioController.Instance.PlayerSFX(0);
            }
        }
        
    }
    public bool IsDeath()
    {
        return isDeath;
    }
    private void MeleeAttack()
    {
        playerAnim.SetTrigger("Attack");
        AudioController.Instance.PlayerSFX(8);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach(var enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Current health of player: " + currentHealth);
        playerAnim.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            Die();
        }
        UpdateHealth();
    }
    private void Die()
    {
        Debug.Log("Player die");
        isDeath = true;
        playerAnim.SetBool("IsDeath", true);
        AudioController.Instance.PlayerSFX(5);
        Destroy(gameObject,1);
    }

    public void UpdateHealth()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        healthText.text = currentHealth + "/" + maxHealth;
    }

    public void RestoreHealth(int healthToGive)
    {
        currentHealth += healthToGive;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealth();
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
