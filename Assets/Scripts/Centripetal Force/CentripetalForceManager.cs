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
    private Transform spawnPoint;
    //�÷��̾� ������
    [SerializeField]
    private GameObject playerPrefab;

    //�÷��̾�
    private GameObject player;

    //���� ����
    private void StartGame()
    {
        if(player != null)
        {
            Destroy(player);
        }
        player = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
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
