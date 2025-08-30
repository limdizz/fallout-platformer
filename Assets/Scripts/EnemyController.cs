using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public Transform leftPoint;
    public Transform rightPoint; 
    private bool movingRight = true;

    [Header("Player Detection")]
    public Transform player;
    public float detectionRange = 8f;
    private bool playerInRange;
    public float stopDistance = 2f;

    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;
    public float fireRate = 3f;
    private float nextFireTime;
    public AudioClip shootSound;

    void Start()
    {
        nextFireTime = Time.time + 1f;
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        playerInRange = distanceToPlayer <= detectionRange;

        if (playerInRange)
        {
            Chase(distanceToPlayer);
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            transform.localScale = new Vector3(1, 1, 1);

            if (transform.position.x >= rightPoint.position.x)
                movingRight = false;
        }

        else
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            transform.localScale = new Vector3(-1, 1, 1);

            if (transform.position.x <= leftPoint.position.x)
                movingRight = true;
        }
    }

    void Chase(float distanceToPlayer)
    {
        if (player.position.x > transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);

        if (distanceToPlayer > stopDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
        }

        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        AudioSource.PlayClipAtPoint(shootSound, transform.position);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        EnemyBulletController bulletScript = bullet.GetComponent<EnemyBulletController>();

        Vector2 directionToPlayer = (player.position - firePoint.position).normalized;
        bulletScript.direction = directionToPlayer;
        bulletScript.speed = bulletSpeed;
    }
}

