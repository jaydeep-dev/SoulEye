using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
    public static void LoadLevel(Scenes sceneToLoad)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(sceneToLoad.ToString());
    }
}

public enum Scenes
{
    Level1,
    Level2,
    Level0,
    MainMenu,
}
