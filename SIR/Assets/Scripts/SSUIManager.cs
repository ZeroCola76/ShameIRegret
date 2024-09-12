using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SSUIManager : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI artist;
    public TextMeshProUGUI level;
    public TextMeshProUGUI leonHeart;
    public TextMeshProUGUI zeroHeart;
    public TextMeshProUGUI coin;
    public Image cover;
    public List<Sprite> coverList = new List<Sprite>();

    private void Start()
    {
        Sprite[] covers = Resources.LoadAll<Sprite>("Cover");

        foreach (Sprite cover in covers)
        {
            coverList.Add(cover);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.sODatas != null)
        {
            title.text = GameManager.Instance.sODatas.title;
            artist.text = GameManager.Instance.sODatas.artist;
            level.text = GameManager.Instance.sODatas.level;
            leonHeart.text = GameManager.Instance.leonHeart.ToString();
            zeroHeart.text = GameManager.Instance.zeroHeart.ToString();
            coin.text = GameManager.Instance.coin.ToString();
            cover.sprite = coverList[GameManager.Instance.songCount];
        }

    }
}
