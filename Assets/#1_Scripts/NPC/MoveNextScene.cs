using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveNextScene : MonoBehaviour
{
    [SerializeField]
    private string nextMap;

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
            Parameter.instance.GetComponent<WorldMapManager>().forest2.gameObject.SetActive(true);
            Database.Instance.nowPlayer.clearedLevel = 1;
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
