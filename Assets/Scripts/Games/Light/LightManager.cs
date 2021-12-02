using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LightManager : MonoBehaviour
{
    //빛 생성 지점
    [SerializeField]
    private Transform lightSpawnPoint;
    //빛 프리팹
    [SerializeField]
    private GameObject lightPrefab;
    //빛
    private new GameObject light;

    //경과 시간 텍스트
    [SerializeField]
    private TextMeshProUGUI TextUI_elapsedTime;
    //경과 시간
    private float elapsedTime = 0f;

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
        if (light != null)
        {
            Destroy(light);
        }
        light = Instantiate(lightPrefab, lightSpawnPoint.position, Quaternion.identity);

        elapsedTime = 0f;

        started = true;
    }

    //게임 종료
    public void FinishGame()
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
        else
        {
            elapsedTime += Time.deltaTime;
            TextUI_elapsedTime.text = elapsedTime.ToString("n2");
        }
    }
}
