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
    [SerializeField]
    private Text text;
    private WaitForSeconds waitTime = new WaitForSeconds(1f);
    [SerializeField]
    private float loadTime;
    [SerializeField]
    private float currentLoadTime;

    private void Start()
    {
        loadingBar.fillAmount = 0;
    }
    private void Update()
    {
        LoadingText();
    }

    public void OpenLoadingBar()
    {
        loadingScene.gameObject.SetActive(true);
    }

    private void LoadingCheck()
    {

    }

    private void LoadingText()
    {
        if (currentLoadTime >= loadTime)
        {
            if (text.text == "Loading...")
                text.text = "Loading";
            else
                text.text += ".";
            currentLoadTime = 0;
        }
        else
            currentLoadTime += Time.deltaTime;
    }

}
