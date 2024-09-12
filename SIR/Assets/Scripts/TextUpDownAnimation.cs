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
    private Ease easeType = Ease.InOutBounce; //이거 뭐가 좋은지 잘 모르겠음 

    void Start()
    {
        textToAnimate = GetComponent<TextMeshProUGUI>();

        // Text를 위아래로 반복적으로 움직이는 애니메이션 설정
        textToAnimate.rectTransform.DOBlendableMoveBy(new Vector3(0, moveDistance, 0), duration)
            .SetEase(easeType)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
