using UnityEngine;

//���ɷ� ���� �Ŵ���
public class CentripletalForceManager : MonoBehaviour
{
    //���� ���� ����
    [HideInInspector]
    public bool started = false;

    private void Update()
    {
        if (!started)
        {
            //���� ���� ��ư Ŭ�� �� ���� ����
            if (Input.GetKeyDown(Key.START))
            {
                started = true;
            }
        }
    }
}
