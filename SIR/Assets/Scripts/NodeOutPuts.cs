using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;
using TMPro; //�̷� �� �ִٴ�


/// <summary>
/// ��Ʈ ��±�
/// 
/// �迹���� �ۼ�
/// </summary>
public class NodeOutPuts : MonoBehaviour
{
    public SODatas noteDataContainer;

    public GameObject[] wasdPrefabs;

    public RectTransform[] spawnPoints;

    private float inGameTime;
    private int currentIndex = 0;
    public Canvas canvas; // ĵ���� ����

    private NodeDeterminator nodeDeterminator;

    private ObjectPool<GameObject> nodePool;

    public Image clear;

    private bool isClear; //���ϰ� �߰�

    public TextMeshProUGUI combo;

    void Start()
    {
        noteDataContainer = GameManager.Instance.sODatas;
        nodeDeterminator = GetComponent<NodeDeterminator>();

        StartCoroutine(SpawnNotes());

        SoundManager.Instance.PlaySong(GameManager.Instance.songCount-1);

       
    }

    private void Update()
    {
        inGameTime += Time.deltaTime;

        if(isClear && (Input.GetKeyDown(KeyCode.Escape)))
        {
            isClear = false;
            combo.text = "0";
            clear.gameObject.SetActive(false);
            PoolManager.Instance.InitializePools();
            CumstomSceneManager.Instance.LoadScene(3);
        }

        combo.text = nodeDeterminator.scoreTxt.text;
    }

    private IEnumerator SpawnNotes()
    {
        while (currentIndex < noteDataContainer.noteDataArray.Length)
        {
            float noteTime = noteDataContainer.noteDataArray[currentIndex].time / 1000f; 
            float delta = Mathf.Abs(noteTime - inGameTime);

            //Debug.Log(inGameTime);
            if (inGameTime>= noteTime -1f) /*(delta < 0.1f)*/ //���� 0.01�̿��� �̷��� .,..�ǳ�?
            {
                SpawnNote(noteDataContainer.noteDataArray[currentIndex]);
                currentIndex++;
            }

            yield return null; // ���� �����ӱ��� ���
        }

        yield return new WaitForSeconds(5f); 
        GameClear();
    }

    private void SpawnNote(SODatas.NoteData note)
    {
        int spawnIndex = GetSpawnIndex(note.x);
        if (spawnIndex >= 0 && spawnIndex < spawnPoints.Length)
        {
            string poolKey = $"Prefab{spawnIndex % 2}Pool";
            GameObject prefab = wasdPrefabs[spawnIndex % 2];
            GameObject instance = PoolManager.Instance.GetObject(poolKey, prefab, canvas.transform);
            RectTransform rectTransform = instance.GetComponent<RectTransform>();
            rectTransform.SetParent(canvas.transform, false);
            rectTransform.anchoredPosition = spawnPoints[spawnIndex].GetComponent<RectTransform>().anchoredPosition;
            if (note.type == 128) // �� ��Ʈ
            {
                float longNoteDuration = note.longNoteTime - note.time;

                //float longNoteHeight = prefab.GetComponent<RectTransform>().sizeDelta.y;

                // �ճ�Ʈ�� Height ����
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, longNoteDuration);

                float startPositionY = rectTransform.anchoredPosition.y + (rectTransform.sizeDelta.y / 4);
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, startPositionY);

                float baseSpeed = 1.3f / 800f; 
                float longNoteHeight = rectTransform.sizeDelta.y/4;
                float totalDistance = 800 + longNoteHeight; 
                float adjustedDuration = baseSpeed * totalDistance; 

                float targetPositionY = -800 - (rectTransform.sizeDelta.y/4); 
                rectTransform.DOAnchorPosY(targetPositionY, adjustedDuration).SetEase(Ease.Linear).OnComplete(() =>
                {
                    ///�߼��� ��ġ����!
                    //PoolManager.Instance.ReturnObject(poolKey, instance);
                    nodePool.Release(instance);
                });
                nodeDeterminator.NodeBoard(instance, spawnIndex,true, adjustedDuration);
            }
            else
            {

                rectTransform.DOAnchorPosY(-800, 1.3f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    //PoolManager.Instance.ReturnObject(poolKey, instance);
                    nodePool.Release(instance);
                });
                nodeDeterminator.NodeBoard(instance, spawnIndex, false,-1);
            }

        }
    }

    private int GetSpawnIndex(int x)
    {
        switch (x)
        {
            case 64: return 0;
            case 192: return 1;
            case 320: return 2;
            case 448: return 3;
            default: return -1; 
        }
    }

    void GameClear()
    {
        isClear = true;
        clear.gameObject.SetActive(true);
    }
}
