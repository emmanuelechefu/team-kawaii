using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoading
{
    public const string SceneLobby = "Lobby";
    public const string SceneTutorial = "Tutorial";
    public const string SceneLevel1 = "Level1";
    // add Level2, Level3 later

    public static void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void ReloadActive()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
