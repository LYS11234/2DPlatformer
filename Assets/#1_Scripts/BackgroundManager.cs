using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    private Camera theCamera;
    [SerializeField]
    private Canvas canvas;
    void Start()
    {
        theCamera = FindObjectOfType<Camera>();
        canvas.worldCamera = theCamera;
        PlayerManager.instance.gameObject.SetActive(true);
        PlayerManager.instance.canMove = true;
        PlayerManager.instance.canAttack = true;
        Parameter.instance.gameObject.SetActive(true);
    }
}
