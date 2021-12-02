using UnityEngine;

public class NetForceObject : MonoBehaviour
{
    //�շ� ���� �Ŵ���
    private NetForceManager gameManager;

    private void Awake()
    {
        //�շ� ���� �Ŵ��� �ʱ�ȭ
        gameManager = GameObject.FindGameObjectWithTag(Tag.GAME_MANAGER).GetComponent<NetForceManager>();
    }

    //��ü ���� ��Ż Ȯ��
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (gameManager.Started)
        {
            if (collider.CompareTag(Tag.RANGE))
            {
                gameManager.FinishGame();
            }
        }
    }
}
