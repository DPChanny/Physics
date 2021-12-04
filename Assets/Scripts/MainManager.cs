using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

//���� ȭ�� �Ŵ���
public class MainManager : MonoBehaviour
{
    [SerializeField]
    private Transform window;
    [SerializeField]
    private GameObject outWall;

    //���ɷ� ���� ����
    public void OnCentripetalForce()
    {
        SceneManager.LoadScene(SceneName.CENTRIPETAL_FORCE);
    }

    //�շ� ���� ����
    public void OnNetForce()
    {
        SceneManager.LoadScene(SceneName.NET_FORCE);
    }

    //�� ���� ����
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
