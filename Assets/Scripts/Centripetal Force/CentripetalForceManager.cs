using UnityEngine;
using UnityEngine.SceneManagement;

//���ɷ� ���� �Ŵ���
public class CentripetalForceManager : MonoBehaviour
{
    //���� ���� ����
    private bool started = false;
    public bool Started
    {
        get
        {
            return started;
        }
    }

    //�÷��̾� ���� ����
    [SerializeField]
    private Transform playerSpawnPoint;
    //�÷��̾� ������
    [SerializeField]
    private GameObject playerPrefab;
    //�÷��̾�
    private GameObject playerInstantiated;

    //���� ����
    private void StartGame()
    {
        if(playerInstantiated != null)
        {
            Destroy(playerInstantiated);
        }
        playerInstantiated = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
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
    }
}
