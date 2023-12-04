using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveSceneNPC : MonoBehaviour
{
    [SerializeField]
    private string mapName;
    [SerializeField]
    private LoadingBar loadingBar;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerManager.instance.canAttack = false;
            if(Input.GetKeyDown(KeyCode.X))
            {
                loadingBar.OpenLoadingBar();
                SceneManager.LoadSceneAsync(mapName);
                
            }
        }
    }

}
