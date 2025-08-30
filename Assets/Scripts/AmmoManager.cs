using TMPro;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    public static AmmoManager Instance;
    [SerializeField] private TMP_Text ammoText;
    private int ammo = 32;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void DecreaseAmmo(int number)
    {
        ammo -= number;
        ammoText.text = ammo.ToString();
    }
}
