using UnityEngine;

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

    //���� 
    public void FailedGame()
    {
        started = false;
    }

    public void SucceedGame()
    {
        started = false;
    }

    private void Update()
    {
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
