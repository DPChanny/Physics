using System.Collections;
using UnityEngine;

//구심력 게임 플레이어
public class CentripetalForcePlayer : MonoBehaviour
{
    //구심력 게임 매니저
    [SerializeField]
    private CentripletalForceManager manager;

    //플레이어 물리 컴포넌트
    [SerializeField]
    private Rigidbody2D playerRigidbody;
    //플레이어 진행 속도
    [SerializeField]
    private float speed;

    [HideInInspector]
    //플레이어 진행 방향
    public Vector2 direction;

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
    //회전축 최대 거리
    private const float maxAnchorDistance = 10f;

    //트랙 범위 렌더러
    [SerializeField]
    private LineRenderer trackRangeRenderer;
    //트랙 범위 트리거
    [SerializeField]
    private CircleCollider2D trackRangeTrigger;

    //렌더러 모서리 개수
    [SerializeField]
    private int segments;

    //종료 지점 위치
    private Vector3 finishPointPosition;

    private void Start()
    {
        //렌더러 모서리 개수 초기화
        anchorRotateRangeRenderer.positionCount = segments + 1;
        trackRangeRenderer.positionCount = segments + 1;

        //구심력 게임 매니저 초기화
        manager = GameObject.FindGameObjectWithTag(Tag.GAME_MANAGER).GetComponent<CentripletalForceManager>();

        //트랙 범위 트리거 초기화
        trackRangeTrigger.radius = manager.TrackRange;

        //트랙 범위 렌더링
        float angle = 20f;

        for (int i = 0; i < segments + 1; i++)
        {
            trackRangeRenderer.SetPosition(
                i,
                new Vector2(
                    Mathf.Cos(Mathf.Deg2Rad * angle) * manager.TrackRange,
                    Mathf.Sin(Mathf.Deg2Rad * angle) * manager.TrackRange));

            angle += (360f / segments);
        }
    }

    private void Update()
    {
        if (!manager.Started)
        {
            return;
        }

        CheckAnchorControl();
    }

    //실패 효과 재생 함수
    private void PlayFailedEffect()
    {
        trackRangeRenderer.startColor = Color.red;
        trackRangeRenderer.endColor = Color.red;
        anchorRotateRangeRenderer.enabled = false;
    }

    //성공 효과 코루틴 변수
    private IEnumerator SucceedEffectI = null;

    //성공 효과 재생 함수
    private void PlaySucceedEffect()
    {
        anchorRotateRangeRenderer.enabled = false;
        if (SucceedEffectI != null)
        {
            StopCoroutine(SucceedEffectI);
        }
        SucceedEffectI = SucceedEffect();
        StartCoroutine(SucceedEffectI);
    }

    //성공 효과 코루틴 함수
    private IEnumerator SucceedEffect()
    {
        while (true)
        {
            playerRigidbody.MovePosition(Vector3.Lerp(playerRigidbody.position, finishPointPosition, speed * Time.deltaTime));

            if(Vector3.Distance(playerRigidbody.position, finishPointPosition) < 0.005f)
            {
                playerRigidbody.MovePosition(finishPointPosition);
                yield break;
            }

            yield return null;
        }
    }

    //회전축 조작 확인 함수
    private void CheckAnchorControl()
    {
        //마우스 클릭 시 회전축 생성
        if (Input.GetMouseButtonDown(Key.MOUSE_LEFT))
        {
            //마우스 위치 및 거리 계산
            anchorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            anchorDistance = Vector3.Distance(anchorPosition, playerRigidbody.position);

            if (anchorDistance < maxAnchorDistance)
            {
                anchorRotateRangeRenderer.enabled = true;

                //회전축 회전 방향 계산
                float rotationDirectionAngle = Vector2.SignedAngle(
                    anchorPosition - playerRigidbody.position,
                    direction);
                rotateDirection = rotationDirectionAngle / Mathf.Abs(rotationDirectionAngle);

                anchored = true;

                //휘전축 궤도 렌더링
                float angle = 20f;

                for (int i = 0; i < segments + 1; i++)
                {
                    anchorRotateRangeRenderer.SetPosition(
                        i,
                        anchorPosition + new Vector2(
                            Mathf.Cos(Mathf.Deg2Rad * angle) * anchorDistance,
                            Mathf.Sin(Mathf.Deg2Rad * angle) * anchorDistance));

                    angle += (360f / segments);
                }
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
        if (!manager.Started)
        {
            return;
        }

        Vector2 playerPosition;

        if (anchored)
        {
            //플레이어 위치 회전축 방향 벡터
            Vector2 _direction = playerRigidbody.position - anchorPosition;

            //플레이어 이동 각도
            float angle = (180 * speed * Time.deltaTime) / (anchorDistance * Mathf.PI);

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
            playerPosition = playerRigidbody.position + direction * speed * Time.deltaTime;
        }

        playerRigidbody.MovePosition(playerPosition);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (manager.Started)
        {
            //종료 지점 조달 확인
            if (collider.CompareTag(Tag.FINISH_POINT))
            {
                manager.SucceedGame();
                finishPointPosition = collider.transform.position;
                PlaySucceedEffect();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (manager.Started)
        {
            //트랙 범위 이탈 확인
            if (collider.CompareTag(Tag.TRACK))
            {
                manager.FailedGame();
                PlayFailedEffect();
            }
        }
    }
}
