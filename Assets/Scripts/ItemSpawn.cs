using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    public GameObject RightSide;
    public GameObject[] items;
    public float startDelay, repeatRate;

    void Start()
    {
        InvokeRepeating("Spawn", startDelay, repeatRate);
    }

    void Spawn()
    {
        Vector3 pos = new Vector3(Random.Range(gameObject.transform.position.x, RightSide.transform.position.x), gameObject.transform.position.y, 0);
        Instantiate(items[Random.Range(0, items.Length)], pos, gameObject.transform.rotation);
    }
}
