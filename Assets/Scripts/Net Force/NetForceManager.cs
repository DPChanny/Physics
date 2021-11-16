using UnityEngine;
using UnityEngine.SceneManagement;

public class NetForceManager : MonoBehaviour
{
    //물체 범위 렌더러
    [SerializeField]
    private LineRenderer objectRangeRenderer;
    //물체 범위 트리거
    [SerializeField]
    private CircleCollider2D objectRangeTrigger;

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

    private void Awake()
    {
        //렌더러 위치 개수 초기화
        objectRangeRenderer.positionCount = Public.setting.positionCount + 1;

        //물체 범위 트리거 초기화
        objectRangeTrigger.radius = Public.setting.netForceSetting.objectRange;
    }

    private void Start()
    {
        //물체 범위 렌더링
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

    //게임 시작 여부
    private bool started = false;
    public bool Started
    {
        get
        {
            return started;
        }
    }

    //게임 시작
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

        started = true;
    }

    //게임 종료
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
    }
}
