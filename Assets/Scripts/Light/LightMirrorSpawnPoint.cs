using System.Collections.Generic;
using UnityEngine;

public class LightMirrorSpawnPoint : MonoBehaviour
{
    //거울 프리팹
    [SerializeField]
    private GameObject mirrorPrefab;
    //거울
    private List<GameObject> mirrorsInstantiated = new List<GameObject>();

    //거울 생성 지점 메쉬
    [SerializeField]
    private GameObject mirrorSpawnPointMesh;

    //빛 게임 매니저
    private LightManager gameManager;
    private void Awake()
    {
        //빛 게임 매니저 초기화
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
