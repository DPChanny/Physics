using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

//메인 화면 매니저
public class MainManager : MonoBehaviour
{
    [SerializeField]
    private Transform window;
    [SerializeField]
    private GameObject outWall;

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

    public void OnWindow()
    {
        Camera.main.GetComponent<CameraManager>().MoveTo(window, 2.5f, new UnityAction(OnOutWallOut));
        Camera.main.GetComponent<CameraManager>().ZoomInOut(5, 2.5f, null);
    }

    public void OnOutWallOut()
    {
        outWall.GetComponent<FadeManager>().Out(2.5f, null);
    }
}
