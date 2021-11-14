using UnityEngine;
using UnityEngine.SceneManagement;

public class NetForceManager : MonoBehaviour
{
    //물체 범위
    [SerializeField]
    private float objectRange;
    //물체 범위 렌더러
    [SerializeField]
    private LineRenderer objectRangeRenderer;
    //렌더러 모서리 개수
    [SerializeField]
    private int segments;

    //물체 범위 트리거
    [SerializeField]
    private CircleCollider2D objectRangeTrigger;

    //게임 시작 여부
    private bool started = false;
    public bool Started
    {
        get
        {
            return started;
        }
    }

    //게임 시작 함수
    private void StartGame()
    {
        //물체 범위 트리거 초기화
        objectRangeTrigger.radius = objectRange;

        //렌더러 모서리 개수 초기화
        objectRangeRenderer.positionCount = segments + 1;

        //트랙 범위 렌더링
        float angle = 20f;

        for (int i = 0; i < segments + 1; i++)
        {
            objectRangeRenderer.SetPosition(
                i,
                new Vector2(
                    Mathf.Cos(Mathf.Deg2Rad * angle) * objectRange,
                    Mathf.Sin(Mathf.Deg2Rad * angle) * objectRange));

            angle += (360f / segments);
        }

        started = true;
    }

    //게임 실패 함수
    public void FailedGame()
    {
        started = false;
    }

    //게임 성공 함수
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
            //게임 시작 버튼 클릭 시 게임 시작
            if (Input.GetKeyDown(Key.START))
            {
                StartGame();
            }
        }
    }

    //오브젝트 범위 이탈 확인
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (started)
        {
            if (collider.CompareTag(Tag.OBJECT))
            {
                FailedGame();
            }
        }
    }
}
