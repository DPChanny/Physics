using System.Collections;
using UnityEngine;

public class LightLight : MonoBehaviour
{
    //빛 렌더러
    [SerializeField]
    private LineRenderer lightRenderer;

    //빛 물리 컴포넌트
    [SerializeField]
    private Rigidbody2D lightRigidbody;

    //빛 게임 매니저
    private LightManager gameManager;

    //빛 진행 방향
    private Vector2 direction = Vector2.down;

    //빛 속도
    private Collider2D lastCollider;

    //종료 지점 위치
    private Vector3 finishPointPosition;

    private void Awake()
    {
        //빛 게임 매니저 초기화
        gameManager = GameObject.FindGameObjectWithTag(Tag.GAME_MANAGER).GetComponent<LightManager>();
    }

    private void Start()
    {
        //빛 렌더러 초기화
        lightRenderer.positionCount = 1;
        CreatePosition(lightRigidbody.position);
    }
    private void Update()
    {
        //카메라 상 위치 확인
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        foreach (Plane plane in planes){
            if(plane.GetDistanceToPoint(lightRigidbody.position) < 0)
            {
                gameManager.FailedGame();
                PlayFailedEffect();
            }
        }

        if (!gameManager.Started)
        {
            return;
        }

        lightRenderer.SetPosition(lightRenderer.positionCount - 1, lightRigidbody.position);
    }

    private void FixedUpdate()
    {
        if (!gameManager.Started)
        {
            return;
        }

        lightRigidbody.MovePosition(lightRigidbody.position + direction * Public.setting.lightSetting.speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(Tag.MIRROR))
        {
            if (lastCollider != null)
            {
                lastCollider.enabled = true;
            }
            direction = Vector2.Reflect(direction, collision.contacts[0].normal);
            lastCollider = collision.collider;
            lastCollider.enabled = false;
            lightRigidbody.position = collision.contacts[0].point;
            CreatePosition(collision.contacts[0].point);
        }
        if (collision.collider.CompareTag(Tag.OBSTACLE))
        {
            gameManager.FailedGame();
            PlayFailedEffect();
        }
    }

    private void CreatePosition(Vector2 _position)
    {
        lightRenderer.SetPosition(lightRenderer.positionCount - 1, _position);
        lightRenderer.positionCount += 1;
        lightRenderer.SetPosition(lightRenderer.positionCount - 1, _position);
    }


    //실패 효과 재생
    private void PlayFailedEffect()
    {
        lightRenderer.startColor = Color.red;
        lightRenderer.endColor = Color.red;
        if (lastCollider != null)
        {
            lastCollider.enabled = true;
        }
    }

    //성공 효과 코루틴
    private Coroutine SucceedEffectC = null;

    //성공 효과 재생
    private void PlaySucceedEffect()
    {
        if (lastCollider != null)
        {
            lastCollider.enabled = true;
        }
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
            lightRenderer.SetPosition(lightRenderer.positionCount - 1, lightRigidbody.position);
            lightRigidbody.MovePosition(
                Vector3.Lerp(
                    lightRigidbody.position, 
                    finishPointPosition, 
                    Public.setting.centripetalForceSetting.speed * Time.deltaTime));

            if (Vector3.Distance(lightRigidbody.position, finishPointPosition) < 0.005f)
            {
                lightRenderer.SetPosition(lightRenderer.positionCount - 1, finishPointPosition);
                lightRigidbody.MovePosition(finishPointPosition);
                yield break;
            }

            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (gameManager.Started)
        {
            //종료 지점 조달 확인
            if (collider.CompareTag(Tag.FINISH_POINT))
            {
                gameManager.SucceedGame();
                finishPointPosition = collider.transform.position;
                PlaySucceedEffect();
            }
        }
    }
}
