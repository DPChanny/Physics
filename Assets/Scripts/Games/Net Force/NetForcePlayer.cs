using UnityEngine;

public class NetForcePlayer : MonoBehaviour
{
    //�շ� ���� �Ŵ���
    private NetForceManager gameManager;

    //��ü ���� ������Ʈ
    private Rigidbody2D objectRigidbody;

    //�� ������
    [SerializeField]
    private LineRenderer forceRenderer;

    //�� ����
    private float forcePower = Public.setting.netForceSetting.minForcePower;

    //�� ���� ���� �ӵ�
    private const float forcePowerControlSpeed = 100f;

    //�� ����
    private ForceDirection forceDirection = ForceDirection.None;

    private void Awake()
    {
        //�շ� ���� �Ŵ��� �ʱ�ȭ
        gameManager = GameObject.FindGameObjectWithTag(Tag.GAME_MANAGER).GetComponent<NetForceManager>();

        //��ü ���� ������Ʈ �ʱ�ȭ
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
