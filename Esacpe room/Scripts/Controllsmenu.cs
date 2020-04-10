using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controllsmenu : MonoBehaviour
{
    public void GoToMainMenu()
    {
        transform.parent.GetChild(0).gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
