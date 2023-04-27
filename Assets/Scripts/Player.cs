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
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    [SerializeField] private float healingSpeed = 1f;
    [SerializeField] private bool isDeath;
    public float MaxHealth { get { return maxHealth; } set { maxHealth = value; } }
    public float CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }
    public float HealingSpeed { get { return healingSpeed; } set { healingSpeed = value; } }

    [Header("Health UI")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;

    [Header("Magic")]
    [SerializeField] private float maxMagic = 100f;
    [SerializeField] private float currentMagic;
    [SerializeField] private float magicRefillSpeed = 2f;
    [SerializeField] private float bulletMagicCost = 20f;
    public float MaxMagic { get { return maxMagic; } set { maxMagic = value; } }
    public float CurrentMagic { get { return currentMagic; } set { currentMagic = value; } }
    public float MagicRefillSpeed { get { return magicRefillSpeed; } set { magicRefillSpeed = value; } }

    [Header("Magic UI")]
    [SerializeField] private Slider magicSlider;
    [SerializeField] private TMP_Text magicText;

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
    public int AttackDamage { get { return attackDamage; } set { attackDamage = value; } }

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

        maxHealth = SaveManager.Instance.activeSave.maxHealth;
        maxMagic = SaveManager.Instance.activeSave.maxMagic;
        healingSpeed = SaveManager.Instance.activeSave.healingSpeed;
        magicRefillSpeed = SaveManager.Instance.activeSave.magicRefillSpeed;

        currentHealth = maxHealth;
        currentMagic = maxMagic;
        UpdateHealth();
        UpdateMagic();
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        UpdateHealth();
        Healing();
        UpdateMagic();
        RefillMagic();
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

            if (Input.GetMouseButtonDown(1) && currentMagic>= bulletMagicCost)
            {
                var bullet = Instantiate(bulletPref, shootingPoint.position, shootingPoint.rotation);
                bullet.MoveDirection = new Vector2(transform.localScale.x, 0);
                currentMagic -= bulletMagicCost;
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
        healthText.text = Mathf.RoundToInt(currentHealth) + "/" + maxHealth;
    }

    public void Healing()
    {
        currentHealth += healingSpeed * Time.deltaTime;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
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

    public void UpdateMagic()
    {
        magicSlider.maxValue = maxMagic;
        magicSlider.value = currentMagic;
        magicText.text = Mathf.RoundToInt(currentMagic) + "/" + maxMagic;
    }
    public void RefillMagic()
    {
        currentMagic += magicRefillSpeed * Time.deltaTime;
        if(currentMagic > maxMagic)
        {
            currentMagic = maxMagic;
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
