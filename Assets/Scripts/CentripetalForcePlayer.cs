using UnityEngine;

public class CentripetalForcePlayer : MonoBehaviour
{
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
    private RotateDirection rotateDirection;

    //ȸ���� ȸ�� ����
    private enum RotateDirection
    {
        ClockDirection, //�ð� ����
        ReverseClockDirection //�ݽð� ����
    }

    //���� ���۽� �ʱ� ���ӵ� �ο�
    public void StartGame()
    {
        anchored = false;
        playerRigidbody.velocity = direction * speed * Time.deltaTime;
    }

    public void Update()
    {
        if (!manager.started)
        {
            return;
        }

        //���콺 Ŭ�� �� ȸ���� ����
        if (Input.GetMouseButtonDown(Key.MOUSE_LEFT))
        {
            //���콺 Ŭ�� ��ġ ���
            anchorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //���콺 ��ġ�� �÷��̾ �Ÿ�
            anchorDistance = Vector3.Distance(anchorPosition, transform.position);                         

            //ȸ���� ȸ�� ���� ���
            if(Vector2.SignedAngle(
                    (new Vector2(
                        transform.position.x, 
                        transform.position.y) - anchorPosition), 
                    playerRigidbody.velocity) > 0){
                rotateDirection = RotateDirection.ReverseClockDirection;
            }
            else
            {
                rotateDirection = RotateDirection.ClockDirection;
            }

            anchored = true;
        }
        //���콺 Ŭ�� �� ȸ���� ����
        if (Input.GetMouseButtonUp(Key.MOUSE_LEFT))
        {
            anchored = false;
        }

        if (anchored)
        {

        }
        else
        {
            playerRigidbody.velocity = direction * speed * Time.deltaTime;
        }
    }
}
