using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using MiniJSON;
using System.Text;

public static class SaveManager
{
    public static string directory = "/Score/";
    public static string fileName = "ScoreData.txt";


    public static void Save(Score _saveObject)
    {
        string dir = Application.persistentDataPath + directory;

        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        string _previousScore = "";

        // Checks and reads if previous levels score is already saved
        if (File.Exists(dir + fileName))
        {
            var fileStream = new FileStream(dir + fileName, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                _previousScore = streamReader.ReadToEnd();
            }
        }
      

        string _shapesCollected = MiniJSON.Json.Serialize(_saveObject.shapeScoreDic);
        string _totalScore = MiniJSON.Json.Serialize(_saveObject.totalScore);
        string _levelName = MiniJSON.Json.Serialize(GameManager.Instance.currentLevelName);

        string json = "'"+ _previousScore + "'\n " +
                      "Level : '"+ _levelName + "'\n" +
                      "Total Score : '" + _totalScore + "'\n " +
                      "Shapes Collected : '" + _shapesCollected + "'\n " + "";

        File.WriteAllText(dir + fileName, json);

    }
}
