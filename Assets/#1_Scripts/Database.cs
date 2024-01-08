using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerData
{
    public int gold = 50;
    public string destination;
    public int additionalAtk = 0;
    public int upgradeCost = 50;
    public int potions = 5;
    public float restoreHealth = 20;
    public int level = 1;
    public int exp = 100;
    public int currentExp = 0;
    public int hp = 100;
    public int sp = 1000;
    public int mp = 100;
    public string[] items_name = new string[48];
    public int[] itemCount = new int[48];
    public int clearedLevel;
    public int[] store_ItemCount = new int[48];
    public int oldmanStep;
}
public class Database : MonoBehaviour
{
    public static Database Instance;

    public PlayerData nowPlayer = new PlayerData();

    [SerializeField]
    public Inventory theInven;
    [SerializeField]
    public Store store;

    public string path;

    public string filename = "save";
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);

        path = Application.persistentDataPath + "/";
    }

    public void Save()
    {
        string saveData = JsonUtility.ToJson(nowPlayer);

        File.WriteAllText(path+filename, saveData);
        print(path + filename);
    }

    public void Load() 
    {
        string loaddata = File.ReadAllText(path + filename);
        nowPlayer = JsonUtility.FromJson<PlayerData>(loaddata);
        for (int i = 0; i < store.store_Slots.Length; i++)
        {
            store.store_Slots[i].itemCount = nowPlayer.store_ItemCount[i];
            if (store.store_Slots[i].item != null)
                store.store_Slots[i].text_Count.text = store.store_Slots[i].itemCount.ToString();
        }
        for (int i = 0; i < Database.Instance.nowPlayer.items_name.Length; i++)
        {
            Debug.Log(Database.Instance.nowPlayer.items_name[i]);
            Database.Instance.theInven.LoadToInven(i, Database.Instance.nowPlayer.items_name[i], Database.Instance.nowPlayer.itemCount[i]);
        }
        Parameter.instance.hp = Database.Instance.nowPlayer.hp;
        Parameter.instance.currentHp = Parameter.instance.hp;
        Parameter.instance.sp = Database.Instance.nowPlayer.sp;
        Parameter.instance.currentSp = Parameter.instance.sp;
        Parameter.instance.mp = Database.Instance.nowPlayer.mp;
        Parameter.instance.currentMp = Parameter.instance.mp;
        Parameter.instance.exp = Database.Instance.nowPlayer.exp;
        Parameter.instance.currentLevel = Database.Instance.nowPlayer.level;
        Parameter.instance.currentExp = Database.Instance.nowPlayer.currentExp;
        Parameter.instance.levelText.text = Parameter.instance.currentLevel.ToString();
    }
}
