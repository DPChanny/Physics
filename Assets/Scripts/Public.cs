using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public static class Public
{
    //���� ����
    public static Setting setting = Setting.defaultSetting;
    public static Game game;
    public static Record record = null;

    //��� �ε�
    public static void LoadRecords()
    {
        if (!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }
        if (!File.Exists(Application.streamingAssetsPath + "/record.json"))
        {
            record = new Record();
            return;
        }
        StreamReader streamReader = new StreamReader(Application.streamingAssetsPath + "/record.json");
        record = JsonUtility.FromJson<RecordWrapper>(streamReader.ReadToEnd()).Unwrap();
        streamReader.Close();
    }

    //��� ����
    public static void SaveRecords()
    {
        if (!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }
        StreamWriter streamWriter = new StreamWriter(Application.streamingAssetsPath + "/record.json", false);
        streamWriter.Write(JsonUtility.ToJson(record.Wrap(), true));
        streamWriter.Close();
    }

    //ȭ�� �ε�
    public static void LoadScene(string _scene)
    {
        Camera.main.GetComponent<CameraManager>().In(5, new UnityAction(() => SceneManager.LoadScene(_scene)));
    }
}
