using UnityEngine;
using UnityEngine.Events;

//메인 화면 매니저
public class MainManager : MonoBehaviour
{
    [SerializeField]
    private FadeManager outWall; //외벽
    [SerializeField]
    private Transform window; //창문 위치
    [SerializeField]
    private Transform middle; //중앙 위치
    [SerializeField]
    private Transform bookshelf; //책장 위치
    [SerializeField]
    private Transform main; //주 위치
    [SerializeField]
    private Transform door; //문 위치
    [SerializeField]
    private Transform desk; //책상 위치

    //메인 카메라 매니저
    private CameraManager mainCamera;

    private void Awake()
    {
        if (Public.record == null)
        {
            Public.LoadRecords();
        }

        //메인 카메라 매니저 초기화
        mainCamera = Camera.main.GetComponent<CameraManager>();
    }

    private CameraStatus cameraStatus = CameraStatus.Main;

    //구심력 게임 실행
    public void OnCentripetalForce()
    {
        Public.game = Game.CentripetalForce;
        LoadNote();
    }

    //합력 게임 실행
    public void OnNetForce()
    {
        Public.game = Game.NetForce;
        LoadNote();
    }

    //빛 게임 샐행
    public void OnLight()
    {
        Public.game = Game.Light;
        LoadNote();
    }

    private void LoadNote()
    {
        SetCameraToMiddle(
            new UnityAction(() => SetCameraToDesk(
                new UnityAction(() => Public.LoadScene(SceneName.NOTE)))));
    }

    private void Update()
    {
        if (Input.GetKeyDown(Key.EXIT))
        {
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
                SetCameraToMiddle(
                    new UnityAction(() => SetCameraToWindow(
                        new UnityAction(() => SetCameraToMain(null)))));
                return;
            }
            if(cameraStatus == CameraStatus.Desk)
            {
                SetCameraToMiddle(
                    new UnityAction(() => SetCameraToBookshelf(null)));
                return;
            }
        }
    }

    //창문 버튼
    public void OnWindow()
    {
        SetCameraToWindow(
            new UnityAction(() => SetCameraToMiddle(
                new UnityAction(() => SetCameraToBookshelf(null)))));
    }

    //문 버튼
    public void OnDoor()
    {
        Public.SaveRecords();
        Application.Quit();
    }

    //카메라 창문 위치 이동
    private void SetCameraToWindow(UnityAction _action)
    {
        door.gameObject.SetActive(false);
        window.gameObject.SetActive(false);
        bookshelf.gameObject.SetActive(false);
        outWall.In(7.5f, null);
        mainCamera.MoveTo(window, 7.5f, _action);
        mainCamera.ZoomInOut(2f, 7.5f, null);
        cameraStatus = CameraStatus.Window;
    }

    //카메라 중앙 위치 이동
    private void SetCameraToMiddle(UnityAction _action)
    {
        door.gameObject.SetActive(false);
        window.gameObject.SetActive(false);
        bookshelf.gameObject.SetActive(false);
        outWall.Out(7.5f, null);
        mainCamera.MoveTo(middle, 7.5f, _action);
        mainCamera.ZoomInOut(3.5f, 7.5f, null);
        cameraStatus = CameraStatus.Middle;
    }

    //카메라 주 위치 이동
    private void SetCameraToMain(UnityAction _action)
    {
        door.gameObject.SetActive(true);
        window.gameObject.SetActive(true);
        bookshelf.gameObject.SetActive(false);
        outWall.In(7.5f, null);
        mainCamera.MoveTo(main, 7.5f, _action);
        mainCamera.ZoomInOut(10f, 7.5f, null);
        cameraStatus = CameraStatus.Main;
    }

    //카메라 책장 위치 이동
    private void SetCameraToBookshelf(UnityAction _action)
    {
        door.gameObject.SetActive(false);
        window.gameObject.SetActive(false);
        bookshelf.gameObject.SetActive(true);
        outWall.Out(7.5f, null);
        mainCamera.MoveTo(bookshelf, 7.5f, _action);
        mainCamera.ZoomInOut(1.5f, 7.5f, null);
        cameraStatus = CameraStatus.Bookshelf;
    }


    //카메라 책상 위치 이동
    private void SetCameraToDesk(UnityAction _action)
    {
        door.gameObject.SetActive(false);
        window.gameObject.SetActive(false);
        bookshelf.gameObject.SetActive(true);
        outWall.Out(7.5f, null);
        mainCamera.MoveTo(desk, 7.5f, _action);
        mainCamera.ZoomInOut(1.5f, 7.5f, null);
        cameraStatus = CameraStatus.Desk;
    }
}
