using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldMapManager : MonoBehaviour
{
    [SerializeField]
    private List<Image> mapList;
    [SerializeField]
    private Rigidbody2D mapPoint;

    [SerializeField]
    private int mapNum = 0;
    public string mapName;
    private void Update()
    {
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
    }

    private void CheckMap()
    {
        mapName = mapList[mapNum].gameObject.name;
        Vector3 _vec = new Vector3();
        _vec = mapList[mapNum].gameObject.transform.position;
        mapPoint.MovePosition(_vec);
    }
}
