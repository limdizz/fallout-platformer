using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemChecker : MonoBehaviour
{
    public int score;
    public GameObject ScoreObject;
    TMP_Text scoreText;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bad")
        {
            HealthManager.Instance.DecreaseHealth(25);
            HealthManager.Instance.HandleHealth();
        }

        else if (other.gameObject.tag == "Finish")
        {
            SceneManager.LoadScene(0);
        }

        else if (other.gameObject.tag == "Medkit")
        {
            if (!HealthManager.Instance.HealthEquals100)
            {
                HealthManager.Instance.IncreaseHealth(10);
                Destroy(other.gameObject);
            }
        }
    }

    void Start()
    {
        scoreText = ScoreObject.GetComponent<TMP_Text>();
    }
}
