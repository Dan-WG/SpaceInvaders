using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONReader : MonoBehaviour
{

    public TextAsset levelJSON;

    [System.Serializable]
    public class Level
    {
        public string type;
        public int rows;
    }

    [System.Serializable]
    public class LevelList
    {
        public Level[] level;    
    }

    public LevelList MyLevels = new LevelList();


    private void Start()
    {
        StartCoroutine(LoadFiles());
    }

    IEnumerator LoadFiles()
    {
        MyLevels = JsonUtility.FromJson<LevelList>(levelJSON.text);
        GameManager.type = MyLevels.level[GameManager.levelcount].type;
        GameManager.rows = MyLevels.level[GameManager.levelcount].rows;


        yield return null;
    }
}
