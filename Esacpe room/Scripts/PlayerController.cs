using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody rigidBody;
    public GameObject TVPanel;
    public GameObject ScrollPanel;
    public GameObject LockPanel;
    public GameObject LockPanel2;
    public GameObject LockPanel3;
    public GameObject LockerPanel;
    public GameObject ChestPanel;
    public GameObject DrawerPanel;
    public GameObject PausePanel;
    public float rotationSpeed, moveSpeed, maxInteractDistance;
    bool isKeyCollected;
    bool isGamePaused;
    public bool isLevelComplete;
    public Transform InteractionPanelsParent;

    public Text statusMsg;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        isGamePaused = false;
        isKeyCollected = false;
        isLevelComplete = false;
    }

    IEnumerator ShowStatusforTSeconds(string msg, float sec)
    {
        statusMsg.text = msg;

        yield return new WaitForSeconds(sec);

        statusMsg.text = "";
    }

    IEnumerator GoToNextLevel()
    {
        yield return new WaitForSeconds(2);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;

        SceneManager.LoadScene(nextSceneIndex);
    }

    bool isAnyInteractionPanelOpen()
    {
        bool isAnyPanelOpen = false;
        for(int i=0; i<InteractionPanelsParent.childCount; i++)
        {
            GameObject panel = InteractionPanelsParent.GetChild(i).gameObject;

            if (panel.activeInHierarchy)
            {
                isAnyPanelOpen = true;
                break;
            }
        }

        return isAnyPanelOpen;
    }

    void InteractIfUnlocked(GameObject item, GameObject attachedPanel)
    {
        ItemLockManager ILMScript = item.GetComponent<ItemLockManager>();
        if (ILMScript == null)
            attachedPanel.SetActive(true);
        else if (ILMScript.isUnlocked == true)
            attachedPanel.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (isGamePaused == false)
        {
            bool isMoving = false;
            if (Input.GetKey(KeyCode.W) && isAnyInteractionPanelOpen() == false)
            {
                rigidBody.AddForce(transform.forward * moveSpeed * Time.deltaTime);
                isMoving = true;
            }

            if (Input.GetKey(KeyCode.A) && isAnyInteractionPanelOpen() == false)
            {
                transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);
                isMoving = true;
            }
            if (Input.GetKey(KeyCode.D) && isAnyInteractionPanelOpen() == false)
            {
                transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
                isMoving = true;
            }
            if (Input.GetKey(KeyCode.S) && isAnyInteractionPanelOpen() == false)
            {
                rigidBody.AddForce(-transform.forward * moveSpeed / 2 * Time.deltaTime);
                isMoving = true;
            }
            if (isMoving == false && Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, maxInteractDistance))
                {
                    GameObject HitItem = hit.transform.gameObject;
                    if (HitItem.CompareTag("TV"))
                        TVPanel.SetActive(true);
                    else if (HitItem.CompareTag("Scroll"))
                        ScrollPanel.SetActive(true);
                    else if (HitItem.CompareTag("Lock"))
                    {
                        if (HitItem.GetComponent<PadlockController>().isUnlocked == false)
                            LockPanel.SetActive(true);
                    }
                    else if (HitItem.CompareTag("Locker"))
                    {
                        InteractIfUnlocked(HitItem, LockerPanel);
                    }
                    else if (HitItem.CompareTag("Chest"))
                    {
                        InteractIfUnlocked(HitItem, ChestPanel);
                        //ChestPanel.SetActive(true);
                    }
                    else if (HitItem.CompareTag("Drawer"))
                    {
                        if (isKeyCollected == true)
                            DrawerPanel.SetActive(true);
                        else
                            StartCoroutine(ShowStatusforTSeconds("Collect the Key first", 2));
                    }
                    else if (HitItem.CompareTag("Lock2"))
                    {
                       
                        if (HitItem.GetComponent<PadlockController>().isUnlocked == false)
                            LockPanel2.SetActive(true);
                    }
                    else if (HitItem.CompareTag("Lock3"))
                    {
                        if (HitItem.GetComponent<PadlockController>().isUnlocked == false)
                            LockPanel3.SetActive(true);
                    }
                    else if (HitItem.CompareTag("Key"))
                    {
                        HitItem.SetActive(false);
                        isKeyCollected = true;
                    }
                    else if (HitItem.CompareTag("Door"))
                    {
                        if (isLevelComplete == true)
                        {
                            StartCoroutine(ShowStatusforTSeconds("Level Completed", 2));
                            StartCoroutine(GoToNextLevel());
                        }
                        else
                            StartCoroutine(ShowStatusforTSeconds("Open the lock first", 2));
                    }
                }
            }
            if(isMoving == false)
            {
                rigidBody.velocity = Vector3.zero;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (PausePanel.activeInHierarchy)
            {
                isGamePaused = false;
                PausePanel.SetActive(false);
            }
            else
            {
                isGamePaused = true;
                PausePanel.SetActive(true);
            }
        }  
    }
}
