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

    private new void Start()
    {
        
        mapImage = Parameter.instance.gameObject.GetComponent<WorldMapManager>().m_WorldMap;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && canTalk)
        {
            Rest();
            PlayerManager.instance.canMove = false;
            mapImage.gameObject.SetActive(true);
        }
        //if(mapImage.gameObject.active && Input.GetKeyDown(KeyCode.Z))
        //{
        //    mapImage.gameObject.SetActive(false);
        //    PlayerManager.instance.gameObject.SetActive(false);
        //    Parameter.instance.gameObject.SetActive(false);
        //    SceneManager.LoadSceneAsync("99_LoadingScene");
        //}
    }

    private void Rest()
    {
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
        if (other.transform.name == "Player")
        {
            PlayerManager.instance.canAttack = false;
            canTalk = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.name == "Player")
        {
            PlayerManager.instance.canAttack = true;
            canTalk = false;
        }
    }
}
