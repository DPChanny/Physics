using UnityEngine;
using UnityEngine.SceneManagement;

public class NetForceManager : MonoBehaviour
{
    //��ü ����
    [SerializeField]
    private float objectRange;
    //��ü ���� ������
    [SerializeField]
    private LineRenderer objectRangeRenderer;
    //������ �𼭸� ����
    [SerializeField]
    private int segments;

    //��ü ���� Ʈ����
    [SerializeField]
    private CircleCollider2D objectRangeTrigger;

    //���� ���� ����
    private bool started = false;
    public bool Started
    {
        get
        {
            return started;
        }
    }

    //���� ���� �Լ�
    private void StartGame()
    {
        //��ü ���� Ʈ���� �ʱ�ȭ
        objectRangeTrigger.radius = objectRange;

        //������ �𼭸� ���� �ʱ�ȭ
        objectRangeRenderer.positionCount = segments + 1;

        //Ʈ�� ���� ������
        float angle = 20f;

        for (int i = 0; i < segments + 1; i++)
        {
            objectRangeRenderer.SetPosition(
                i,
                new Vector2(
                    Mathf.Cos(Mathf.Deg2Rad * angle) * objectRange,
                    Mathf.Sin(Mathf.Deg2Rad * angle) * objectRange));

            angle += (360f / segments);
        }

        started = true;
    }

    //���� ���� �Լ�
    public void FailedGame()
    {
        started = false;
    }

    //���� ���� �Լ�
    public void SucceedGame()
    {
        started = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(Key.EXIT))
        {
            SceneManager.LoadScene(SceneName.MAIN);
        }
        if (!started)
        {
            //���� ���� ��ư Ŭ�� �� ���� ����
            if (Input.GetKeyDown(Key.START))
            {
                StartGame();
            }
        }
    }

    //������Ʈ ���� ��Ż Ȯ��
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (started)
        {
            if (collider.CompareTag(Tag.OBJECT))
            {
                FailedGame();
            }
        }
    }
}
