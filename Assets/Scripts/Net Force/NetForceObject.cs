using UnityEngine;

public class NetForceObject : MonoBehaviour
{
    //�շ� ���� �Ŵ���
    private NetForceManager manager;

    private void Awake()
    {
        //�շ� ���� �Ŵ��� �ʱ�ȭ
        manager = GameObject.FindGameObjectWithTag(Tag.GAME_MANAGER).GetComponent<NetForceManager>();
    }

    //��ü ���� ��Ż Ȯ��
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (manager.Started)
        {
            if (collider.CompareTag(Tag.RANGE))
            {
                manager.FinishGame();
            }
        }
    }
}
