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
    private LightManager manager;
    //�� ���� ����
    private Vector2 direction = Vector2.right;

    //�� �ӵ�
    private const float speed = 10f;
    private Collider2D lastCollider;

    //���� ���� ��ġ
    private Vector3 finishPointPosition;

    private void Awake()
    {
        //�� ���� �Ŵ��� �ʱ�ȭ
        manager = GameObject.FindGameObjectWithTag(Tag.GAME_MANAGER).GetComponent<LightManager>();
    }

    private void Start()
    {
        //�� ������ �ʱ�ȭ
        lightRenderer.positionCount = 1;
        CreatePosition(lightRigidbody.position);
    }

    private void Update()
    {
        if (!manager.Started)
        {
            return;
        }

        lightRenderer.SetPosition(lightRenderer.positionCount - 1, lightRigidbody.position);
    }

    private void FixedUpdate()
    {
        if (!manager.Started)
        {
            return;
        }

        lightRigidbody.MovePosition(lightRigidbody.position + direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
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

    private void CreatePosition(Vector2 _position)
    {
        lightRenderer.SetPosition(lightRenderer.positionCount - 1, _position);
        lightRenderer.positionCount += 1;
        lightRenderer.SetPosition(lightRenderer.positionCount - 1, _position);
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
            lightRigidbody.MovePosition(
                Vector3.Lerp(
                    lightRigidbody.position, 
                    finishPointPosition, 
                    Public.setting.centripetalForceSetting.speed * Time.deltaTime));

            if (Vector3.Distance(lightRigidbody.position, finishPointPosition) < 0.005f)
            {
                lightRigidbody.MovePosition(finishPointPosition);
                yield break;
            }

            yield return null;
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (manager.Started)
        {
            //���� ���� ���� Ȯ��
            if (collider.CompareTag(Tag.FINISH_POINT))
            {
                manager.SucceedGame();
                finishPointPosition = collider.transform.position;
                PlaySucceedEffect();
            }
        }
    }
}
