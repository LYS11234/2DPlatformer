using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFineder : MonoBehaviour
{
    public static MapFineder Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }
    int a = 0;
    private void Start()
    {
        a = 0;
    }
}
