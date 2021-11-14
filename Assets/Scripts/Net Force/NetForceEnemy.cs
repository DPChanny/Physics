using UnityEngine;

public class NetForceEnemy : MonoBehaviour
{
    //합력 게임 매니저
    private NetForceManager manager;

    //물체 물리 컴포넌트
    private Rigidbody2D objectRigidbody;

    //힘 렌더러
    [SerializeField]
    private LineRenderer forceRenderer;

    //힘 세기
    private float forcePower = Public.setting.netForceSetting.defaultForcePower;

    //목표 위치
    private float targetPosition = 0f;

    //힘 방향
    private ForceDirection forceDirection = Public.setting.netForceSetting.enemyDefaultForceDirection;

    //전환 지연 시간
    private float toogleCoolTime = 0f;

    private void Awake()
    {
        //합력 게임 매니저 초기화
        manager = GameObject.FindGameObjectWithTag(Tag.GAME_MANAGER).GetComponent<NetForceManager>();

        //물체 물리 컴포넌트 초기화
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
