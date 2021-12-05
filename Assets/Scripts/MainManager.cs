using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

//메인 화면 매니저
public class MainManager : MonoBehaviour
{
    [SerializeField]
    private GameObject outWall;
    [SerializeField]
    private Transform window;
    [SerializeField]
    private Transform middle;
    [SerializeField]
    private Transform bookshelf;
    [SerializeField]
    private Transform main;

    private enum CameraStatus
    {
        Main,
        Middle,
        Window,
        Bookshelf
    }

    private CameraStatus cameraStatus = CameraStatus.Main;

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

    private void Update()
    {
        if (Input.GetKeyDown(Key.EXIT))
        {
            if(cameraStatus == CameraStatus.Main)
            {
                Application.Quit();
                return;
            }
            if(cameraStatus == CameraStatus.Middle)
            {
                SetCameraToWindow(
                        new UnityAction(() => SetCameraToMain(null)));
                return;
            }
            if (cameraStatus == CameraStatus.Window)
            {
                SetCameraToMain(null);
                return;
            }
            if (cameraStatus == CameraStatus.Bookshelf)
            {
                SetCameraToMiddle(null);
                return;
            }
        }
    }

    public void OnWindow()
    {
        SetCameraToWindow(
            new UnityAction(() => SetCameraToMiddle(null)));
    }

    public void OnDoor()
    {
        Application.Quit();
    }

    public void OnBookshelf()
    {
        SetCameraToBookshelf(null);
    }

    private void SetCameraToWindow(UnityAction _action)
    {
        window.gameObject.SetActive(false);
        bookshelf.gameObject.SetActive(false);
        outWall.GetComponent<FadeManager>().In(7.5f, null);
        Camera.main.GetComponent<CameraManager>().MoveTo(window, 7.5f, _action);
        Camera.main.GetComponent<CameraManager>().ZoomInOut(2f, 7.5f, null);
        cameraStatus = CameraStatus.Window;
    }

    private void SetCameraToMiddle(UnityAction _action)
    {
        window.gameObject.SetActive(false);
        bookshelf.gameObject.SetActive(true);
        outWall.GetComponent<FadeManager>().Out(7.5f, null);
        Camera.main.GetComponent<CameraManager>().MoveTo(middle, 7.5f, _action);
        Camera.main.GetComponent<CameraManager>().ZoomInOut(3.5f, 7.5f, null);
        cameraStatus = CameraStatus.Middle;
    }

    private void SetCameraToMain(UnityAction _action)
    {
        window.gameObject.SetActive(true);
        bookshelf.gameObject.SetActive(false);
        outWall.GetComponent<FadeManager>().In(7.5f, null);
        Camera.main.GetComponent<CameraManager>().MoveTo(main, 7.5f, _action);
        Camera.main.GetComponent<CameraManager>().ZoomInOut(10f, 7.5f, null);
        cameraStatus = CameraStatus.Main;
    }

    public void SetCameraToBookshelf(UnityAction _action)
    {
        window.gameObject.SetActive(false);
        bookshelf.gameObject.SetActive(false);
        outWall.GetComponent<FadeManager>().Out(7.5f, null);
        Camera.main.GetComponent<CameraManager>().MoveTo(bookshelf, 7.5f, _action);
        Camera.main.GetComponent<CameraManager>().ZoomInOut(1.5f, 7.5f, null);
        cameraStatus = CameraStatus.Bookshelf;
    }
}
