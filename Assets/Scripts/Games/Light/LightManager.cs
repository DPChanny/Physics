using TMPro;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    //�� ���� ����
    [SerializeField]
    private Transform lightSpawnPoint;
    //�� ������
    [SerializeField]
    private GameObject lightPrefab;
    //��
    private GameObject lightInstantiated;

    //��� �ð� �ؽ�Ʈ
    [SerializeField]
    private TextMeshProUGUI TextUI_elapsedTime;
    //��� �ð�
    private float elapsedTime = 0f;

    //���� ���� ����
    private bool started = false;
    public bool Started
    {
        get
        {
            return started;
        }
    }

    //���� ����
    private void StartGame()
    {
        if (lightInstantiated != null)
        {
            Destroy(lightInstantiated);
        }
        lightInstantiated = Instantiate(lightPrefab, lightSpawnPoint.position, Quaternion.identity);

        elapsedTime = 0f;

        started = true;
    }

    //���� ����
    public void FinishGame(bool _succeed)
    {
        Public.record.lightRecords.Add(
            new LightRecord(GameObject.FindGameObjectsWithTag(Tag.MIRROR).Length, elapsedTime, _succeed));
        started = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(Key.EXIT))
        {
            Public.LoadScene(SceneName.NOTE);
        }
        if (!started)
        {
            if (Input.GetKeyDown(Key.START))
            {
                StartGame();
            }
        }
        else
        {
            elapsedTime += Time.deltaTime;
            TextUI_elapsedTime.text = elapsedTime.ToString("n2");
        }
    }
}
