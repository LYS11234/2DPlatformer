using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    #region Components
    [Header("Components")]

    [SerializeField]
    protected Animator anim;
    [SerializeField]
    protected Sprite[] potraits;

    #endregion
    [Space(10)]
    #region Variables
    [Header("Variables")]
    [SerializeField]
    protected string npcName;
    public string npcType;
    [SerializeField]
    protected string[] npcDialogue;
    #endregion

    
}
