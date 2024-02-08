using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private Transform genPos;
    [SerializeField]
    private BoxCollider2D bound;
    public GameObject blockBridge;
    public GameObject bridge;

    void Start()
    {
        if (blockBridge != null)
        {
            bridge.SetActive(false);
            if (Database.Instance.nowPlayer.bridgeFixed)
            {
                blockBridge.SetActive(false);
                bridge.SetActive(true);
            }            
        }
        canvas.worldCamera = PlayerManager.instance.camera;
        PlayerManager.instance.gameObject.SetActive(true);
        PlayerManager.instance.canMove = true;
        PlayerManager.instance.canAttack = true;
        Parameter.instance.gameObject.SetActive(true);
        PlayerManager.instance.playerTransform.position = genPos.position;
        CameraManager.instance.SetBound(bound);
    }
}
