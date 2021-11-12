using System.Collections.Generic;
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
    //�÷��̾� ���� ����
    [SerializeField]
    private Vector2 direction;

    //ȸ���� ����
    private bool anchored = false;
    //ȸ���� ��ġ
    private Vector2 anchorPosition;
    //ȸ���� �Ÿ�
    private float anchorDistance; 
    //ȸ���� ȸ�� ����
    private float rotateDirection;
    //ȸ���� ȸ�� �˵�
    [SerializeField]
    private LineRenderer anchorRotateRange;
    //ȸ���� ���� �˵� �𼭸� ����
    [SerializeField]
    private int segments;
    //ȸ���� �ִ� �Ÿ�
    private const float maxAnchorDistance = 10f;

    private void Start()
    {
        //ȸ���� ȸ�� �˵� �𼭸� ���� �ʱ�ȭ
        anchorRotateRange.positionCount = segments + 1;
    }

    private void Update()
    {
        if (!manager.started)
        {
            return;
        }

        //���콺 Ŭ�� �� ȸ���� ����
        if (Input.GetMouseButtonDown(Key.MOUSE_LEFT))
        {
            //���콺 ��ġ �� �Ÿ� ���
            anchorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            anchorDistance = Vector3.Distance(anchorPosition, playerRigidbody.position);       

            if(anchorDistance < maxAnchorDistance)
            {
                anchorRotateRange.enabled = true;

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
                    anchorRotateRange.SetPosition(
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
            anchorRotateRange.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            anchored = false;
        }
    }

    private void FixedUpdate()
    {
        if (!manager.started)
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
            playerPosition =  playerRigidbody.position + direction * speed * Time.deltaTime;
        }

        playerRigidbody.MovePosition(playerPosition);
    }
}
