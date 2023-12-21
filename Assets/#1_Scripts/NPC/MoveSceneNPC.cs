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

    private void Start()
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

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
    }
}
