using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField]
    public string nextScene;

    private void Start()
    {
        loadingBar.fillAmount = 0;
        nextScene = Database.Instance.nowPlayer.destination;
        LoadingCheck();
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
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0.0f;
        StartCoroutine(LoadNextScene(timer, op));
    }

    IEnumerator LoadNextScene(float _timer, AsyncOperation _op)
    {
        while(!_op.isDone)
        {
            yield return null;
            _timer += Time.deltaTime;
            if(_op.progress < 0.9f)
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, _op.progress, _timer);
                if(loadingBar.fillAmount >= 0.9f)
                    _timer = 0.0f;
            }
            else
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, 1f, _timer);
                if(loadingBar.fillAmount == 1f)
                {
                    _op.allowSceneActivation = true;
                    yield break;
                }
            }
        }

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
