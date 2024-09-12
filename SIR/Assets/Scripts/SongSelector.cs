using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongSelector : MonoBehaviour
{
    public GameObject buttonPrefab; // ��ư ������
    public Transform contentPanel;  // Scroll View�� ������ �г�
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlayMusic(2);

        for (int i = 0; i < GameManager.Instance.songList.Count; i++)
        {
            GameObject newButton = Instantiate(buttonPrefab, contentPanel);
            SelectSongbtn songButton = newButton.GetComponent<SelectSongbtn>();
            songButton.songIndex = i;

            Text buttonText = newButton.GetComponentInChildren<Text>();
            //buttonText.text = GameManager.Instance.songList[i].title;
        }
    }
}
