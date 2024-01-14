using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private Transform genPos;
    [SerializeField]
    private BoxCollider2D bound;
    void Start()
    {
        canvas.worldCamera = PlayerManager.instance.camera;
        PlayerManager.instance.gameObject.SetActive(true);
        PlayerManager.instance.canMove = true;
        PlayerManager.instance.canAttack = true;
        Parameter.instance.gameObject.SetActive(true);
        PlayerManager.instance.playerTransform.position = genPos.position;
        CameraManager.instance.SetBound(bound);
    }
}
