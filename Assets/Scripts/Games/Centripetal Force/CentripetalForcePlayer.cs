using System.Collections;
using UnityEngine;

//���ɷ� ���� �÷��̾�
public class CentripetalForcePlayer : MonoBehaviour
{
    //���ɷ� ���� �Ŵ���
    private CentripetalForceManager gameManager;

    //�÷��̾� ���� ������Ʈ
    private Rigidbody2D playerRigidbody;
    //�÷��̾� �̵� ����
    private Vector2 direction = Vector2.right;

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

    //Ʈ�� ���� ������
    [SerializeField]
    private LineRenderer trackRangeRenderer;
    //Ʈ�� ���� Ʈ����
    [SerializeField]
    private CircleCollider2D trackRangeTrigger;

    //���� ���� ��ġ
    private Vector3 finishPointPosition;

    private void Awake()
    {
        //�÷��̾� ���� ������Ʈ �ʱ�ȭ
        playerRigidbody = GetComponent<Rigidbody2D>();

        //���ɷ� ���� �Ŵ��� �ʱ�ȭ
        gameManager = GameObject.FindGameObjectWithTag(Tag.GAME_MANAGER).GetComponent<CentripetalForceManager>();

        //Ʈ�� ���� Ʈ���� �ʱ�ȭ
        trackRangeTrigger.radius = Public.setting.centripetalForceSetting.trackRange;

        //������ ��ġ ���� �ʱ�ȭ
        anchorRotateRangeRenderer.positionCount = Public.setting.positionCount + 1;
        trackRangeRenderer.positionCount = Public.setting.positionCount + 1;
    }

    private void Start()
    {
        //Ʈ�� ���� ������
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

    //���� ȿ�� ���
    private void PlayFailedEffect()
    {
        gameManager.FinishGame();
        anchorRotateRangeRenderer.enabled = false;
        trackRangeRenderer.startColor = Color.red;
        trackRangeRenderer.endColor = Color.red;
    }

    //���� ȿ�� �ڷ�ƾ
    private Coroutine SucceedEffectC = null;

    //���� ȿ�� ���
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

    //���� ȿ�� �ڷ�ƾ
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

    //ȸ���� ���� Ȯ��
    private void CheckAnchorControl()
    {
        //���콺 Ŭ�� �� ȸ���� ����
        if (Input.GetMouseButtonDown(Key.MOUSE_LEFT))
        {
            //���콺 ��ġ �� �Ÿ� ���
            anchorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            anchorDistance = Vector3.Distance(anchorPosition, playerRigidbody.position);

            anchorRotateRangeRenderer.enabled = true;

            //ȸ���� ȸ�� ���� ���
            float rotationDirectionAngle = Vector2.SignedAngle(
                anchorPosition - playerRigidbody.position,
                direction);
            rotateDirection = rotationDirectionAngle / Mathf.Abs(rotationDirectionAngle);

            anchored = true;

            //������ �˵� ������
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
        if (!gameManager.Started)
        {
            return;
        }

        Vector2 playerPosition;

        if (anchored)
        {
            //�÷��̾� ��ġ ȸ���� ���� ����
            Vector2 _direction = playerRigidbody.position - anchorPosition;

            //�÷��̾� �̵� ����
            float angle = 
                (180 * Public.setting.centripetalForceSetting.speed * Time.deltaTime) / (anchorDistance * Mathf.PI);

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
            playerPosition = playerRigidbody.position + 
                direction * Public.setting.centripetalForceSetting.speed * Time.deltaTime;
        }

        playerRigidbody.MovePosition(playerPosition);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (gameManager.Started)
        {
            //���� ���� ���� Ȯ��
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
            //Ʈ�� ���� ��Ż Ȯ��
            if (collider.CompareTag(Tag.RANGE))
            {
                PlayFailedEffect();
            }
        }
    }
}
