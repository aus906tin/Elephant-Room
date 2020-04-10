using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChestController : MonoBehaviour
{
    public Text Chesttext;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void ExitControls()
    {
        this.gameObject.SetActive(false);
    }
}
