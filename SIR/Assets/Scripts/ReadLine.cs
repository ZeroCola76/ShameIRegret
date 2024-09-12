using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class ReadLine : MonoBehaviour
{
    public List<TextAsset> dcsvList = new List<TextAsset>();
    // Start is called before the first frame update
    void Start()
    {
        TextAsset[] csvFiles = Resources.LoadAll<TextAsset>("DCSV");

        foreach (TextAsset song in csvFiles)
        {
            dcsvList.Add(song);
        }

        if (csvFiles != null && csvFiles.Length > 0)
        {
            foreach (TextAsset csvFile in csvFiles)
            {
                List<DLDatas> noteDataList = ParseCsvFile(csvFile.text);
                SaveNoteDataToScriptableObject(noteDataList, csvFile.name);
            }
        }
        else
        {
            Debug.LogError("파일 없는데");
        }
    }

    List<DLDatas> ParseCsvFile(string csvText)
    {
        List<DLDatas.TalkData> noteDataList = new List<DLDatas.TalkData>();
        StringReader reader = new StringReader(csvText);

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine().Trim();  // 양쪽 공백 제거
            string[] values = line.Split(',');
            string name = values[0];
            string linetxt = values[1];

            DLDatas.TalkData noteData = new DLDatas.TalkData
            {
                name = name,
                line = linetxt
            };

            noteDataList.Add(noteData);
        }

        DLDatas dialogData = ScriptableObject.CreateInstance<DLDatas>();
        dialogData.talkDataArray = noteDataList.ToArray();

        // 생성된 ScriptableObject를 리스트로 반환
        List<DLDatas> dlDataList = new List<DLDatas> { dialogData };
        return dlDataList;
    }

    void SaveNoteDataToScriptableObject(List<DLDatas> noteDataList, string fileName)
    {
#if UNITY_EDITOR
        foreach (DLDatas songData in noteDataList)
        {
            // ScriptableObject 저장 (Asset으로 생성)
            string filePath = $"Assets/Resources/Line/{fileName}.asset"; // 파일 이름을 이용해 저장 경로 지정
            UnityEditor.AssetDatabase.CreateAsset(songData, filePath);
            UnityEditor.AssetDatabase.SaveAssets();

            Debug.Log("dSO 생성 성공");
        }
#else
        Debug.LogWarning("AssetDatabase is not available at runtime!");
#endif
    }
}
