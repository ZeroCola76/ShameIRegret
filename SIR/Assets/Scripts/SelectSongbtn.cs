using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SelectSongbtn : MonoBehaviour
{
    public int songIndex;
    private Button button;
    public TextMeshProUGUI songName;
    private GameObject charPanel;

    // Start is called before the first frame update
    void Start()
    {
        GameObject topParent = GetTopParent(this.gameObject);
        charPanel = FindChildByName(topParent, "CharSelect");

        button = GetComponent<Button>();
        songName.text = GameManager.Instance.songList[songIndex].title;
        EventTrigger eventTrigger = button.gameObject.GetComponent<EventTrigger>();
        if (eventTrigger == null)
        {
            eventTrigger = button.gameObject.AddComponent<EventTrigger>();

        }
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter; 
        entry.callback.AddListener((data) => { OnMouseEnterHandler(); });
        eventTrigger.triggers.Add(entry);
        button.onClick.AddListener(showCharSelect); //원래 로드씬이였던것
    }
    void OnMouseEnterHandler()
    {
        Debug.Log("마우스를 올렸습니다!");

        GameManager.Instance.SelectedSong(songIndex);
    }

    void showCharSelect()
    {
        if (songIndex == 0)
        {
            GameManager.Instance.SelectRandomSong(songIndex);
        }

        charPanel.SetActive(true);
    }

    public GameObject GetTopParent(GameObject obj)
    {
        Transform currentTransform = obj.transform;
        while (currentTransform.parent != null)
        {
            currentTransform = currentTransform.parent;
        }
        return currentTransform.gameObject;
    }

    public GameObject FindChildByName(GameObject parent, string childName)
    {
        Transform childTransform = parent.transform.Find(childName);
        if (childTransform != null)
        {
            return childTransform.gameObject;
        }
        return null;
    }
}
