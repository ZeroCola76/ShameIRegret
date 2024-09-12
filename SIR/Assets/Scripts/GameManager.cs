using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-50)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // 싱글톤 인스턴스
    private string playerName; //플레이어 이름
    public int coin; //코인
    public int zeroHeart; //제로 호감도
    public int leonHeart; //레온 호감도
    public List<SODatas> songList = new List<SODatas>();
    public List<DLDatas> lineList = new List<DLDatas>();
    public SODatas sODatas;
    public DLDatas dlDatas;
    public int songCount = -1;
    public bool isVelvet;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        SODatas[] songs = Resources.LoadAll<SODatas>("Song");
        DLDatas[] lines = Resources.LoadAll<DLDatas>("Line");
        SODatas emptySong = new SODatas();
        emptySong.title = "랜덤곡 선택";
        songList.Add(emptySong);

        foreach (SODatas song in songs)
        {
            songList.Add(song);
        }

        foreach(DLDatas line in lines)
        {
            lineList.Add(line);
        }

        sODatas = songList[0];
        dlDatas = lineList[0];

        isVelvet = true;
    }

    public void SelectedSong(int index)
    {
        if (index >= 0 && index < songList.Count)
        {
            songCount = index;
            sODatas = songList[index];
        }
    }

    public void SelectRandomSong(int index)
    {
        int randomIndex = UnityEngine.Random.Range(1, songList.Count);

        //songList[0] = songList[randomIndex];
        songCount = randomIndex;
        sODatas = songList[randomIndex];
    }
}
