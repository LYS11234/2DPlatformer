using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour
{
    public static Database Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    public int gold;
    public string destination;
    public int additionalAtk;
    public int upgradeCost;
    public int potions;
    public float restoreHealth;
    public int level;
    public int exp;
    public int currentExp;
    public int hp;
    public int sp;
    public int mp;
    public Item[] items;
    public int[] itemCount;
}
