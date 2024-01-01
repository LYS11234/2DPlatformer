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
    private Image mapImage;
    [SerializeField]
    private bool canTalk;

    private new void Start()
    {
        mapImage = MapFineder.Instance.gameObject.GetComponent<Image>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && canTalk)
        {
            Rest();
            PlayerManager.instance.canMove = false;
            mapImage.gameObject.SetActive(true);
        }
    }

    private void Rest()
    {
        PotionManager.Instance.currentPotions = Database.Instance.potions;
        Parameter.instance.currentHp = Parameter.instance.hp;
        Parameter.instance.currentMp = Parameter.instance.mp;
        Parameter.instance.currentSp = Parameter.instance.sp;
        PotionManager.Instance.potion_Full_Img.gameObject.SetActive(true);
        PotionManager.Instance.potion_None_Img.gameObject.SetActive(false);
        PotionManager.Instance.potions.text = Database.Instance.potions.ToString();
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
