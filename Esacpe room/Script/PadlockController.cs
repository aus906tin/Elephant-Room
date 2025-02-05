﻿using UnityEngine;
using System.Collections;

public class PadlockController : MonoBehaviour {

    private Animator anim;
    private int unlockHash;
    public CylinderController[] Cylinders = new CylinderController[4];

    public int[] Combination = new int[4];
    public bool isUnlocked;
    // Use this for initialization
    void Start() {
        isUnlocked = false;
        for (int i = 0; i < 4; i++)
        {
            GameObject go = transform.Find("BodyGroup").transform.Find(string.Format("Cylinder{0}", i + 1)).gameObject;
            Cylinders[i] = go.GetComponent<CylinderController>();
            Cylinders[i].padlockController = this;
        }
        anim = GetComponent<Animator>();
        anim.enabled = false;
        unlockHash = Animator.StringToHash("OpenLock");
    }

    public bool CheckCombination() {
        bool correctCombination = true;
        for (int i = 0; i < 4; i++)
        {
            if (Cylinders[i].Moving || Combination[i] != Cylinders[i].CurrentPosition) {
                correctCombination = false;
                break;
            }
        }

        if (correctCombination) {
            anim.enabled = true;
            anim.Play(unlockHash);
            isUnlocked = true;
            UnlockLockedItem();
        }
        return correctCombination;
    }

    public void UnlockLockedItem()
    {
        if(transform.parent != null)
        {
            ItemLockManager script = transform.parent.GetComponent<ItemLockManager>();

            if(script != null)
            {
                script.isUnlocked = true;
            }
        }
    }
}
