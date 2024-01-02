using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DieMessage : MonoBehaviour
{
    public Image dieMessageBase;

    public Text dieMessage;

    public Text cheerMessage;

    private WaitForSeconds waitTime = new WaitForSeconds(1f);
    private float a;

    public Color color = new Color(171, 0, 0, 0);
    private void Update()
    {
        if (PlayerManager.instance.isDead && dieMessage.color.a < 1)
            StartCoroutine(ShowDeadMessage());
        else if (PlayerManager.instance.isDead && dieMessage.color.a >= 1)
            Revive();

    }

    private IEnumerator ShowDeadMessage()
    {
        a += 0.01f;
        color = new Color(171, 0, 0, a);
        Debug.Log($"alpha is {dieMessage.color.a}");
        dieMessage.color = color;
        
        yield return waitTime;

    }

    private void Revive()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            Parameter.instance.currentHp = Parameter.instance.hp;
            Parameter.instance.currentSp = Parameter.instance.sp;
            Parameter.instance.currentMp = Parameter.instance.mp;
            Parameter.instance.currentExp = 0;
            PotionManager.Instance.currentPotions = Database.Instance.nowPlayer.potions;
            PotionManager.Instance.potion_Full_Img.gameObject.SetActive(true);
            PotionManager.Instance.potion_None_Img.gameObject.SetActive(false);
            PotionManager.Instance.potions.text = Database.Instance.nowPlayer.potions.ToString();
            PlayerManager.instance.gameObject.transform.position = new Vector2(-1.338f, -0.419f);
            dieMessageBase.gameObject.SetActive(false);
            PlayerManager.instance.isDead = false;
            PlayerManager.instance.gameObject.SetActive(false);
            Parameter.instance.gameObject.SetActive(false);
            SceneManager.LoadSceneAsync("99_LoadingScene");
        }
    }

}
