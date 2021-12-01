using TMPro;
using UnityEngine;

public class CentripetalForceDifficultyManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField InputUI_speed;
    [SerializeField]
    private TMP_InputField InputUI_trackRange;

    private void OnEnable()
    {
        InputUI_speed.interactable = Public.setting.centripetalForceDifficulty == Difficulty.Custom;
        InputUI_trackRange.interactable = Public.setting.centripetalForceDifficulty == Difficulty.Custom;

        InputUI_speed.text = Public.setting.centripetalForceSetting.speed.ToString();
        InputUI_trackRange.text = Public.setting.centripetalForceSetting.trackRange.ToString();
    }

    public void OnConfirm()
    {
        if (Public.setting.centripetalForceDifficulty == Difficulty.Custom)
        {
            float.TryParse(InputUI_speed.text, out Public.setting.centripetalForceSetting.speed);
            float.TryParse(InputUI_trackRange.text, out Public.setting.centripetalForceSetting.trackRange);
        }
        gameObject.SetActive(false);
    }

    public void OnDifficulty(int _difficulty)
    {
        Public.setting.centripetalForceDifficulty = (Difficulty)_difficulty;
    }
}
