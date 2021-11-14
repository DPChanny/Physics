using UnityEngine;
using UnityEngine.SceneManagement;

public class NetForceManager : MonoBehaviour
{
    //��ü ���� ������Ʈ
    private Rigidbody2D objectRigidbody;

    //��ü ���� ������
    [SerializeField]
    private LineRenderer objectRangeRenderer;
    //��ü ���� Ʈ����
    [SerializeField]
    private CircleCollider2D objectRangeTrigger;

    //�÷��̾� ������
    [SerializeField]
    private GameObject playerPrefab;
    //�÷��̾� ���� ��ġ
    [SerializeField]
    private Transform playerSpawnPoint;
    //�÷��̾�
    private GameObject player;

    [SerializeField]
    //�� ������
    private GameObject enemyPrefab;
    //�� ���� ��ġ
    [SerializeField]
    private Transform enemySpawnPoint;
    //��
    private GameObject enemy;

    private void Awake()
    {
        //��ü ���� ������Ʈ �ʱ�ȭ
        objectRigidbody = GameObject.FindGameObjectWithTag(Tag.OBJECT).GetComponent<Rigidbody2D>();
        
        //��ü ���� Ʈ���� �ʱ�ȭ
        objectRangeTrigger.radius = Public.setting.netForceSetting.objectRange;

        //������ ��ġ ���� �ʱ�ȭ
        objectRangeRenderer.positionCount = Public.setting.positionCount + 1;
    }

    private void Start()
    {
        //��ü ���� ������
        float angle = 0f;

        for (int i = 0; i < Public.setting.positionCount + 1; i++)
        {
            objectRangeRenderer.SetPosition(
                i,
                new Vector2(
                    Mathf.Cos(Mathf.Deg2Rad * angle) * Public.setting.netForceSetting.objectRange,
                    Mathf.Sin(Mathf.Deg2Rad * angle) * Public.setting.netForceSetting.objectRange));

            angle += (360f / Public.setting.positionCount);
        }
    }

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
        objectRigidbody.velocity = Vector2.zero;
        objectRigidbody.position = Vector2.zero;

        objectRangeRenderer.startColor = Color.blue;
        objectRangeRenderer.endColor = Color.blue;

        if (player != null)
        {
            Destroy(player);
        }
        player = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);

        if (enemy != null)
        {
            Destroy(enemy);
        }
        enemy = Instantiate(enemyPrefab, enemySpawnPoint.position, Quaternion.identity);

        started = true;
    }

    //���� ����
    public void FinishedGame()
    {
        objectRangeRenderer.startColor = Color.red;
        objectRangeRenderer.endColor = Color.red;

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

    //��ü ���� ��Ż Ȯ��
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (started)
        {
            if (collider.CompareTag(Tag.OBJECT))
            {
                FinishedGame();
            }
        }
    }
}
