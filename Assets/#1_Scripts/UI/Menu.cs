using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private Image menu;

    // Update is called once per frame
    void Update()
    {
        VisibleMenu();
    }

    private void VisibleMenu()
    {
        if (!menu.gameObject.active)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                menu.gameObject.SetActive(true);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                menu.gameObject.SetActive(false);
            }
        }
    }

    public void QuitBtn()
    {
        Application.Quit();
    }
}
