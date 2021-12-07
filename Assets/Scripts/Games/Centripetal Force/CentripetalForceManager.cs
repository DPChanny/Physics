using UnityEngine;

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

    private CentripetalForceDifficultyManager difficultyManager;

    private void Awake()
    {
        difficultyManager = GameObject.FindGameObjectWithTag(Tag.DIFFICULTY_MANAGER).GetComponent<CentripetalForceDifficultyManager>();
    }

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
