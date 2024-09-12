using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextUpDownAnimation : MonoBehaviour
{
    private TextMeshProUGUI textToAnimate;
    private float moveDistance = 1f;
    private float duration = 0.5f;
    private Ease easeType = Ease.InOutBounce; //�̰� ���� ������ �� �𸣰��� 

    void Start()
    {
        textToAnimate = GetComponent<TextMeshProUGUI>();

        // Text�� ���Ʒ��� �ݺ������� �����̴� �ִϸ��̼� ����
        textToAnimate.rectTransform.DOBlendableMoveBy(new Vector3(0, moveDistance, 0), duration)
            .SetEase(easeType)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
