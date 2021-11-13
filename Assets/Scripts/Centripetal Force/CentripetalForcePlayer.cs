using System.Collections;
using UnityEngine;

//���ɷ� ���� �÷��̾�
public class CentripetalForcePlayer : MonoBehaviour
{
    //���ɷ� ���� �Ŵ���
    [SerializeField]
    private CentripletalForceManager manager;

    //�÷��̾� ���� ������Ʈ
    [SerializeField]
    private Rigidbody2D playerRigidbody;
    //�÷��̾� ���� �ӵ�
    [SerializeField]
    private float speed;

    [HideInInspector]
    //�÷��̾� ���� ����
    public Vector2 direction;

    //ȸ���� ����
    private bool anchored = false;
    //ȸ���� ��ġ
    private Vector2 anchorPosition;
    //ȸ���� �Ÿ�
    private float anchorDistance; 
    //ȸ���� ȸ�� ����
    private float rotateDirection;
    //ȸ���� ȸ�� �˵� ������
    [SerializeField]
    private LineRenderer anchorRotateRangeRenderer;
    //ȸ���� �ִ� �Ÿ�
    private const float maxAnchorDistance = 10f;

    //Ʈ�� ���� ������
    [SerializeField]
    private LineRenderer trackRangeRenderer;
    //Ʈ�� ���� Ʈ����
    [SerializeField]
    private CircleCollider2D trackRangeTrigger;

    //������ �𼭸� ����
    [SerializeField]
    private int segments;

    //���� ���� ��ġ
    private Vector3 finishPointPosition;

    private void Start()
    {
        //������ �𼭸� ���� �ʱ�ȭ
        anchorRotateRangeRenderer.positionCount = segments + 1;
        trackRangeRenderer.positionCount = segments + 1;

        //���ɷ� ���� �Ŵ��� �ʱ�ȭ
        manager = GameObject.FindGameObjectWithTag(Tag.GAME_MANAGER).GetComponent<CentripletalForceManager>();

        //Ʈ�� ���� Ʈ���� �ʱ�ȭ
        trackRangeTrigger.radius = manager.TrackRange;

        //Ʈ�� ���� ������
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

    //���� ȿ�� ��� �Լ�
    private void PlayFailedEffect()
    {
        trackRangeRenderer.startColor = Color.red;
        trackRangeRenderer.endColor = Color.red;
        anchorRotateRangeRenderer.enabled = false;
    }

    //���� ȿ�� �ڷ�ƾ ����
    private IEnumerator SucceedEffectI = null;

    //���� ȿ�� ��� �Լ�
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

    //���� ȿ�� �ڷ�ƾ �Լ�
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

    //ȸ���� ���� Ȯ�� �Լ�
    private void CheckAnchorControl()
    {
        //���콺 Ŭ�� �� ȸ���� ����
        if (Input.GetMouseButtonDown(Key.MOUSE_LEFT))
        {
            //���콺 ��ġ �� �Ÿ� ���
            anchorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            anchorDistance = Vector3.Distance(anchorPosition, playerRigidbody.position);

            if (anchorDistance < maxAnchorDistance)
            {
                anchorRotateRangeRenderer.enabled = true;

                //ȸ���� ȸ�� ���� ���
                float rotationDirectionAngle = Vector2.SignedAngle(
                    anchorPosition - playerRigidbody.position,
                    direction);
                rotateDirection = rotationDirectionAngle / Mathf.Abs(rotationDirectionAngle);

                anchored = true;

                //������ �˵� ������
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

        //���콺 Ŭ�� ��� �� ȸ���� ����
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
            //�÷��̾� ��ġ ȸ���� ���� ����
            Vector2 _direction = playerRigidbody.position - anchorPosition;

            //�÷��̾� �̵� ����
            float angle = (180 * speed * Time.deltaTime) / (anchorDistance * Mathf.PI);

            //�÷��̾� �̵� ��ġ ���� ���
            float x1 = anchorDistance * Mathf.Cos(Mathf.Deg2Rad * angle);
            float x2 = x1 / Mathf.Tan((90 - angle) * Mathf.Deg2Rad);

            //�÷��̾� �̵� ��ġ ���
            playerPosition = anchorPosition +
                (_direction.normalized * x1) +
                ((new Vector2(
                    _direction.y,
                    -_direction.x)).normalized * x2 * rotateDirection);

            //�÷��̾� ���� ���� ���
            direction = (new Vector2(
                (playerPosition - anchorPosition).y,
                -(playerPosition - anchorPosition).x)).normalized * rotateDirection;
        }
        else
        {
            //�÷��̾� ���� ���� �̵�
            playerPosition = playerRigidbody.position + direction * speed * Time.deltaTime;
        }

        playerRigidbody.MovePosition(playerPosition);
    }

    private void OnTriggerEnter2D(Collider2D collider)
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

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (manager.Started)
        {
            //Ʈ�� ���� ��Ż Ȯ��
            if (collider.CompareTag(Tag.TRACK))
            {
                manager.FailedGame();
                PlayFailedEffect();
            }
        }
    }
}
