using UnityEngine;

public class CentripletalForceManager : MonoBehaviour
{
    [HideInInspector]
    public bool started = false;

    private void Update()
    {
        if (!started)
        {
            if (Input.GetKeyDown(Key.START))
            {
                started = true;
                GameObject.FindGameObjectWithTag(Tag.PLAYER).GetComponent<CentripetalForcePlayer>().StartGame();
            }
        }
    }
}
