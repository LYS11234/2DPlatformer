using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveSceneNPC : MonoBehaviour
{
    [SerializeField]
    private string mapName;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerManager.instance.canAttack = false;
            if(Input.GetKeyDown(KeyCode.X))
            {
                Parameter.instance.gameObject.SetActive(false);
                PlayerManager.instance.gameObject.SetActive(false);
                SceneManager.LoadSceneAsync(mapName);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            PlayerManager.instance.canAttack = true;
    }

}
