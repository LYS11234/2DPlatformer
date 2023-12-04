using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    [SerializeField]
    private Image loadingScene;
    [SerializeField]
    private Image loadingBar;

    private void Update()
    {
        
    }

    public void OpenLoadingBar()
    {
        loadingScene.gameObject.SetActive(true);
    }

    private void LoadingCheck()
    {

    }
}
