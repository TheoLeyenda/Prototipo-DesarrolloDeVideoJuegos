using UnityEngine.SceneManagement;

public static class LevelLoader 
{
    public static string prevLevel;
    public static string nextLevel;

    public static void LoadLevel(string name)
    {
        nextLevel = name;

        SceneManager.LoadScene("LoadScene");
    }
}
