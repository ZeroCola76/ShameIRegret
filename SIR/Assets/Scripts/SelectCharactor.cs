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
            isMoving = true; // �̵� ����
            rectTransform.DOAnchorPosX(rectTransform.anchoredPosition.x + 1800, 0.5f)
                         .SetEase(Ease.InOutQuad)
                         .OnComplete(() => isMoving = false); // �̵� �Ϸ� �� �÷��� �缳��
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
    /// �� �̰� �� ��ġ�°Ŷ� ���� �ᵵ �ɵ�?
    /// �̷����ϸ� �� ������ �ƴѰŰ����� �� �𸣰ڴ�
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
