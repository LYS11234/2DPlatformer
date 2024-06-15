using Schema.Builtin.Nodes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveSceneNPC : AllienceNPC
{
    [SerializeField]
    private string mapName;
    [SerializeField]
    private GameObject mapImage;
    [SerializeField]
    private bool canTalk;
    public int numberOfObject;
    private float distance;

    private new void Start()
    {
        mapImage = Parameter.instance.gameObject.GetComponent<WorldMapManager>().m_WorldMap;
    }

    private void Update()
    {
        distance = Mathf.Abs(PlayerManager.instance.transform.position.x - this.transform.position.x);
        if (numberOfObject == 1)
        {
            if (distance <= 0.3)
            {
                PlayerManager.instance.canAttack = false;
                canTalk = true;
            }
            else
            {
                PlayerManager.instance.canAttack = true;
                canTalk = false;
            }
        }
        else
        {
            PlayerManager.instance.canAttack = true;
            canTalk = false;
        }
        if (Input.GetKeyDown(KeyCode.X) && canTalk)
        {
            Rest();
            PlayerManager.instance.canMove = false;
            mapImage.gameObject.SetActive(true);
        }
        //if(mapImage.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Z))
        //{
        //    mapImage.gameObject.SetActive(false);
        //    PlayerManager.instance.gameObject.SetActive(false);
        //    Parameter.instance.gameObject.SetActive(false);
        //    SceneManager.LoadSceneAsync("99_LoadingScene");
        //}
    }

    private void Rest()
    {
        Parameter.instance.gameObject.GetComponent<WorldMapManager>().monsterSpawnCount = 0;
        PotionManager.Instance.currentPotions = Database.Instance.nowPlayer.potions;
        Parameter.instance.currentHp = Parameter.instance.hp;
        Parameter.instance.currentMp = Parameter.instance.mp;
        Parameter.instance.currentSp = Parameter.instance.sp;
        PotionManager.Instance.potion_Full_Img.gameObject.SetActive(true);
        PotionManager.Instance.potion_None_Img.gameObject.SetActive(false);
        PotionManager.Instance.potions.text = Database.Instance.nowPlayer.potions.ToString();
        Database.Instance.Save();
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        numberOfObject++;
    }

    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    if (other.transform.tag == "Monster" && !other.GetComponent<HostileNPC>().isDead)
    //    {
    //        PlayerManager.instance.canAttack = true;
    //        canTalk = false;
    //    }
    //    else if(other.transform.tag == "Monster" && other.GetComponent<HostileNPC>().isDead)
    //    {
    //        PlayerManager.instance.canAttack = false;
    //        canTalk = true;
    //    }
    //}

    private void OnTriggerExit2D(Collider2D other)
    {
        numberOfObject--;
        if (other.transform.name == "Player")
        {
            PlayerManager.instance.canAttack = true;
            canTalk = false;
        }
    }
}
