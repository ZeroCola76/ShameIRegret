using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Conversation : MonoBehaviour
{
    public CinemachineVirtualCamera camera;
    public Transform[] waypoints;
    public Image diaImg;
    public TextMeshProUGUI name;
    public TextMeshProUGUI line;
    public Image fadeout;
    int index = 1;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlayMusic(0);

        fadeout.DOFade(0f, 2)
                  .SetEase(Ease.Linear);
        //.OnComplete(OnFadeInComplete); // 페이드 인 완료 후 실행할 함수 지정

        if (camera != null && waypoints != null && waypoints.Length > 0)
        {
            // 경유지 배열의 위치를 벡터3 배열로 변환
            Vector3[] path = new Vector3[waypoints.Length];
            for (int i = 0; i < waypoints.Length; i++)
            {
                path[i] = waypoints[i].position;
            }

            // 버추얼 카메라의 Transform을 경유지 배열을 따라 이동
            camera.transform.DOPath(path, 5, PathType.CatmullRom)
                .SetEase(Ease.InOutQuad)
                .OnComplete(StartTalk);
                //.SetLookAt(0.01f); // 이동 중 경로를 바라보도록 설정
        }
    }

    void StartTalk()
    {
        diaImg.gameObject.SetActive(true);
        name.gameObject.SetActive(true);
        line.gameObject.SetActive(true);

        name.text = GameManager.Instance.dlDatas.talkDataArray[0].name;
        line.text = GameManager.Instance.dlDatas.talkDataArray[0].line;
    }
    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Debug.Log("눌렀는데");

            if (index >= GameManager.Instance.dlDatas.talkDataArray.Length)// return;
            {
                CumstomSceneManager.Instance.LoadScene(3);
                return;
            }

            name.text = GameManager.Instance.dlDatas.talkDataArray[index].name;
            line.text = GameManager.Instance.dlDatas.talkDataArray[index].line;
            index++;
        }
    }
}
