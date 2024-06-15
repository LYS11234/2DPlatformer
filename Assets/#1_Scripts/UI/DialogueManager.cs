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
    public Text dialogueText;
    public Image upgradeAllow;
    public Image upgradeDeny;
    public Image pointer;
    public Image talk;
    public Image buy;
    public Image sell;
    public GameObject buyBase;
    public GameObject inven;
    #endregion

    public void PrintDialogue(string _name, string _dialogue)
    {
        if (!panel.gameObject.activeSelf)
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
