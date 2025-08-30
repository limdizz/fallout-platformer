using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    public float speed;
    public Vector2 direction = Vector2.right; 
    public float lifetime = 1f;
    
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HealthManager.Instance.DecreaseHealth(10);
            Destroy(gameObject);
        }

        if (other.CompareTag("Good"))
        {
            Destroy(gameObject);
        }
    }
}
