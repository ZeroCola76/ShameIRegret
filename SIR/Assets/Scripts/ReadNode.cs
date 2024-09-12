using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static SODatas;
using UnityEngine.UI;
using Unity.VisualScripting;

/// <summary>
/// csv 파일을 파싱한다. ,를 기준으로 
/// 나중에 모노비헤이비어를 뗄 예정이다. 지금은 디버그 때문에...
/// 
/// 김예리나 작성
/// </summary>
[DefaultExecutionOrder(-100)]
public class ReadNode : MonoBehaviour
{
    //public TextAsset[] csvFile; //여기에 csv 파일을 할당할거다.
    public List<TextAsset> csvList = new List<TextAsset>();
    void Start()
    {
        TextAsset[] csvFiles = Resources.LoadAll<TextAsset>("CSV");

        foreach (TextAsset song in csvFiles)
        {
            csvList.Add(song);
        }

        if (csvFiles != null && csvFiles.Length > 0)
        {
            foreach (TextAsset csvFile in csvFiles)
            {
                List<SODatas> noteDataList = ParseCsvFile(csvFile.text);
                SaveNoteDataToScriptableObject(noteDataList,csvFile.name);
            }
        }
        else
        {
            Debug.LogError("파일 없는데");
        }
    }

    List<SODatas> ParseCsvFile(string csvText)
    {
        List<SODatas.NoteData> noteDataList = new List<SODatas.NoteData>();
        StringReader reader = new StringReader(csvText);
        string title = "";
        string artist = "";
        string level = "";
        bool isTitleParsed = false;
        bool isArtistParsed = false;
        bool isLevelParsed = false;

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine().Trim();  // 양쪽 공백 제거

            // 불필요한 쉼표 제거
            line = line.TrimEnd(',');


            // 제목과 아티스트를 포함하는 줄을 처음으로 한 번만 처리
            if (!isTitleParsed && line.StartsWith("Title-"))
            {
                title = line.Substring(6).Trim();
                isTitleParsed = true;
                continue;
            }
            if (!isArtistParsed && line.StartsWith("Artist-"))
            {
                artist = line.Substring(7).Trim();
                isArtistParsed = true;
                continue;
            }
            if (!isLevelParsed && line.StartsWith("Level-"))
            {
                level = line.Substring(6).Trim();
                isLevelParsed = true;
                continue;
            }

            // 노트 데이터를 포함하는 줄을 처리
            string[] values = line.Split(',');
            if (values.Length < 5) continue; // 유효하지 않은 줄 무시

            int x = int.Parse(values[0]);
            int y = int.Parse(values[1]);
            int time = int.Parse(values[2]);
            int type = int.Parse(values[3]);
            int dummy = int.Parse(values[4]);
            int longNoteTime = -1;

            // 롱노트인 경우 롱노트 시간 파싱
            if (type == 128 && values.Length > 5)
            {
                string[] longNoteData = values[5].Split(':');
                if (longNoteData.Length > 0)
                {
                    longNoteTime = int.Parse(longNoteData[0]);
                }
                else
                {
                    Debug.LogWarning("롱노트 시간의 값이 없습니다!");
                }
            }

            // NoteData 객체 생성 및 설정
            SODatas.NoteData noteData = new SODatas.NoteData
            {
                x = x,
                y = y,
                time = time,
                type = type,
                longNoteTime = longNoteTime
            };

            // 생성된 NoteData를 리스트에 추가
            noteDataList.Add(noteData);
        }

        // ScriptableObject 생성 및 설정
        SODatas songData = ScriptableObject.CreateInstance<SODatas>();
        songData.title = title;
        songData.artist = artist;
        songData.level = level;
        songData.noteDataArray = noteDataList.ToArray();

        // 생성된 ScriptableObject를 리스트로 반환
        List<SODatas> songDataList = new List<SODatas> { songData };
        return songDataList;
    }

    void SaveNoteDataToScriptableObject(List<SODatas> noteDataList, string fileName)
    {
#if UNITY_EDITOR
        foreach (SODatas songData in noteDataList)
        {
            // ScriptableObject 저장 (Asset으로 생성)
            string filePath = $"Assets/Resources/Song/{fileName}.asset"; // 파일 이름을 이용해 저장 경로 지정
            UnityEditor.AssetDatabase.CreateAsset(songData, filePath);
            UnityEditor.AssetDatabase.SaveAssets();

            Debug.Log("SO 생성 성공");
        }
#else
        Debug.LogWarning("AssetDatabase is not available at runtime!");
#endif
    }
}
