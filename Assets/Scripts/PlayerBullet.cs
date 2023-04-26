using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private Vector2 moveDirection;
    [SerializeField] private GameObject bulletEffectPref;
    [SerializeField] private int bulletDamage = 10;
    private Rigidbody2D bulletRb;
    public Vector2 MoveDirection 
    {
        get { return moveDirection; }
        set { moveDirection = value; }
    }
 
    void Start()
    {
        bulletRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        bulletRb.velocity = moveDirection * bulletSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(bulletDamage);
        }    
        Instantiate(bulletEffectPref, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
