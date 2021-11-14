using UnityEngine;
using UnityEngine.SceneManagement;

//���ɷ� ���� �Ŵ���
public class CentripletalForceManager : MonoBehaviour
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

    //Ʈ�� ����
    private float trackRange = 2.5f;
    public float TrackRange
    {
        get
        {
            return trackRange;
        }
    }

    [SerializeField]
    private Vector2 direction;

    //�÷��̾� ���� ����
    [SerializeField]
    private Transform spawnPoint;
    //�÷��̾� ������
    [SerializeField]
    private GameObject playerPrefab;

    //�÷��̾�
    private CentripetalForcePlayer player;

    //���� ���� �Լ�
    private void StartGame()
    {
        if(player != null)
        {
            Destroy(player.gameObject);
        }
        player = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity).GetComponent<CentripetalForcePlayer>();
        player.direction = direction;
        started = true;
    }

    //���� ���� �Լ�
    public void FailedGame()
    {
        started = false;
    }

    //���� ���� �Լ�
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
            //���� ���� ��ư Ŭ�� �� ���� ����
            if (Input.GetKeyDown(Key.START))
            {
                StartGame();
            }
        }
    }
}
