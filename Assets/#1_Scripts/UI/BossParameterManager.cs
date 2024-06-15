using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossParameterManager : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    public Image bossHpBase;
    public Image bossHp;
    public Text bossName;
    private void Start()
    {
        canvas.worldCamera = PlayerManager.instance.mainCamera;
    }
}
