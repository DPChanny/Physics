using System.Collections.Generic;
using UnityEngine;

public class LightMirrorSpawnPoint : MonoBehaviour
{
    //�ſ� ������
    [SerializeField]
    private GameObject mirrorPrefab;
    //�ſ�
    private List<GameObject> mirrorsInstantiated = new List<GameObject>();

    //�ſ� ���� ���� �޽�
    [SerializeField]
    private GameObject mirrorSpawnPointMesh;

    //�� ���� �Ŵ���
    private LightManager gameManager;
    private void Awake()
    {
        //�� ���� �Ŵ��� �ʱ�ȭ
        gameManager = GameObject.FindGameObjectWithTag(Tag.GAME_MANAGER).GetComponent<LightManager>();
    }

    private void Update()
    {
        mirrorSpawnPointMesh.SetActive(!gameManager.Started);

        if (gameManager.Started)
        {
            return;
        }

        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");
        transform.Rotate(0f, 0f, scroll * 50f);
        if (Input.GetMouseButtonDown(Key.MOUSE_LEFT))
        {
            mirrorsInstantiated.Add(Instantiate(mirrorPrefab, transform.position, transform.rotation));
        }
        if (Input.GetMouseButtonDown(Key.MOUSE_RIGHT))
        {
            if(mirrorsInstantiated.Count != 0)
            {
                Destroy(mirrorsInstantiated[mirrorsInstantiated.Count - 1]);
                mirrorsInstantiated.RemoveAt(mirrorsInstantiated.Count - 1);
            }
        }
    }
}
