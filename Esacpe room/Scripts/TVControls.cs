using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TVControls : MonoBehaviour
{

    public Text TVChannel;

    // Start is called before the first frame update
    void Start()
    {

    }
    public void ExitControls()
    {
        //print("Inside ext");
        this.gameObject.SetActive(false);
    }
}
