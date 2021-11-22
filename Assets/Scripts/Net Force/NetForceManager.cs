using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NetForceManager : MonoBehaviour
{
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
    private GameObject playerInstantiated;

    [SerializeField]
    //�� ������
    private GameObject enemyPrefab;
    //�� ���� ��ġ
    [SerializeField]
    private Transform enemySpawnPoint;
    //��
    private GameObject enemyInstantiated;

    //��ü ������
    [SerializeField]
    private GameObject objectPrefab;
    //��ü ���� ��ġ
    [SerializeField]
    private Transform objectSpawnPoint;
    //��ü
    private GameObject objectInstantiated;

    //���� �ؽ�Ʈ
    [SerializeField]
    private TextMeshProUGUI TextUI_score;
    //����
    private float score = 0f;
    //���� ���� �ð�
    private float scoreCoolTime = 0f;

    private void Awake()
    {
        //������ ��ġ ���� �ʱ�ȭ
        objectRangeRenderer.positionCount = Public.setting.positionCount + 1;

        //��ü ���� Ʈ���� �ʱ�ȭ
        objectRangeTrigger.radius = Public.setting.netForceSetting.objectRange - 0.5f;
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
        objectRangeRenderer.startColor = Color.blue;
        objectRangeRenderer.endColor = Color.blue;

        if (objectInstantiated != null)
        {
            DestroyImmediate(objectInstantiated);
        }
        objectInstantiated = Instantiate(objectPrefab, objectSpawnPoint.position, Quaternion.identity);

        if (playerInstantiated != null)
        {
            Destroy(playerInstantiated);
        }
        playerInstantiated = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);

        if (enemyInstantiated != null)
        {
            Destroy(enemyInstantiated);
        }
        enemyInstantiated = Instantiate(enemyPrefab, enemySpawnPoint.position, Quaternion.identity);

        score = 0f;
        scoreCoolTime = 0f;

        started = true;
    }

    //���� ����
    public void FinishGame()
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
        else
        {
            TextUI_score.text = score.ToString("n2");
            if (scoreCoolTime > 1)
            {
                scoreCoolTime = 0;
                score += 1 - 
                    (Vector3.Distance(
                        objectInstantiated.transform.position, 
                        objectSpawnPoint.position) / Public.setting.netForceSetting.objectRange);
            }
            else
            {
                scoreCoolTime += Time.deltaTime;
            }
        }
    }
}
