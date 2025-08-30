using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    [SerializeField] private TMP_Text scoreText;
    private int score;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }
}
