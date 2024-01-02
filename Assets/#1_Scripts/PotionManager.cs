using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionManager : MonoBehaviour
{
    public static PotionManager Instance;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
        {
            Destroy(this.gameObject);
        }
    }

    [SerializeField]
    public Image potion_Full_Img;
    [SerializeField]
    public Image potion_None_Img;
    [SerializeField]
    public Text potions;
    public int currentPotions;

    private void Start()
    {
        potions.text = currentPotions.ToString();
    }
    void Update()
    {
        DrinkPotion();
    }

    private void DrinkPotion()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl) && PlayerManager.instance.canMove)
        {
            //PlayerManager.instance.canAttack = false;
            StartCoroutine(DrinkCoroutine());
        }
    }

    private IEnumerator DrinkCoroutine()
    {
        if(currentPotions > 0)
        {
            currentPotions--;
            if (Parameter.instance.currentHp + Database.Instance.nowPlayer.restoreHealth >= Parameter.instance.hp)
            {
                Parameter.instance.currentHp = Parameter.instance.hp;
                
            }
            else
                Parameter.instance.currentHp += (int)Database.Instance.nowPlayer.restoreHealth;
            potions.text = currentPotions.ToString();
            if (currentPotions == 0)
            {
                potion_Full_Img.gameObject.SetActive(false);
                potion_None_Img.gameObject.SetActive(true);
            }
        }
        else
            StartCoroutine(DrinkCancelCoroutine());
        yield return null;
    }

    private IEnumerator DrinkCancelCoroutine()
    {
        yield return null;
    }    
}
