using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

/// <summary>
/// 노드 판정하는 클래스
/// </summary>
public class NodeDeterminator : MonoBehaviour
{
    public TextMeshProUGUI judgeTxt;
    public TextMeshProUGUI scoreTxt;
    public Image hpBar;
    private int score;

    public void NodeBoard(GameObject instance, int spawnIndex, bool isLong, float longnoteTime)
    {
        StartCoroutine(NodeBoardRoutine(instance, spawnIndex,isLong, longnoteTime));
    }

    private IEnumerator NodeBoardRoutine(GameObject instance, int spawnIndex, bool isLong, float longnoteTime)
    {
        RectTransform rectTransform = instance.GetComponent<RectTransform>();
        bool keyAlreadyPressed = false;
        float longNoteStartTime = 0;

        if (isLong)
        {
            while (true)
            {
                float yOffset = Mathf.Abs(rectTransform.anchoredPosition.y - (rectTransform.sizeDelta.y / 4) + 464f);
                bool isJudged = false; // 롱노트 판정 상태 초기화

                // 초기 키 입력 체크 및 yOffset에 따른 판정
                if (CheckKeyPress(spawnIndex) && !isJudged)
                {
                    //SoundManager.Instance.PlayEffect(0);
                    if (yOffset <= 50)
                    {
                        judgeTxt.text = "PERFECT";
                        isJudged = true;
                        keyAlreadyPressed = true;
                        longNoteStartTime = Time.time;
                    }
                    else if (yOffset <= 100)
                    {
                        judgeTxt.text = "GREAT";
                        isJudged = true;
                        keyAlreadyPressed = true;
                        longNoteStartTime = Time.time;
                    }
                    else if (yOffset <= 150)
                    {
                        judgeTxt.text = "GOOD";
                        isJudged = true;
                        keyAlreadyPressed = true;
                        longNoteStartTime = Time.time;
                    }
                }

                if (keyAlreadyPressed)
                {
                    if (CheckKeyPressing(spawnIndex))
                    {
                        // 키가 계속 눌려있으면 점수 증가
                        yield return new WaitForSeconds(0.5f);
                        score++;
                        scoreTxt.text = score.ToString();

                        // 롱노트가 끝났을 때 루틴 종료
                        if (Time.time - longNoteStartTime +1.5f >= longnoteTime)
                        {
                            //judgeTxt.text = "LONG NOTE COMPLETE";
                            yield break;
                        }
                    }
                    else // 키 입력을 중간에 뗐을 경우
                    {
                        judgeTxt.text = "MISS";
                        score = 0;
                        scoreTxt.text = score.ToString();
                        hpBar.fillAmount -= 0.1f;
                        if(hpBar.fillAmount == 0)
                        {
                            EventManager.Instance.SomethingHappens(EventType.rhythmDie);

                        }
                        else
                        {

                        EventManager.Instance.SomethingHappens(EventType.rhythmHurt);
                        }
                        yield break;
                    }
                }

                // 노트가 화면을 벗어났을 때 처리
                if (rectTransform.anchoredPosition.y < -700 && !keyAlreadyPressed)
                {
                    judgeTxt.text = "MISS";
                    score = 0;
                    scoreTxt.text = score.ToString();
                    hpBar.fillAmount -= 0.1f;
                    if (hpBar.fillAmount == 0)
                    {
                        EventManager.Instance.SomethingHappens(EventType.rhythmDie);

                    }
                    else
                    {

                        EventManager.Instance.SomethingHappens(EventType.rhythmHurt);
                    }
                    yield break;
                }

                yield return null;
            }
        }
        else
        {
            while (true)
            {
                if (CheckKeyPress(spawnIndex))
                {

                    float yOffset = Mathf.Abs(rectTransform.anchoredPosition.y + 464f);
                    //SoundManager.Instance.PlayEffect(0);

                    if (yOffset <= 50)
                    {
                        judgeTxt.text = "PERFECT";
                        score++;
                        keyAlreadyPressed = true;
                        //Debug.Log("Perfect!");
                    }
                    else if (yOffset <= 100)
                    {
                        judgeTxt.text = "GREAT";
                        score++;
                        keyAlreadyPressed = true;
                        //Debug.Log("Great!");
                    }
                    else if (yOffset <= 150)
                    {
                        judgeTxt.text = "GOOD";
                        score++;
                        keyAlreadyPressed = true;
                        //Debug.Log("Good!");
                    }
                    //else
                    //{
                    //    judgeTxt.text = "MISS";
                    //    score = 0;
                    //    hpBar.fillAmount -= 0.1f;
                    //    Debug.Log("Miss!");
                    //}
                    scoreTxt.text = score.ToString();
                    //yield break;
                }

                if (rectTransform.anchoredPosition.y < -700 && !keyAlreadyPressed)
                {
                    judgeTxt.text = "MISS";
                    score = 0;
                    scoreTxt.text = score.ToString();
                    hpBar.fillAmount -= 0.1f;
                    if (hpBar.fillAmount == 0)
                    {
                        EventManager.Instance.SomethingHappens(EventType.rhythmDie);

                    }
                    else
                    {

                        EventManager.Instance.SomethingHappens(EventType.rhythmHurt);
                    }
                    //Debug.Log("Miss!");

                    yield break;
                }
                yield return null;
            }
        }
        
    }

    private bool CheckKeyPress(int spawnIndex)
    {
        switch (spawnIndex)
        {
            case 0:
                return Input.GetKeyDown(KeyCode.D);
            case 1:
                return Input.GetKeyDown(KeyCode.F);
            case 2:
                return Input.GetKeyDown(KeyCode.J);
            case 3:
                return Input.GetKeyDown(KeyCode.K);
            default:
                return false;
        }
    }

    private bool CheckKeyPressing(int spawnIndex)
    {
        switch (spawnIndex)
        {
            case 0:
                return Input.GetKey(KeyCode.D);
            case 1:
                return Input.GetKey(KeyCode.F);
            case 2:
                return Input.GetKey(KeyCode.J);
            case 3:
                return Input.GetKey(KeyCode.K);
            default:
                return false;
        }
    }
}