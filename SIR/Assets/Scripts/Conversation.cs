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
        //.OnComplete(OnFadeInComplete); // ���̵� �� �Ϸ� �� ������ �Լ� ����

        if (camera != null && waypoints != null && waypoints.Length > 0)
        {
            // ������ �迭�� ��ġ�� ����3 �迭�� ��ȯ
            Vector3[] path = new Vector3[waypoints.Length];
            for (int i = 0; i < waypoints.Length; i++)
            {
                path[i] = waypoints[i].position;
            }

            // ���߾� ī�޶��� Transform�� ������ �迭�� ���� �̵�
            camera.transform.DOPath(path, 5, PathType.CatmullRom)
                .SetEase(Ease.InOutQuad)
                .OnComplete(StartTalk);
                //.SetLookAt(0.01f); // �̵� �� ��θ� �ٶ󺸵��� ����
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
            Debug.Log("�����µ�");

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
