using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class RecordManager : MonoBehaviour
{
    [SerializeField]
    private ScrollViewManager ScrollUI_centripetalForce;
    [SerializeField]
    private ScrollViewManager ScrollUI_netForce;
    [SerializeField]
    private ScrollViewManager ScrollUI_light;

    [SerializeField]
    private GameObject ButtonUI_prefab;

    private void Start()
    {
        for (int i = 0; i < Public.record.centripetalForceRecords.Count; i++)
        {
            int index = i;
            Button button = ScrollUI_centripetalForce.AddUI(i.ToString(), ButtonUI_prefab).GetComponent<Button>();
            if (Public.record.centripetalForceRecords[i].difficulty == Difficulty.Custom)
                button.onClick.AddListener(new UnityAction(() => OnCentripetalForce(index)));
            button.GetComponentInChildren<TextMeshProUGUI>().text =
                "성공 여부: " + (Public.record.centripetalForceRecords[i].succeed ? "성공" : "실패") +
                " 난이도: " + DifficultyToString(Public.record.centripetalForceRecords[i].difficulty);
        }
        for (int i = 0; i < Public.record.netForceRecords.Count; i++)
        {
            int index = i;
            Button button = ScrollUI_netForce.AddUI(i.ToString(), ButtonUI_prefab).GetComponent<Button>();
            if (Public.record.netForceRecords[i].difficulty == Difficulty.Custom)
                button.onClick.AddListener(new UnityAction(() => OnNetForce(index)));
            button.GetComponentInChildren<TextMeshProUGUI>().text =
                "점수: " + Public.record.netForceRecords[i].score.ToString("n2") +
                " 난이도: " + DifficultyToString(Public.record.netForceRecords[i].difficulty);
        }
        for (int i = 0; i < Public.record.lightRecords.Count; i++)
        {
            int index = i;
            Button button = ScrollUI_light.AddUI(i.ToString(), ButtonUI_prefab).GetComponent<Button>();
            button.GetComponentInChildren<TextMeshProUGUI>().text =
                "성공 여부: " + (Public.record.lightRecords[i].succeed ? "성공" : "실패") +
                " 거울 개수: " + Public.record.lightRecords[i].mirrorCount.ToString() +
                " 경과 시간: " + Public.record.lightRecords[i].elapsedTime.ToString("n2");
        }
    }

    private void OnCentripetalForce(int _index)
    {

    }

    private void OnNetForce(int _index)
    {

    }

    private string DifficultyToString(Difficulty _difficulty)
    {
        if (_difficulty == Difficulty.Normal)
            return "보통";
        else if (_difficulty == Difficulty.Easy)
            return "쉬움";
        else if (_difficulty == Difficulty.Hard)
            return "어려움";
        else return "사용자 설정";
    }

    private void Update()
    {
        if (Input.GetKeyDown(Key.EXIT))
        {
            Public.LoadScene(SceneName.MAIN);
        }
    }
}
