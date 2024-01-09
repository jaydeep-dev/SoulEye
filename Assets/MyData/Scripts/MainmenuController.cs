using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainmenuController : MonoBehaviour
{

    [SerializeField] private LeanPopup settingsPopup;

    public void OnPlayBtnClicked()
    {
        GameManager.LoadLevel(Scenes.Level0);
    }

    public void OnQuitBtnClicked()
    {
        Application.Quit();
    }

    public void OnSettingsBtnClicked()
    {
        settingsPopup.Open();
    }
}
