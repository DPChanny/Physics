using UnityEngine;

//구심력 게임 매니저
public class CentripetalForceManager : MonoBehaviour
{
    //게임 시작 여부
    private bool started = false;
    public bool Started
    {
        get
        {
            return started;
        }
    }

    //플레이어 생성 지점
    [SerializeField]
    private Transform playerSpawnPoint;
    //플레이어 프리팹
    [SerializeField]
    private GameObject playerPrefab;
    //플레이어
    private GameObject playerInstantiated;

    private CentripetalForceDifficultyManager difficultyManager;

    private void Awake()
    {
        difficultyManager = GameObject.FindGameObjectWithTag(Tag.DIFFICULTY_MANAGER).GetComponent<CentripetalForceDifficultyManager>();
    }

    //게임 시작
    private void StartGame()
    {
        if(playerInstantiated != null)
        {
            Destroy(playerInstantiated);
        }
        playerInstantiated = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
        started = true;
    }

    //게임 종료
    public void FinishGame(bool _succeed)
    {
        Public.record.centripetalForceRecords.Add(
            new CentripetalForceRecord(
                Public.setting.centripetalForceDifficulty, 
                Public.setting.centripetalForceSetting, 
                _succeed));
        started = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(Key.EXIT))
        {
            Public.LoadScene(SceneName.NOTE);
        }
        if (!started && !difficultyManager.Active)
        {
            if (Input.GetKeyDown(Key.START))
            {
                StartGame();
            }
        }
    }
}
