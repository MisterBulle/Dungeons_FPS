using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerFunction : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
