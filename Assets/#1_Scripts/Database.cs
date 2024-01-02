using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor.Search;

public class PlayerData
{
    public int gold = 50;
    public string destination;
    public int additionalAtk = 0;
    public int upgradeCost = 50;
    public int potions = 5;
    public float restoreHealth = 20;
    public int level = 1;
    public int exp;
    public int currentExp = 0;
    public int hp;
    public int sp;
    public int mp;
    public string[] items_name = new string[48];
    public int[] itemCount = new int[48];
}
public class Database : MonoBehaviour
{
    public static Database Instance;

    public PlayerData nowPlayer = new PlayerData();

    [SerializeField]
    private Inventory theInven;

    string path;

    string filename = "save";
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
    private void Start()
    {
        Load();
    }

    public void Save()
    {
        string saveData = JsonUtility.ToJson(nowPlayer);

        File.WriteAllText(path+filename, saveData);
        print(path);
    }

    public void Load() 
    {
        string loaddata = File.ReadAllText(path + filename);
        nowPlayer = JsonUtility.FromJson<PlayerData>(loaddata);
        for (int i = 0; i < nowPlayer.items_name.Length; i++)
        {
            theInven.LoadToInven(i, nowPlayer.items_name[i], nowPlayer.itemCount[i]);
        }
    }
}
