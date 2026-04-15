using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    [Header("Item Settings")]
    public int pointsValue = 10; 
    public AudioClip collectSound; 


    [Header("Animation")]
    public float floatSpeed = 0.5f; 
    public float floatHeight = 0.2f; 
    public float rotationSpeed = 90f; 

    private Vector3 startPosition;
    private float floatTimer;

    void Start()
    {
        startPosition = transform.position;
        floatTimer = Random.Range(0f, Mathf.PI * 2f);
    }

    void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed + floatTimer) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }

    void Collect()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(pointsValue);
        }

        if (collectSound != null)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
        }

        Destroy(gameObject);
    }
}
