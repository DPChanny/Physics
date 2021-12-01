using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField]
    private GameObject UI;
    [SerializeField]
    private GameObject centripetalForce;

    private void Awake()
    {
        if(GameObject.FindGameObjectsWithTag(Tag.DIFFICULTY_MANAGER).Length != 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(Key.DIFFICULTY_MANAGER))
        {
            UI.SetActive(true);
        }
    }

    public void OnCentripetalForce()
    {
        centripetalForce.SetActive(true);
    }

    public void OnConfirm()
    {
        UI.SetActive(false);
    }
}
