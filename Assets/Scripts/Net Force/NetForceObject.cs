using UnityEngine;

public class NetForceObject : MonoBehaviour
{
    //합력 게임 매니저
    private NetForceManager manager;

    private void Awake()
    {
        //합력 게임 매니저 초기화
        manager = GameObject.FindGameObjectWithTag(Tag.GAME_MANAGER).GetComponent<NetForceManager>();
    }

    //물체 범위 이탈 확인
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
