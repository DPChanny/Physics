using UnityEngine;

//구심력 게임 매니저
public class CentripletalForceManager : MonoBehaviour
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

    //트랙 범위
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

    //플레이어 생성 지점
    [SerializeField]
    private Transform spawnPoint;
    //플레이어 프리팹
    [SerializeField]
    private GameObject playerPrefab;

    //플레이어
    private CentripetalForcePlayer player;

    //게임 시작 함수
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

    //실패 
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
            //게임 시작 버튼 클릭 시 게임 시작
            if (Input.GetKeyDown(Key.START))
            {
                StartGame();
            }
        }
    }
}
