using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public void OnCentripetalForce()
    {
        SceneManager.LoadScene(SceneName.CENTRIPETAL_FORCE);
    }

    public void OnNetForce()
    {
        SceneManager.LoadScene(SceneName.NET_FORCE);
    }
}
