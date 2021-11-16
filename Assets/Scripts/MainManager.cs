using UnityEngine;
using UnityEngine.SceneManagement;

//���� ȭ�� �Ŵ���
public class MainManager : MonoBehaviour
{
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
}
