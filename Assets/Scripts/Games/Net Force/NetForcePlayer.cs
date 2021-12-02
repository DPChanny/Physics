using UnityEngine;

public class NetForcePlayer : MonoBehaviour
{
    //합력 게임 매니저
    private NetForceManager gameManager;

    //물체 물리 컴포넌트
    private Rigidbody2D objectRigidbody;

    //힘 렌더러
    [SerializeField]
    private LineRenderer forceRenderer;

    //힘 세기
    private float forcePower = Public.setting.netForceSetting.minForcePower;

    //힘 세기 조작 속도
    private const float forcePowerControlSpeed = 100f;

    //힘 방향
    private ForceDirection forceDirection = ForceDirection.None;

    private void Awake()
    {
        //합력 게임 매니저 초기화
        gameManager = GameObject.FindGameObjectWithTag(Tag.GAME_MANAGER).GetComponent<NetForceManager>();

        //물체 물리 컴포넌트 초기화
        objectRigidbody = GameObject.FindGameObjectWithTag(Tag.OBJECT).GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!gameManager.Started)
        {
            forceRenderer.enabled = false;
            return;
        }

        forceRenderer.SetPosition(0, transform.position);
        forceRenderer.SetPosition(1, objectRigidbody.position);

        float y = Input.GetAxisRaw("Vertical");
        transform.position += Vector3.up * y * Public.setting.netForceSetting.speed * Time.deltaTime;
        transform.position =
            new Vector3(
                transform.position.x, 
                Mathf.Clamp(
                    transform.position.y, 
                    -Public.setting.netForceSetting.moveRange, 
                    Public.setting.netForceSetting.moveRange), 
                transform.position.z);

        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");
        forcePower += forcePowerControlSpeed * scroll;
        forcePower = 
            Mathf.Clamp(
                forcePower, 
                Public.setting.netForceSetting.minForcePower, 
                Public.setting.netForceSetting.maxForcePower);

        if(Input.GetMouseButton(Key.MOUSE_LEFT) && Input.GetMouseButton(Key.MOUSE_RIGHT))
        {
            forceDirection = ForceDirection.None;
        }
        else
        {
            if (Input.GetMouseButton(Key.MOUSE_LEFT))
            {
                forceRenderer.startColor =
                    new Color(
                        Color.red.r,
                        Color.red.g,
                        Color.red.b,
                        forcePower / Public.setting.netForceSetting.maxForcePower);
                forceRenderer.endColor =
                    new Color(
                        Color.red.r,
                        Color.red.g,
                        Color.red.b,
                        forcePower / Public.setting.netForceSetting.maxForcePower);
                forceDirection = ForceDirection.Pull;
            }
            else if (Input.GetMouseButton(Key.MOUSE_RIGHT))
            {
                forceRenderer.startColor =
                    new Color(
                        Color.blue.r,
                        Color.blue.g,
                        Color.blue.b,
                        forcePower / Public.setting.netForceSetting.maxForcePower);
                forceRenderer.endColor =
                    new Color(
                        Color.blue.r,
                        Color.blue.g,
                        Color.blue.b,
                        forcePower / Public.setting.netForceSetting.maxForcePower);
                forceDirection = ForceDirection.Push;
            }
            else
            {
                forceDirection = ForceDirection.None;
            }
        }

        forceRenderer.enabled = forceDirection != ForceDirection.None;

        objectRigidbody.AddForce(((Vector2)transform.position - objectRigidbody.position).normalized * forcePower * Time.deltaTime * (int)forceDirection);
    }
}
