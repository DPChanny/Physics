using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NetForceManager : MonoBehaviour
{
    //플레이어 프리팸
    [SerializeField]
    private GameObject playerPrefab;
    //플레이어 생성 위치
    [SerializeField]
    private Transform playerSpawnPoint;
    //플레이어
    private GameObject playerInstantiated;

    [SerializeField]
    //적 프리팹
    private GameObject enemyPrefab;
    //적 생성 위치
    [SerializeField]
    private Transform enemySpawnPoint;
    //적
    private GameObject enemyInstantiated;

    //물체 프리팹
    [SerializeField]
    private GameObject objectPrefab;
    //물체 생성 위치
    [SerializeField]
    private Transform objectSpawnPoint;
    //물체
    private GameObject objectInstantiated;

    //물체 범위 프리팹
    [SerializeField]
    private GameObject objectRangePrefab;
    //물체 범위 생성 위치
    [SerializeField]
    private Transform objectRangeSpawnPoint;
    //물체 범위
    private GameObject objectRangeInstantiated;

    //점수 텍스트
    [SerializeField]
    private TextMeshProUGUI TextUI_score;
    //점수
    private float score = 0f;
    //점수 지연 시간
    private float scoreCoolTime = 0f;

    //게임 시작 여부
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


    //게임 시작
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

    //게임 종료
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
