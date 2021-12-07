using UnityEngine;
using UnityEngine.Events;

//���� ȭ�� �Ŵ���
public class MainManager : MonoBehaviour
{
    [SerializeField]
    private FadeManager outWall; //�ܺ�
    [SerializeField]
    private Transform window; //â�� ��ġ
    [SerializeField]
    private Transform middle; //�߾� ��ġ
    [SerializeField]
    private Transform bookshelf; //å�� ��ġ
    [SerializeField]
    private Transform main; //�� ��ġ
    [SerializeField]
    private Transform door; //�� ��ġ
    [SerializeField]
    private Transform desk; //å�� ��ġ

    //���� ī�޶� �Ŵ���
    private CameraManager mainCamera;

    private void Awake()
    {
        if (Public.record == null)
        {
            Public.LoadRecords();
        }

        //���� ī�޶� �Ŵ��� �ʱ�ȭ
        mainCamera = Camera.main.GetComponent<CameraManager>();
    }

    private CameraStatus cameraStatus = CameraStatus.Main;

    //���ɷ� ���� ����
    public void OnCentripetalForce()
    {
        Public.game = Game.CentripetalForce;
        LoadNote();
    }

    //�շ� ���� ����
    public void OnNetForce()
    {
        Public.game = Game.NetForce;
        LoadNote();
    }

    //�� ���� ����
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

    //â�� ��ư
    public void OnWindow()
    {
        SetCameraToWindow(
            new UnityAction(() => SetCameraToMiddle(
                new UnityAction(() => SetCameraToBookshelf(null)))));
    }

    //�� ��ư
    public void OnDoor()
    {
        Public.SaveRecords();
        Application.Quit();
    }

    //ī�޶� â�� ��ġ �̵�
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

    //ī�޶� �߾� ��ġ �̵�
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

    //ī�޶� �� ��ġ �̵�
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

    //ī�޶� å�� ��ġ �̵�
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


    //ī�޶� å�� ��ġ �̵�
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
