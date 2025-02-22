using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public static class LevelLoader
{
    public static void LoadLevel(string levelName)
    {
        if (Application.CanStreamedLevelBeLoaded(levelName))
        {
            SceneManager.LoadScene(levelName);
        }
        else
        {
            Debug.LogWarning("LEVELLOADER LoadLevel Error: invalid scene specified");
        }
    }

    public static void LoadLevel(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(levelIndex);
        }
        else
        {
            Debug.LogWarning("LEVELLOADER LoadLevel Error: invalid scene specified");
        }

    }

    public static IEnumerator LoadLevelAsync(int levelIndex, UnityAction action = null)
    {
        if (levelIndex >= 0 && levelIndex < SceneManager.sceneCountInBuildSettings)
        {
            yield return SceneManager.LoadSceneAsync(levelIndex);
            action?.Invoke();
        }
        else
        {
            Debug.LogWarning("LEVELLOADER LoadLevel Error: invalid scene specified");
            yield return null;
        }
    }

    public static void ReloadLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public static IEnumerator ReloadLevelAsync(UnityAction action = null)
    {
        yield return SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        action?.Invoke();
    }
}
