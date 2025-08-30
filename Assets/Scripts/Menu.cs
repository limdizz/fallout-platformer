using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject image_level1, sign_level1, image_level2, sign_level2;

    public void ShowObjectsLevel1()
    {
        image_level1.SetActive(true);
        sign_level1.SetActive(true);
    }

    public void ShowObjectsLevel2()
    {
        image_level2.SetActive(true);
        sign_level2.SetActive(true);
    }

    public void HideObjectsLevel1()
    {
        image_level1.SetActive(false);
        sign_level1.SetActive(false);
    }

    public void HideObjectsLevel2()
    {
        image_level2.SetActive(false);
        sign_level2.SetActive(false);
    }

    public void OnPlayLevel1Button()
    {
        SceneManager.LoadScene(2);
    }

    public void SelectLevels()
    {
        SceneManager.LoadScene(1);
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
