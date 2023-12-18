using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldMapManager : MonoBehaviour
{
    [SerializeField] private GameObject m_WorldMap;
    [SerializeField]
    private Image villiage;
    [SerializeField]
    private Image forest1;
    [SerializeField]
    private Image forest2;
    [SerializeField] 
    private Image forest3;
    [SerializeField]
    private Rigidbody2D mapPoint;

    [SerializeField]
    private int mapNum = 0;
    [SerializeField]
    private string sceneName;
    private void Update()
    {
        if(m_WorldMap.active)
            ChangeMap();
    }

    private void ChangeMap()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(mapNum != 0)
                mapNum--;
            CheckMap();
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (mapNum != 3)
                mapNum++;
            CheckMap();
        }
        if(Input.GetKeyDown(KeyCode.X) && Database.Instance.destination != sceneName)
        {
            PlayerManager.instance.gameObject.SetActive(false);
            Parameter.instance.gameObject.SetActive(false);
            sceneName = Database.Instance.destination;
            m_WorldMap.SetActive(false);
            SceneManager.LoadSceneAsync("99_LoadingScene");
        }
    }

    private void CheckMap()
    {
        Vector2 _vec = new Vector2();
        switch (mapNum)
        {
            case 0: 
                _vec.Set(villiage.transform.position.x, villiage.transform.position.y);
                Database.Instance.destination = villiage.gameObject.name;
                break;
            case 1:
                _vec.Set(forest1.transform.position.x, forest1.transform.position.y);
                Database.Instance.destination = forest1.gameObject.name;
                break;
            case 2:
                _vec.Set(forest2.transform.position.x, forest2.transform.position.y);
                Database.Instance.destination = forest2.gameObject.name;
                break;
            default:
                _vec.Set(forest3.transform.position.x, forest3.transform.position.y);
                Database.Instance.destination = forest3.gameObject.name;
                break;

        }
        
        mapPoint.position = _vec;
    }
}
