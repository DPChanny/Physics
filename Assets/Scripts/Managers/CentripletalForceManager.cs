using UnityEngine;

//구심력 게임 매니저
public class CentripletalForceManager : MonoBehaviour
{
    //게임 시작 여부
    [HideInInspector]
    public bool started = false;

    private void Update()
    {
        if (!started)
        {
            //게임 시작 버튼 클릭 시 게임 시작
            if (Input.GetKeyDown(Key.START))
            {
                started = true;
            }
        }
    }
}
