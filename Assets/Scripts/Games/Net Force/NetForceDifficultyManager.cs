using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NetForceDifficultyManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField InputUI_speed;
    [SerializeField]
    private TMP_InputField InputUI_objectRange;
    [SerializeField]
    private TMP_InputField InputUI_maxForcePower;
    [SerializeField]
    private TMP_InputField InputUI_minForcePower;
    [SerializeField]
    private Toggle ToggleUI_enemyMove;
    [SerializeField]
    private Toggle ToggleUI_enemyForceDirectionToggle;
    [SerializeField]
    private Toggle ToggleUI_enemyForcePowerToggle;

    [SerializeField]
    private GameObject UI;

    public bool Active
    {
        get
        {
            return UI.activeInHierarchy;
        }
    }

    private NetForceManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag(Tag.GAME_MANAGER).GetComponent<NetForceManager>();
    }

    private void Init()
    {
        InputUI_speed.interactable = 
            Public.setting.netForceDifficulty == Difficulty.Custom;
        InputUI_objectRange.interactable = 
            Public.setting.netForceDifficulty == Difficulty.Custom;
        InputUI_maxForcePower.interactable = 
            Public.setting.netForceDifficulty == Difficulty.Custom;
        InputUI_minForcePower.interactable = 
            Public.setting.netForceDifficulty == Difficulty.Custom;
        ToggleUI_enemyMove.interactable = 
            Public.setting.netForceDifficulty == Difficulty.Custom;
        ToggleUI_enemyForceDirectionToggle.interactable = 
            Public.setting.netForceDifficulty == Difficulty.Custom;
        ToggleUI_enemyForcePowerToggle.interactable = 
            Public.setting.netForceDifficulty == Difficulty.Custom;

        InputUI_speed.text = Public.setting.netForceSetting.speed.ToString();
        InputUI_objectRange.text = Public.setting.netForceSetting.objectRange.ToString();
        InputUI_maxForcePower.text = Public.setting.netForceSetting.maxForcePower.ToString();
        InputUI_minForcePower.text = Public.setting.netForceSetting.minForcePower.ToString();
        ToggleUI_enemyMove.isOn = Public.setting.netForceSetting.enemyMove;
        ToggleUI_enemyForceDirectionToggle.isOn = 
            Public.setting.netForceSetting.enemyForceDirectionToggle;
        ToggleUI_enemyForcePowerToggle.isOn = 
            Public.setting.netForceSetting.enemyForcePowerToggle;
    }

    private void Update()
    {
        if (!Active && !gameManager.Started)
        {
            if (Input.GetKeyDown(Key.DIFFICULTY_MANAGER))
            {
                UI.SetActive(true);

                Init();
            }
        }
    }

    public void OnConfirm()
    {
        if (Public.setting.netForceDifficulty == Difficulty.Custom)
        {
            float.TryParse(InputUI_speed.text, out Public.setting.netForceSetting.speed);
            float.TryParse(InputUI_objectRange.text, out Public.setting.netForceSetting.objectRange);
            float.TryParse(InputUI_maxForcePower.text, out Public.setting.netForceSetting.maxForcePower);
            float.TryParse(InputUI_minForcePower.text, out Public.setting.netForceSetting.minForcePower);
            Public.setting.netForceSetting.enemyMove = ToggleUI_enemyMove.isOn;
            Public.setting.netForceSetting.enemyForceDirectionToggle = 
                ToggleUI_enemyForceDirectionToggle.isOn;
            Public.setting.netForceSetting.enemyForcePowerToggle = 
                ToggleUI_enemyForcePowerToggle.isOn;
        }
        UI.SetActive(false);
    }

    public void OnDifficulty(int _difficulty)
    {
        Public.setting.netForceDifficulty = (Difficulty)_difficulty;

        Init();
    }
}
