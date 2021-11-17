using System.Collections;
using UnityEngine;

public class LightLight : MonoBehaviour
{
    //�� ������
    [SerializeField]
    private LineRenderer lightRenderer;

    //�� ���� ������Ʈ
    [SerializeField]
    private Rigidbody2D lightRigidbody;

    //�� ���� �Ŵ���
    private LightManager gameManager;

    //�� ���� ����
    private Vector2 direction = Vector2.down;

    //�� �ӵ�
    private Collider2D lastCollider;

    //���� ���� ��ġ
    private Vector3 finishPointPosition;

    private void Awake()
    {
        //�� ���� �Ŵ��� �ʱ�ȭ
        gameManager = GameObject.FindGameObjectWithTag(Tag.GAME_MANAGER).GetComponent<LightManager>();
    }

    private void Start()
    {
        //�� ������ �ʱ�ȭ
        lightRenderer.positionCount = 1;
        CreatePosition(lightRigidbody.position);
    }
    private void Update()
    {
        //ī�޶� �� ��ġ Ȯ��
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


    //���� ȿ�� ���
    private void PlayFailedEffect()
    {
        lightRenderer.startColor = Color.red;
        lightRenderer.endColor = Color.red;
        if (lastCollider != null)
        {
            lastCollider.enabled = true;
        }
    }

    //���� ȿ�� �ڷ�ƾ
    private Coroutine SucceedEffectC = null;

    //���� ȿ�� ���
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

    //���� ȿ�� �ڷ�ƾ
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
            //���� ���� ���� Ȯ��
            if (collider.CompareTag(Tag.FINISH_POINT))
            {
                gameManager.SucceedGame();
                finishPointPosition = collider.transform.position;
                PlaySucceedEffect();
            }
        }
    }
}
