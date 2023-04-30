using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float attackDistance;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float timer;
    [SerializeField] private Transform leftLimit;
    [SerializeField] private Transform rightLimit;
    public GameObject hotZone;
    public GameObject triggerArea;

    [SerializeField] private Transform groundcheck;  
    [SerializeField] private LayerMask groundFloor;
    private bool isGrounded;

    public Transform target;
    public bool inRange;
    private Animator anim;
    private float distance;
    private bool attackMode;
    private bool cooling;
    private float intTimer;

    private void Awake()
    {
        intTimer = timer; 
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundcheck.position, 1f, groundFloor);

        if (!attackMode && isGrounded)
        {
            Move();
        }
        else if (!isGrounded)
        {
            Patrol();
            Move();
        }

        if (!InsideOfLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack"))
        {
            Patrol();
        }

        if (inRange)
        {
            EnemyLogic();
        }
    }

    void EnemyLogic()
    {
        if (Player.Instance.IsDeath() == false)
        {
            distance = Vector2.Distance(transform.position, target.position);

            if (distance > attackDistance)
            {
                StopAttack();
            }

            else if (attackDistance >= distance && cooling == false)
            {
                Attack();
            }

            if (cooling)
            {
                cooldown();
                anim.SetBool("Attack", false);
            }
        }
     
    }

    void Move()
    {
        anim.SetBool("Walk", true);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack") && !inRange)
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, (moveSpeed+1f) * Time.deltaTime);
        }
    }

    void Attack()
    {
        isGrounded = true;
        timer = intTimer; 
        attackMode = true;

        anim.SetBool("Walk", false);
        anim.SetBool("Attack", true);
    }

    void cooldown()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);
    }
    public void TriggerCooling()
    {
        cooling = true;
    }

    private bool InsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    public void Patrol()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }

        Flip();
    }

    public void Flip()
    {
        if(target!= null)
        {
            if (transform.position.x > target.position.x)
            {
                transform.localScale = new Vector3(-1,1,1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
       
    }
}
