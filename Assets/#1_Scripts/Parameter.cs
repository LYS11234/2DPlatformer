using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Parameter : MonoBehaviour
{
    public static Parameter instance;

    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }
    #endregion

    #region Components
    [Header("Components")]
    public Image hpImage;
    public Text hpText;
    public Image spImage;
    public Text spText;
    public Image mpImage;
    public Text mpText;
    public Image expImage;
    public Text expText;
    public Text levelText;
    #endregion

    #region Status
    [Header("Status")]
    public int hp;
    public int currentHp;
    public int sp;
    public int currentSp;
    public int mp;
    public int currentMp;
    public int exp;
    public int currentExp;
    public int level;
    public int currentLevel;
    #endregion

    #region Extra Variables
    [Header("Extra Variables")]
    [SerializeField]
    private float spRecovTime;
    [SerializeField]
    private float currentSpRecovTime;
    [SerializeField]
    private int spRecovery;
    #endregion


    private void Start()
    {
        Database.Instance.hp = hp;
        Database.Instance.sp = sp;
        Database.Instance.mp = mp;
        Database.Instance.exp = exp;
        Database.Instance.level = level;
        Database.Instance.currentExp = currentExp;
    }
    private void Update()
    {
        CheckParameters();
        CheckEXP();
        SPRecoveryTime();
    }

    private void CheckParameters()
    {
        hpImage.fillAmount = (float)currentHp / hp;
        hpText.text = $"{currentHp.ToString()} / {hp.ToString()}";
        spImage.fillAmount = (float)currentSp / sp;
        spText.text = $"{currentSp.ToString()} / {sp.ToString()}";
        mpImage.fillAmount = (float)currentMp / mp;
        mpText.text = $"{currentMp.ToString()} / {mp.ToString()}";
        expImage.fillAmount = (float)currentExp / exp;
        expText.text = $"{currentExp.ToString()} / {exp.ToString()}";
    }


    #region SP
    private void SPIncrease()
    {
        currentSp += spRecovery;
    }
    
    private void SPRecoveryTime()
    {
        if (currentSp < sp && !PlayerManager.instance.isAttack)
        {
            if (currentSpRecovTime >= spRecovTime)
            {
                if (sp < currentSp + spRecovery)
                    currentSp = sp;
                else
                    SPIncrease();
                currentSpRecovTime = 0;
            }
            else
                currentSpRecovTime += Time.deltaTime;
        }
        else if(PlayerManager.instance.isAttack || !PlayerManager.instance.isGround)
        {
            currentSpRecovTime = 0;
        }
    }
    #endregion
    #region Level
    private void CheckEXP()
    {
        if(currentExp >= exp)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        if (currentLevel < level)
        {
            while (currentExp >= exp)
            {
                currentExp -= exp;
                Database.Instance.currentExp = currentExp;
                currentLevel++;
                Database.Instance.level = currentLevel;
                levelText.text = currentLevel.ToString();
                hp += (int)((float)hp * 0.1f);
                currentHp = hp;
                mp += (int)((float)mp * 0.1f);
                currentMp = mp;
                sp += (int)((float)sp * 0.1f);
                currentSp = sp;
                exp += (int)((float)exp * 0.2f);
            }
        }
    }

    #endregion
}
