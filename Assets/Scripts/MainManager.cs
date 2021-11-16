using UnityEngine;
using UnityEngine.SceneManagement;

//메인 화면 매니저
public class MainManager : MonoBehaviour
{
    //구심력 게임 실행
    public void OnCentripetalForce()
    {
        SceneManager.LoadScene(SceneName.CENTRIPETAL_FORCE);
    }

    //합력 게임 실행
    public void OnNetForce()
    {
        SceneManager.LoadScene(SceneName.NET_FORCE);
    }

    //빛 게임 샐행
    public void OnLight()
    {
        SceneManager.LoadScene(SceneName.LIGHT);
    }
}
