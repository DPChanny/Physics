using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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

    //��� �ð� �ؽ�Ʈ
    [SerializeField]
    private TextMeshProUGUI TextUI_elapsedTime;
    //��� �ð�
    private float elapsedTime = 0f;

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

        elapsedTime = 0f;

        started = true;
    }

    //���� ����
    public void FinishGame()
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
        else
        {
            elapsedTime += Time.deltaTime;
            TextUI_elapsedTime.text = elapsedTime.ToString("n2");
        }
    }
}
