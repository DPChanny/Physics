using UnityEngine;
using TMPro;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField InputUI_speed;
    [SerializeField]
    private TMP_InputField InputUI_trackRange;

    [SerializeField]
    private GameObject UI;

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

    private void Init()
    {
        InputUI_speed.interactable = Public.setting.centripetalForceDifficulty == Difficulty.Custom;
        InputUI_trackRange.interactable = Public.setting.centripetalForceDifficulty == Difficulty.Custom;

        InputUI_speed.text = Public.setting.centripetalForceSetting.speed.ToString();
        InputUI_trackRange.text = Public.setting.centripetalForceSetting.trackRange.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(Key.DIFFICULTY_MANAGER))
        {
            UI.SetActive(true);
            Init();
        }
    }

    public void OnConfirm()
    {
        if(Public.setting.centripetalForceDifficulty == Difficulty.Custom)
        {
            float.TryParse(InputUI_speed.text, out Public.setting.centripetalForceSetting.speed);
            float.TryParse(InputUI_trackRange.text, out Public.setting.centripetalForceSetting.trackRange);
        }
        UI.SetActive(false);
    }

    public void OnDifficulty(int _difficulty)
    {
        Public.setting.centripetalForceDifficulty = (Difficulty)_difficulty;

        Init();
    }
}
