using System.Collections;
using UnityEngine;

//구심력 게임 플레이어
public class CentripetalForcePlayer : MonoBehaviour
{
    //구심력 게임 매니저
    private CentripetalForceManager gameManager;

    //플레이어 물리 컴포넌트
    private Rigidbody2D playerRigidbody;
    //플레이어 이동 방향
    private Vector2 direction = Vector2.right;

    //회전축 유무
    private bool anchored = false;
    //회전축 위치
    private Vector2 anchorPosition;
    //회전축 거리
    private float anchorDistance; 
    //회전축 회전 방향
    private float rotateDirection;
    //회전축 회전 궤도 렌더러
    [SerializeField]
    private LineRenderer anchorRotateRangeRenderer;

    //트랙 범위 렌더러
    [SerializeField]
    private LineRenderer trackRangeRenderer;
    //트랙 범위 트리거
    [SerializeField]
    private CircleCollider2D trackRangeTrigger;

    //종료 지점 위치
    private Vector3 finishPointPosition;

    private void Awake()
    {
        //플레이어 물리 컴포넌트 초기화
        playerRigidbody = GetComponent<Rigidbody2D>();

        //구심력 게임 매니저 초기화
        gameManager = GameObject.FindGameObjectWithTag(Tag.GAME_MANAGER).GetComponent<CentripetalForceManager>();

        //트랙 범위 트리거 초기화
        trackRangeTrigger.radius = Public.setting.centripetalForceSetting.trackRange;

        //렌더러 위치 개수 초기화
        anchorRotateRangeRenderer.positionCount = Public.setting.positionCount + 1;
        trackRangeRenderer.positionCount = Public.setting.positionCount + 1;
    }

    private void Start()
    {
        //트랙 범위 렌더링
        float angle = 0f;

        for (int i = 0; i < Public.setting.positionCount + 1; i++)
        {
            trackRangeRenderer.SetPosition(
                i,
                new Vector2(
                    Mathf.Cos(Mathf.Deg2Rad * angle) * Public.setting.centripetalForceSetting.trackRange,
                    Mathf.Sin(Mathf.Deg2Rad * angle) * Public.setting.centripetalForceSetting.trackRange));

            angle += (360f / Public.setting.positionCount);
        }
    }

    private void Update()
    {
        if (!gameManager.Started)
        {
            return;
        }

        CheckAnchorControl();
    }

    //실패 효과 재생
    private void PlayFailedEffect()
    {
        gameManager.FinishGame();
        anchorRotateRangeRenderer.enabled = false;
        trackRangeRenderer.startColor = Color.red;
        trackRangeRenderer.endColor = Color.red;
    }

    //성공 효과 코루틴
    private Coroutine SucceedEffectC = null;

    //성공 효과 재생
    private void PlaySucceedEffect()
    {
        gameManager.FinishGame();
        anchorRotateRangeRenderer.enabled = false;
        if (SucceedEffectC != null)
        {
            StopCoroutine(SucceedEffectC);
        }
        SucceedEffectC = StartCoroutine(SucceedEffect());
    }

    //성공 효과 코루틴
    private IEnumerator SucceedEffect()
    {
        while (true)
        {
            playerRigidbody.MovePosition(Vector3.Lerp(playerRigidbody.position, finishPointPosition, Public.setting.centripetalForceSetting.speed * Time.deltaTime));

            if(Vector3.Distance(playerRigidbody.position, finishPointPosition) < 0.005f)
            {
                playerRigidbody.MovePosition(finishPointPosition);
                yield break;
            }

            yield return null;
        }
    }

    //회전축 조작 확인
    private void CheckAnchorControl()
    {
        //마우스 클릭 시 회전축 생성
        if (Input.GetMouseButtonDown(Key.MOUSE_LEFT))
        {
            //마우스 위치 및 거리 계산
            anchorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            anchorDistance = Vector3.Distance(anchorPosition, playerRigidbody.position);

            anchorRotateRangeRenderer.enabled = true;

            //회전축 회전 방향 계산
            float rotationDirectionAngle = Vector2.SignedAngle(
                anchorPosition - playerRigidbody.position,
                direction);
            rotateDirection = rotationDirectionAngle / Mathf.Abs(rotationDirectionAngle);

            anchored = true;

            //휘전축 궤도 렌더링
            float angle = 0f;

            for (int i = 0; i < Public.setting.positionCount + 1; i++)
            {
                anchorRotateRangeRenderer.SetPosition(
                    i,
                    anchorPosition + new Vector2(
                        Mathf.Cos(Mathf.Deg2Rad * angle) * anchorDistance,
                        Mathf.Sin(Mathf.Deg2Rad * angle) * anchorDistance));

                angle += (360f / Public.setting.positionCount);
            }
        }

        //마우스 클릭 취소 시 회전축 삭제
        if (Input.GetMouseButtonUp(Key.MOUSE_LEFT))
        {
            anchorRotateRangeRenderer.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            anchored = false;
        }
    }

    private void FixedUpdate()
    {
        if (!gameManager.Started)
        {
            return;
        }

        Vector2 playerPosition;

        if (anchored)
        {
            //플레이어 위치 회전축 방향 벡터
            Vector2 _direction = playerRigidbody.position - anchorPosition;

            //플레이어 이동 각도
            float angle = 
                (180 * Public.setting.centripetalForceSetting.speed * Time.deltaTime) / (anchorDistance * Mathf.PI);

            //플레이어 이동 위치 차이 계산
            float x1 = anchorDistance * Mathf.Cos(Mathf.Deg2Rad * angle);
            float x2 = x1 / Mathf.Tan((90 - angle) * Mathf.Deg2Rad);

            //플레이어 이동 위치 계산
            playerPosition = anchorPosition +
                (_direction.normalized * x1) +
                ((new Vector2(
                    _direction.y,
                    -_direction.x)).normalized * x2 * rotateDirection);

            //플레이어 진행 방향 계산
            direction = (new Vector2(
                (playerPosition - anchorPosition).y,
                -(playerPosition - anchorPosition).x)).normalized * rotateDirection;
        }
        else
        {
            //플레이어 진행 방향 이동
            playerPosition = playerRigidbody.position + 
                direction * Public.setting.centripetalForceSetting.speed * Time.deltaTime;
        }

        playerRigidbody.MovePosition(playerPosition);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (gameManager.Started)
        {
            //종료 지점 조달 확인
            if (collider.CompareTag(Tag.FINISH_POINT))
            {
                finishPointPosition = collider.transform.position;
                PlaySucceedEffect();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (gameManager.Started)
        {
            //트랙 범위 이탈 확인
            if (collider.CompareTag(Tag.RANGE))
            {
                PlayFailedEffect();
            }
        }
    }
}
