using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_EveryDayTasks : MonoBehaviour
{
    public GameObject taskPrefab;
    public Transform contentTransform;
    public DateTime lastReset;
    public List<EveryDayTask> tasks;
    private void Awake()
    {
        HitListMain.Instance.panelEveryDayTasks = this;
        tasks = new List<EveryDayTask>();
    }
    // Start is called before the first frame update
    void Start()
    {
        string savedData = SaveLoad.Load(SaveLoad.EveryDayTasksFileName);
        if(savedData!= null)
        {
            string[] tasksData = savedData.Split('\n');

            for (int i = 0; i < tasksData.Length-1; i++)
            {
                if (i == 0)
                {
                    lastReset = DateTime.Parse(tasksData[i]);
                }
                else
                {
                    string[] taskData = tasksData[i].Split('-');
                    EveryDayTask everyDayTask = Instantiate(taskPrefab, contentTransform).GetComponent<EveryDayTask>();
                    everyDayTask.inputField.text = taskData[0];
                    if (taskData[1].ToLower() == "true")
                        everyDayTask.Check();
                    tasks.Add(everyDayTask);
                }
            }            
        }
        InvokeRepeating(nameof(ResetTasks), 0, 60);
    }
    public void ResetTasks()
    {
        if (lastReset.DayOfYear != DateTime.Now.DayOfYear)
        {
            lastReset = DateTime.Now;
            foreach (var task in tasks)
            {
                if (task.isChecked)
                    task.Check();
            }
            SaveTasks();
            Debug.LogError("Resetted Tasks");
        }

    }

    public void AddTask()
    {
        GameObject go = Instantiate(taskPrefab, contentTransform);
    }
    public void SaveTasks()
    {
        EveryDayTask[] everyDayTasks = contentTransform.GetComponentsInChildren<EveryDayTask>();
        string datatToSave = lastReset.ToString() + "\n";
        foreach (var everyDayTask in everyDayTasks)
        {
            datatToSave += everyDayTask.inputField.text + "-" + everyDayTask.isChecked.ToString() + "\n";
        }
        SaveLoad.Save(datatToSave, SaveLoad.EveryDayTasksFileName);
    }
}
