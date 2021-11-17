using UnityEngine;
using UnityEngine.SceneManagement;

public class LightManager : MonoBehaviour
{
    //�� ���� ����
    [SerializeField]
    private Transform lightSpawnPoint;
    //�� ������
    [SerializeField]
    private GameObject lightPrefab;
    //��
    private new GameObject light;

    //���� ���� ����
    private bool started = false;
    public bool Started
    {
        get
        {
            return started;
        }
    }

    //���� ����
    private void StartGame()
    {
        if (light != null)
        {
            Destroy(light);
        }
        light = Instantiate(lightPrefab, lightSpawnPoint.position, Quaternion.identity);

        started = true;
    }

    //���� ����
    public void FailedGame()
    {
        started = false;
    }

    //���� ����
    public void SucceedGame()
    {
        started = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(Key.EXIT))
        {
            SceneManager.LoadScene(SceneName.MAIN);
        }
        if (!started)
        {
            if (Input.GetKeyDown(Key.START))
            {
                StartGame();
            }
        }
    }
}
