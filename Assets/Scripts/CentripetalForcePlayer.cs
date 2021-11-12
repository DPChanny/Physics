using UnityEngine;

public class CentripetalForcePlayer : MonoBehaviour
{
    [SerializeField]
    private CentripletalForceManager manager;

    //플레이어 물리 컴포넌트
    [SerializeField]
    private Rigidbody2D playerRigidbody;
    //플레이어 진행 속도
    [SerializeField]
    private float speed;
    //플레이어 진행 방향
    [SerializeField]
    private Vector2 direction;

    //회전축 유무
    private bool anchored = false;
    //회전축 위치
    private Vector2 anchorPosition;
    //회전축 거리
    private float anchorDistance;
    //회전축 회전 방향
    private RotateDirection rotateDirection;

    //회전축 회전 방향
    private enum RotateDirection
    {
        ClockDirection, //시계 방향
        ReverseClockDirection //반시계 방향
    }

    //게임 시작시 초기 가속도 부여
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

        //마우스 클릭 시 회전축 생성
        if (Input.GetMouseButtonDown(Key.MOUSE_LEFT))
        {
            //마우스 클릭 위치 계산
            anchorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //마우스 위치와 플레이어간 거리
            anchorDistance = Vector3.Distance(anchorPosition, transform.position);                         

            //회전축 회전 방향 계산
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
        //마우스 클릭 시 회전축 삭제
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
