using UnityEngine;

public class NetForceObjectRange : MonoBehaviour
{
    //�շ� ���� �Ŵ���
    private NetForceManager gameManager;

    //��ü ���� ������
    [SerializeField]
    private LineRenderer objectRangeRenderer;
    //��ü ���� Ʈ����
    [SerializeField]
    private CircleCollider2D objectRangeTrigger;

    private void Awake()
    {
        //�շ� ���� �Ŵ��� �ʱ�ȭ
        gameManager = GameObject.FindGameObjectWithTag(Tag.GAME_MANAGER).GetComponent<NetForceManager>();

        //������ ��ġ ���� �ʱ�ȭ
        objectRangeRenderer.positionCount = Public.setting.positionCount + 1;

        //��ü ���� Ʈ���� �ʱ�ȭ
        objectRangeTrigger.radius = Public.setting.netForceSetting.objectRange;
    }

    private void Start()
    {
        //��ü ���� ������
        float angle = 0f;

        for (int i = 0; i < Public.setting.positionCount + 1; i++)
        {
            objectRangeRenderer.SetPosition(
                i,
                new Vector2(
                    Mathf.Cos(Mathf.Deg2Rad * angle) * Public.setting.netForceSetting.objectRange,
                    Mathf.Sin(Mathf.Deg2Rad * angle) * Public.setting.netForceSetting.objectRange));

            angle += (360f / Public.setting.positionCount);
        }
    }

    private void Update()
    {
        if (!gameManager.Started)
        {
            objectRangeRenderer.startColor = Color.red;
            objectRangeRenderer.endColor = Color.red;
        }
    }
}
