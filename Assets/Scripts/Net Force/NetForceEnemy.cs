using UnityEngine;

public class NetForceEnemy : MonoBehaviour
{
    //�շ� ���� �Ŵ���
    private NetForceManager manager;

    //��ü ���� ������Ʈ
    private Rigidbody2D objectRigidbody;

    //�� ������
    [SerializeField]
    private LineRenderer forceRenderer;

    //�� ����
    private float forcePower = Public.setting.netForceSetting.defaultForcePower;

    //��ǥ ��ġ
    private float targetPosition = 0f;

    //�� ����
    private ForceDirection forceDirection = Public.setting.netForceSetting.enemyDefaultForceDirection;

    //��ȯ ���� �ð�
    private float toogleCoolTime = 0f;

    private void Awake()
    {
        //�շ� ���� �Ŵ��� �ʱ�ȭ
        manager = GameObject.FindGameObjectWithTag(Tag.GAME_MANAGER).GetComponent<NetForceManager>();

        //��ü ���� ������Ʈ �ʱ�ȭ
        objectRigidbody = GameObject.FindGameObjectWithTag(Tag.OBJECT).GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!manager.Started)
        {
            forceRenderer.enabled = false;
            return;
        }

        forceRenderer.SetPosition(0, transform.position);
        forceRenderer.SetPosition(1, objectRigidbody.position);

        if (toogleCoolTime > 1)
        {
            toogleCoolTime = 0f;
            if (Public.setting.netForceSetting.enemyForceDirectionToggle)
            {
                if (Random.Range(1, 100) <= Public.setting.netForceSetting.enemyForceDirectionTogglePercentage)
                {
                    forceDirection = (ForceDirection)Random.Range(-1, 2);
                }
            }
            if (Public.setting.netForceSetting.enemyForcePowerToggle)
            {
                if (Random.Range(1, 100) <= Public.setting.netForceSetting.enemyForcePowerTogglePercentage)
                {
                    forcePower = Random.Range(Public.setting.netForceSetting.minForcePower, Public.setting.netForceSetting.maxForcePower + 1);
                }
            }
        }
        else
        {
            toogleCoolTime += Time.deltaTime;
        }

        if (Public.setting.netForceSetting.enemyMove)
        {
            if (Mathf.Abs(transform.position.y - targetPosition) < 1)
            {
                targetPosition = Random.Range(-Public.setting.netForceSetting.moveRange, Public.setting.netForceSetting.moveRange + 1);
            }

            if (targetPosition > transform.position.y)
            {
                transform.position += Vector3.up *
                    Public.setting.netForceSetting.speed *
                    Time.deltaTime;
            }
            else
            {
                transform.position += Vector3.down *
                    Public.setting.netForceSetting.speed *
                    Time.deltaTime;
            }
        }

        forceRenderer.enabled = forceDirection != ForceDirection.None;

        if (forceDirection == ForceDirection.Pull)
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
        }
        if (forceDirection == ForceDirection.Push)
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
        }

        objectRigidbody.AddForce(((Vector2)transform.position - objectRigidbody.position).normalized * forcePower * Time.deltaTime * (int)forceDirection);
    }
}
