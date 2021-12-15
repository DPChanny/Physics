using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//스크롤 UI 제어
public class ScrollViewManager : MonoBehaviour
{
    [SerializeField]
    private float space; //추가된 UI 사이 공간

    [SerializeField]
    private ScrollRect scrollRect; //스크롤 UI

    //추가된 UI 딕셔너리
    public readonly Dictionary<string, RectTransform> rectTransforms = new Dictionary<string, RectTransform>();

    [HideInInspector]
    public readonly List<string> keys = new List<string>(); //추가된 UI 키

    //UI 추가
    public GameObject AddUI(string _key, GameObject _UI)
    {
        RectTransform newUI = Instantiate(_UI, scrollRect.content).GetComponent<RectTransform>();
        rectTransforms.Add(_key, newUI);
        keys.Add(_key);
        UpdateScrollView();
        return newUI.gameObject;
    }

    //스크롤 UI 업데이트
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

    //스크롤 UI 초기화
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