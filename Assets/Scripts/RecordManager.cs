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
                "���� ����: " + (Public.record.centripetalForceRecords[i].succeed ? "����" : "����") +
                " ���̵�: " + DifficultyToString(Public.record.centripetalForceRecords[i].difficulty);
        }
        for (int i = 0; i < Public.record.netForceRecords.Count; i++)
        {
            int index = i;
            Button button = ScrollUI_netForce.AddUI(i.ToString(), ButtonUI_prefab).GetComponent<Button>();
            if (Public.record.netForceRecords[i].difficulty == Difficulty.Custom)
                button.onClick.AddListener(new UnityAction(() => OnNetForce(index)));
            button.GetComponentInChildren<TextMeshProUGUI>().text =
                "����: " + Public.record.netForceRecords[i].score.ToString("n2") +
                " ���̵�: " + DifficultyToString(Public.record.netForceRecords[i].difficulty);
        }
        for (int i = 0; i < Public.record.lightRecords.Count; i++)
        {
            int index = i;
            Button button = ScrollUI_light.AddUI(i.ToString(), ButtonUI_prefab).GetComponent<Button>();
            button.GetComponentInChildren<TextMeshProUGUI>().text =
                "���� ����: " + (Public.record.lightRecords[i].succeed ? "����" : "����") +
                " �ſ� ����: " + Public.record.lightRecords[i].mirrorCount.ToString() +
                " ��� �ð�: " + Public.record.lightRecords[i].elapsedTime.ToString("n2");
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
            return "����";
        else if (_difficulty == Difficulty.Easy)
            return "����";
        else if (_difficulty == Difficulty.Hard)
            return "�����";
        else return "����� ����";
    }

    private void Update()
    {
        if (Input.GetKeyDown(Key.EXIT))
        {
            Public.LoadScene(SceneName.MAIN);
        }
    }
}
