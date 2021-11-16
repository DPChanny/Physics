using UnityEngine;
using UnityEngine.SceneManagement;

public class LightManager : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lightRenderer;

    //�� �̵� ����
    private Vector2 direction = Vector2.right;

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
        LightSequence();
        started = true;
    }

    private void LightSequence()
    {

    }

    //���� ����
    public void FailedGame()
    {
        started = false;
    }

    //���� ����
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
