using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NetForceManager : MonoBehaviour
{
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

    //��ü ���� ������
    [SerializeField]
    private GameObject objectRangePrefab;
    //��ü ���� ���� ��ġ
    [SerializeField]
    private Transform objectRangeSpawnPoint;
    //��ü ����
    private GameObject objectRangeInstantiated;

    //���� �ؽ�Ʈ
    [SerializeField]
    private TextMeshProUGUI TextUI_score;
    //����
    private float score = 0f;
    //���� ���� �ð�
    private float scoreCoolTime = 0f;

    //���� ���� ����
    private bool started = false;
    public bool Started
    {
        get
        {
            return started;
        }
    }

    private NetForceDifficultyManager difficultyManager;

    private void Awake()
    {
        difficultyManager = GameObject.FindGameObjectWithTag(Tag.DIFFICULTY_MANAGER).GetComponent<NetForceDifficultyManager>();
    }


    //���� ����
    private void StartGame()
    {
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

        if (objectRangeInstantiated != null)
        {
            Destroy(objectRangeInstantiated);
        }
        objectRangeInstantiated = Instantiate(objectRangePrefab, objectRangeSpawnPoint.position, Quaternion.identity);

        score = 0f;
        scoreCoolTime = 0f;

        started = true;
    }

    //���� ����
    public void FinishGame()
    {
        Public.record.netForceRecords.Add(
            new NetForceRecord(
                Public.setting.netForceDifficulty,
                Public.setting.netForceSetting,
                score));
        started = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(Key.EXIT))
        {
            Public.LoadScene(SceneName.NOTE);
        }
        if (!difficultyManager.Active) {
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
}
