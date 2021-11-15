using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LightManager : MonoBehaviour
{
    //게임 시작 여부
    private bool started = false;
    public bool Started
    {
        get
        {
            return started;
        }
    }

    //빛 생성 지점
    [SerializeField]
    private Transform lightSpawnPoint;
    //빛 프리팹
    [SerializeField]
    private GameObject lightPrefab;

    //빛
    private GameObject light;

    //게임 시작
    private void StartGame()
    {
        if (light != null)
        {
            Destroy(light);
        }
        light  = Instantiate(lightPrefab, lightSpawnPoint.position, Quaternion.identity);
        started = true;
    }

    //게임 실패
    public void FailedGame()
    {
        started = false;
    }

    //게임 성공
    public void SucceedGame()
    {
        started = false;
    }


    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(Key.EXIT))
        {
            SceneManager.LoadScene(SceneName.MAIN);
        }
        if (!started)
        {
            if (Input.GetKeyDown(Key.START))
            {
                StartGame();
            }
        }
    }
}
