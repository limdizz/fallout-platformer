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

    public void SetAmmo(int newAmmo)
    {
        ammo = Mathf.Max(0, newAmmo);
        ammoText.text = ammo.ToString();
    }

    public void DecreaseAmmo(int number)
    {
        ammo = Mathf.Max(0, ammo - number);
        ammoText.text = ammo.ToString();
    }
}
