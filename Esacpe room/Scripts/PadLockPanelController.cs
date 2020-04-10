using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PadLockPanelController : MonoBehaviour
{
    public InputField[] lockInputs;
    public CylinderController[] lockCylinders;
    public PadlockController padLock;
    public Text lockStatus;
    public bool isMainDoorLock;
    public PlayerController playerControllerReference;

    public void TryCombination()
    {
        
        bool isValidInput = true;
        bool isCorrectCombination = true;
        lockStatus.text = "";
        for (int i=0; i<lockInputs.Length; i++)
        {
            lockCylinders[i].SetTargetPosition(0);

            string userInpString = lockInputs[i].text.ToString();
            int target;
            if (int.TryParse(userInpString, out target))
            {
                if (padLock.Combination[i] != target)
                    isCorrectCombination = false;

                lockCylinders[i].SetTargetPosition(target);
            }
            else
            {
                isValidInput = false;
                isCorrectCombination = false;
            }
        }

        if(isValidInput == false)
        {
            lockStatus.text = "Invalid input, fill all digits 0-9";
        }
        else
        {
            if (isCorrectCombination)
            {
                lockStatus.text = "Opening lock ..";
                StartCoroutine(OpenLock(1));
            }
            else
            {
                lockStatus.text = "That dint work, try again";
            }
        }
    }

    IEnumerator OpenLock(float delay)
    {
        yield return new WaitForSeconds(delay);
        if(playerControllerReference != null && isMainDoorLock)
        {
            playerControllerReference.isLevelComplete = true;
        }
        exitLockPanel();

    }
    public void exitLockPanel()
    {
        this.gameObject.SetActive(false);
    }
}
