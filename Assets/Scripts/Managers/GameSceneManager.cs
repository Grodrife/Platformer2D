using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager s_gameSceneManager;
    private void Awake()
    {
        if (s_gameSceneManager == null)
        {
            s_gameSceneManager = this;
            DontDestroyOnLoad(gameObject);
        } else if (s_gameSceneManager != this)
        {
            Destroy(gameObject);
        }
    }
    public AsyncOperation ResetCurrentScene()
    {
        return SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
    public AsyncOperation ChangeScene(string sceneToLoad)
    {
        Debug.Log("cargando escena " + sceneToLoad);
        return SceneManager.LoadSceneAsync(sceneToLoad);
    }
    public AsyncOperation LoadGame()
    {
        return SceneManager.LoadSceneAsync("WorldScene01");
    }
    public AsyncOperation ReturnMainMenu()
    {
        return SceneManager.LoadSceneAsync("MainMenuScene");
    }
}
