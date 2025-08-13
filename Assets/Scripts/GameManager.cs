using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    public static void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
