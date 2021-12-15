using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//��ũ�� UI ����
public class ScrollViewManager : MonoBehaviour
{
    [SerializeField]
    private float space; //�߰��� UI ���� ����

    [SerializeField]
    private ScrollRect scrollRect; //��ũ�� UI

    //�߰��� UI ��ųʸ�
    public readonly Dictionary<string, RectTransform> rectTransforms = new Dictionary<string, RectTransform>();

    [HideInInspector]
    public readonly List<string> keys = new List<string>(); //�߰��� UI Ű

    //UI �߰�
    public GameObject AddUI(string _key, GameObject _UI)
    {
        RectTransform newUI = Instantiate(_UI, scrollRect.content).GetComponent<RectTransform>();
        rectTransforms.Add(_key, newUI);
        keys.Add(_key);
        UpdateScrollView();
        return newUI.gameObject;
    }

    //��ũ�� UI ������Ʈ
    private void UpdateScrollView()
    {
        float y = 0f;
        for (int i = 0; i < keys.Count; i++)
        {
            rectTransforms[keys[i]].anchoredPosition = new Vector2(0f, -y);
            y += rectTransforms[keys[i]].sizeDelta.y + space;
        }
        scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, y);
    }

    //��ũ�� UI �ʱ�ȭ
    public void ResetScrollView()
    {
        Transform[] childList = scrollRect.content.GetComponentsInChildren<Transform>();
        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i] != transform)
                    Destroy(childList[i].gameObject);
            }
        }
        keys.Clear();
        rectTransforms.Clear();
    }
}