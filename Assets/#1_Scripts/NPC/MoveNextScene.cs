using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveNextScene : MonoBehaviour
{
    public string nextMap;

    [SerializeField]
    private bool canTalk;

    private void Update()
    {
        if (canTalk)
            MoveScene();
    }

    private void MoveScene()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            Database.Instance.nowPlayer.destination = nextMap;
            if (nextMap == "03_Forest")
            {
                WorldMapManager worldMap = Parameter.instance.GetComponent<WorldMapManager>();
                worldMap.forest2.gameObject.SetActive(true);
                worldMap.mapPoint.transform.position = worldMap.forest2.gameObject.transform.position;
                Database.Instance.nowPlayer.clearedLevel = 1;
            }
            else if(nextMap == "04_BanditCave")
            {
                WorldMapManager worldMap = Parameter.instance.GetComponent<WorldMapManager>();
                worldMap.forest3.gameObject.SetActive(true);
                worldMap.mapPoint.transform.position = worldMap.forest3.transform.position;
                Database.Instance.nowPlayer.clearedLevel = 2;
            }
            Database.Instance.Save();
            SceneManager.LoadSceneAsync("99_LoadingScene");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            canTalk = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            canTalk = false;
    }
}
