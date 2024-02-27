using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using UnityEditor;

public class Title : MonoBehaviour
{
    [SerializeField]
    private GameObject LoadGameBtn;
    private void Start()
    {
        Parameter.instance.gameObject.SetActive(false);
        PlayerManager.instance.gameObject.SetActive(false);
        if (!File.Exists(Database.Instance.path + Database.Instance.filename))
        {
            LoadGameBtn.SetActive(false);
        }
        else
            LoadGameBtn.SetActive(true);
    }
    public void NewGame()
    {
        Database.Instance.nowPlayer.destination = "05_TutorialScene";
        //PlayerManager.instance.gameObject.transform.SetParent(null);
        //Parameter.instance.gameObject.transform.SetParent(null) ;
        SceneManager.LoadSceneAsync("99_LoadingScene");
    }

    public void LoadGame()
    {
        Database.Instance.Load();
        SceneManager.LoadSceneAsync("99_LoadingScene");
    }

    public void QuitGame()
    {
#if !UNITY_EDITOR
        Application.Quit();
#else
        EditorApplication.isPlaying = false;
#endif

    }

    private void SetActiveAll()
    {
        PlayerManager.instance.gameObject.SetActive(true);
        Parameter.instance.gameObject.SetActive(true);
    }
}
