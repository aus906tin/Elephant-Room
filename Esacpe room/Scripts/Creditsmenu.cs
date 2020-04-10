using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Creditsmenu : MonoBehaviour
{
    public GameObject nextPanel;
    public void GoToMainMenu()
    {
        transform.parent.GetChild(0).gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void GoToNextPanel()
    {
        nextPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}