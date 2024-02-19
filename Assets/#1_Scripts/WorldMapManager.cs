using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldMapManager : MonoBehaviour
{
    [SerializeField] public GameObject m_WorldMap;
    [SerializeField]
    private Image villiage;
    [SerializeField]
    private Image forest1;
    [SerializeField]
    public Image forest2;
    [SerializeField] 
    public Image forest3;
    [SerializeField]
    private Rigidbody2D mapPoint;
    [SerializeField]
    private Vector2 originPos;

    [SerializeField]
    private int mapNum = 0;
    [SerializeField]
    private string sceneName;
    [SerializeField]
    private int originMapNum = 0;

    private void Update()
    {
        if(m_WorldMap.activeSelf)
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
        else if(Input.GetKeyDown(KeyCode.X) && Database.Instance.nowPlayer.destination != sceneName)
        {
            PlayerManager.instance.gameObject.SetActive(false);
            Parameter.instance.gameObject.SetActive(false);
            Database.Instance.nowPlayer.destination = sceneName;
            originMapNum = mapNum;
            originPos = mapPoint.position;
            m_WorldMap.SetActive(false);
            SceneManager.LoadSceneAsync("99_LoadingScene");
        }
        else if(Input.GetKeyDown(KeyCode.Z))
        {
            mapNum = originMapNum;
            CheckMap();
            //mapPoint.position = originPos;
            //sceneName = Database.Instance.destination;

            m_WorldMap.SetActive(false);
            PlayerManager.instance.canMove = true;
            PlayerManager.instance.gameObject.SetActive(false);
            Parameter.instance.gameObject.SetActive(false);
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
                sceneName = villiage.gameObject.name;
                break;
            case 1:
                _vec.Set(forest1.transform.position.x, forest1.transform.position.y);
                sceneName = forest1.gameObject.name;
                break;
            case 2:
                if (forest2.gameObject.active)
                {
                    _vec.Set(forest2.transform.position.x, forest2.transform.position.y);
                    sceneName = forest2.gameObject.name;
                }
                else
                {
                    _vec.Set(forest1.transform.position.x, forest1.transform.position.y);
                    sceneName = forest1.gameObject.name;
                    mapNum = 1;
                }
                break;
            default:
                if (forest3.gameObject.active)
                {
                    _vec.Set(forest3.transform.position.x, forest3.transform.position.y);
                    sceneName = forest3.gameObject.name;
                }
                else
                {
                    _vec.Set(forest2.transform.position.x, forest2.transform.position.y);
                    sceneName = forest2.gameObject.name;
                    mapNum = 2;
                }
                
                break;

        }
        
        mapPoint.transform.position = _vec;
    }
}
