using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveSceneNPC : MonoBehaviour
{
    [SerializeField]
    private string mapName;
    [SerializeField]
    private Image mapImage;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerManager.instance.canAttack = false;
            if(Input.GetKeyDown(KeyCode.X))
            {
                PlayerManager.instance.canMove = false;
                mapImage.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerManager.instance.canAttack = true;
            PlayerManager.instance.canMove = true;
        }
    }
}
