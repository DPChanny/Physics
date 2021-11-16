using UnityEngine;
using UnityEngine.SceneManagement;

public class LightManager : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lightRenderer;

    //빛 이동 방향
    private Vector2 direction = Vector2.right;

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
        LightSequence();
        started = true;
    }

    private void LightSequence()
    {

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
