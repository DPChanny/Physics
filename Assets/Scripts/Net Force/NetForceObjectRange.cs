using UnityEngine;

public class NetForceObjectRange : MonoBehaviour
{
    //합력 게임 매니저
    private NetForceManager gameManager;

    //물체 범위 렌더러
    [SerializeField]
    private LineRenderer objectRangeRenderer;
    //물체 범위 트리거
    [SerializeField]
    private CircleCollider2D objectRangeTrigger;

    private void Awake()
    {
        //합력 게임 매니저 초기화
        gameManager = GameObject.FindGameObjectWithTag(Tag.GAME_MANAGER).GetComponent<NetForceManager>();

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

    private void Update()
    {
        if (!gameManager.Started)
        {
            objectRangeRenderer.startColor = Color.red;
            objectRangeRenderer.endColor = Color.red;
        }
    }
}
