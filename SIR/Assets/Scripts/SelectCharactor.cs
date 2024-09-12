using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharactor : MonoBehaviour
{
    private Button Zero;
    private Button velvet;
    private RectTransform rectTransform;
    private bool isMoving = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        Zero = FindChildByName(this.gameObject, "Zero");
        velvet = FindChildByName(this.gameObject, "Velvet");

        Zero.onClick.AddListener(LoadSMainZero);
        velvet.onClick.AddListener(LoadSMainVelvet);

        if (!isMoving)
        {
            isMoving = true; // 이동 시작
            rectTransform.DOAnchorPosX(rectTransform.anchoredPosition.x + 1800, 0.5f)
                         .SetEase(Ease.InOutQuad)
                         .OnComplete(() => isMoving = false); // 이동 완료 후 플래그 재설정
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isMoving)
        {
            isMoving = true;
            this.gameObject.SetActive(false);
            rectTransform.DOAnchorPosX(rectTransform.anchoredPosition.x - 1800, 0.0001f)
                         .OnComplete(() => isMoving = false); 
        }
    }

    void LoadSMainZero()
    {
        CumstomSceneManager.Instance.LoadScene(4);
        GameManager.Instance.isVelvet = false;
    }
    void LoadSMainVelvet()
    {
        CumstomSceneManager.Instance.LoadScene(4);
        GameManager.Instance.isVelvet = true;
    }

    /// <summary>
    /// 음 이거 좀 겹치는거라서 모듈로 써도 될듯?
    /// 이런거하면 좀 정답은 아닌거같은데 잘 모르겠다
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="childName"></param>
    /// <returns></returns>
    public Button FindChildByName(GameObject parent, string childName)
    {
        Transform childTransform = parent.transform.Find(childName);
        if (childTransform != null)
        {
            return childTransform.GetComponent<Button>();
        }
        return null;
    }
}
