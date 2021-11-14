using UnityEngine;
using UnityEngine.SceneManagement;

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
    private Transform spawnPoint;
    //플레이어 프리팹
    [SerializeField]
    private GameObject playerPrefab;

    //플레이어
    private GameObject player;

    //게임 시작
    private void StartGame()
    {
        if(player != null)
        {
            Destroy(player);
        }
        player = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        started = true;
    }

    //게임 실패
    public void FailedGame()
    {
        started = false;
    }

    //게임 성공
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
            if (Input.GetKeyDown(Key.START))
            {
                StartGame();
            }
        }
    }
}
