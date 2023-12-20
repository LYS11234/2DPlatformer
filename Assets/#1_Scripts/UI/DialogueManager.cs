using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    #region Components
    [SerializeField]
    private Image panel;
    [SerializeField]
    private Text nameText;
    [SerializeField]
    public Text dialogueText;
    [SerializeField]
    public Image upgradeAllow;
    [SerializeField]
    public Image upgradeDeny;
    [SerializeField]
    public Image pointer;
    #endregion

    public void PrintDialogue(string _name, string _dialogue)
    {
        if (!panel.gameObject.active)
        {
            panel.gameObject.SetActive(true);
        }
        nameText.text = _name;
        dialogueText.text = _dialogue;
    }

    public void CloseDialogue()
    {
        nameText.text = "";
        dialogueText.text = "";
        panel.gameObject.SetActive(false);
        PlayerManager.instance.canMove = true;
    }
}
