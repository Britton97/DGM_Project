using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private void Awake()
    {
        SetUpSingleton();
    }

    public void LoadGameLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
    public void LoadMenuScreen()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu Screen");
    }

    public void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
