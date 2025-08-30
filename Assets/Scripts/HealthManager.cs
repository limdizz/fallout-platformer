using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance;
    [SerializeField] private TMP_Text healthText;
    public Image vaultBoyFace;
    public Sprite vaultBoyFace100;
    public Sprite vaultBoyFace85_100;
    public Sprite vaultBoyFace70_85;
    public Sprite vaultBoyFace70_55;
    public Sprite vaultBoyFace55_40;
    public Sprite vaultBoyFace40_25;
    public Sprite vaultBoyFace0_25;
    private int health = 100;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void HandleHealth()
    {
        if (health == 100)
        {
            vaultBoyFace.sprite = vaultBoyFace100;
        }

        else if (health >= 85 && health < 100)
        {
            vaultBoyFace.sprite = vaultBoyFace85_100;
        }

        else if (health >= 70 && health < 85)
        {
            vaultBoyFace.sprite = vaultBoyFace70_85;
        }

        else if (health >= 55 && health < 70)
        {
            vaultBoyFace.sprite = vaultBoyFace70_55;
        }

        else if (health >= 40 && health < 55)
        {
            vaultBoyFace.sprite = vaultBoyFace55_40;
        }

        else if (health >= 25 && health < 40)
        {
            vaultBoyFace.sprite = vaultBoyFace40_25;
        }

        else if (health >= 1 && health < 25)
        {
            vaultBoyFace.sprite = vaultBoyFace0_25;
        }

        else if (health <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    public bool HealthEquals100
    {
        get { return health == 100; }
    }

    public void IncreaseHealth(int points)
    {
        health = Mathf.Clamp(health + points, 0, 100);
        healthText.text = health.ToString();
        HandleHealth();
    }

    public void DecreaseHealth(int points)
    {
        health = Mathf.Clamp(health - points, 0, 100);
        healthText.text = health.ToString();
        HandleHealth();
    }
}
