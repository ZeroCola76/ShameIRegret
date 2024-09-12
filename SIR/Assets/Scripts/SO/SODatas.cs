using UnityEngine;

//order는 우선순위
/// <summary>
/// 2차 파싱되는 정보를 담는 SO클래스
/// </summary>
[CreateAssetMenu(fileName = "SODatas", menuName = "SOData/NoteData", order = 1)]
public class SODatas : ScriptableObject
{
    public string title;
    public string artist;
    public string level;
    public NoteData[] noteDataArray; // 노트 데이터 배열

    [System.Serializable]
    public class NoteData
    {
        public int x;
        public int y;
        public int time;
        public int type;
        //public int dummy;
        public int longNoteTime;
    }
}
