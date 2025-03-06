using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogController : MonoBehaviour,Interactable
{
    [SerializeField] Dialog dialog;
    [SerializeField] Dialog speaker;
    public void Interact()
    {
       // Debug.Log("Hello!");
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog,speaker));
    }
}
