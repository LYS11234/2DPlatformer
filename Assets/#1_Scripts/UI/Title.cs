using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField]
    private GameObject LoadGameBtn;
    private void Start()
    {
        Parameter.instance.gameObject.SetActive(false);
        if(!System.IO.Directory.Exists(Application.persistentDataPath + "/" + Database.Instance.filename))
            LoadGameBtn.SetActive(false);
    }
    public void NewGame()
    {
        Database.Instance.nowPlayer.destination = "01_Villiage";
        PlayerManager.instance.gameObject.transform.SetParent(null);
        Parameter.instance.gameObject.transform.SetParent(null) ;
        SceneManager.LoadSceneAsync("99_LoadingScene");
    }

    public void LoadGame()
    {
        Database.Instance.Load();
        SceneManager.LoadSceneAsync("99_LoadingScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
