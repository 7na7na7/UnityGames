using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testDialogue : MonoBehaviour
{
    [SerializeField]
    public Dialogue dialogue;

    private DialogManager theDM;
    void Start()
    {
        theDM = FindObjectOfType<DialogManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "player")
        {
            theDM.ShowDialogue(dialogue);
            this.gameObject.SetActive(false);
        }
    }
}
