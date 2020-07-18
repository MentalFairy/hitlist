using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveLoad
{
    public static string EveryDayTasksFileName = "EveryDayTasks";
    public static string ProjectsListFileName = "ProjectsList";
    public static string CardsFileName = "Cards";
    public static string SoldierFileName = "Soldier";

    public static void Save(string message, string fileName)
    {
        string filePath = Application.persistentDataPath + "\\" + fileName + ".txt";
        FileInfo f = new FileInfo(filePath);
        StreamWriter w;
        if (!f.Exists)
        {
            w = f.CreateText();
        }
        else
        {
            f.Delete();
            w = f.CreateText();
        }
        w.Write(message);
        w.Close();
        //Debug.Log("File saved: " + filePath);

    }
    public static string Load(string fileName)
    {
        string filePath = Application.persistentDataPath + "\\" + fileName + ".txt";
        if (File.Exists(filePath))
        {
            StreamReader r = File.OpenText(filePath);
            string text = r.ReadToEnd();
            r.Close();
            Debug.Log("File loaded: " + filePath);
            return text;
        }
        else
        {
            return null;
        }
    }
}
