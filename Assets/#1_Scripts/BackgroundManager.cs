using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

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
    public GameObject builder;
    [SerializeField]
    private GameObject[] monsters;
    [SerializeField]
    private Transform[] monsterSpawnPos;
    [SerializeField]
    private MoveSceneNPC moveSceneNpc;
    [SerializeField]
    private HostileNPC[] monster;



    void Start()
    {
        if (blockBridge != null)
        {
            bridge.SetActive(false);
            if (Database.Instance.nowPlayer.bridgeFixed)
            {
                bridge.SetActive(true);
                Destroy(blockBridge);
                Destroy(builder);
            }            
        }
        canvas.worldCamera = PlayerManager.instance.mainCamera;
        PlayerManager.instance.gameObject.SetActive(true);
        PlayerManager.instance.canMove = true;
        PlayerManager.instance.canAttack = true;
        Parameter.instance.gameObject.SetActive(true);
        PlayerManager.instance.playerTransform.position = genPos.position;
        CameraManager.instance.SetBound(bound);
        SpawnMonster();
        Parameter.instance.GetComponent<WorldMapManager>().monsterSpawnCount++;

    }

    private void Update()
    {
        if (monsters != null)
        {
            if (!Parameter.instance.GetComponent<WorldMapManager>().m_WorldMap.activeSelf && Parameter.instance.GetComponent<WorldMapManager>().monsterSpawnCount == 0)
            {
                for (int i = 0; i < monster.Length; i++)
                {
                    if (monster[i] != null)
                        Destroy(monster[i].gameObject);
                }
                SpawnMonster();
                
            }
        }
    }

    private void SpawnMonster()
    {
        for (int i = 0; i < monsterSpawnPos.Length; i++)
        {
            monster[i] = Instantiate(monsters[i], monsterSpawnPos[i].position, Quaternion.identity).GetComponent<HostileNPC>();
            Parameter.instance.GetComponent<WorldMapManager>().monsterSpawnCount++;
            monster[i].moveSceneNPC = moveSceneNpc;
        }
    }
}
