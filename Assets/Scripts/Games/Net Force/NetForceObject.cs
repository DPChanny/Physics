using UnityEngine;

public class NetForceObject : MonoBehaviour
{
    //합력 게임 매니저
    private NetForceManager gameManager;

    private void Awake()
    {
        //합력 게임 매니저 초기화
        gameManager = GameObject.FindGameObjectWithTag(Tag.GAME_MANAGER).GetComponent<NetForceManager>();
    }

    //물체 범위 이탈 확인
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
