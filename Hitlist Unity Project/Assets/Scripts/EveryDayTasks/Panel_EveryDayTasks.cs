using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_EveryDayTasks : MonoBehaviour
{
    public GameObject taskPrefab;
    public Transform contentTransform;
    private void Awake()
    {
        HitListMain.Instance.panelEveryDayTasks = this;
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
                string[] taskData = tasksData[i].Split('-');
                EveryDayTask everyDayTask = Instantiate(taskPrefab, contentTransform).GetComponent<EveryDayTask>();
                everyDayTask.inputField.text = taskData[0];
                if (taskData[1].ToLower() == "true")
                    everyDayTask.Check();
            }            
        }
    }

    public void AddTask()
    {
        GameObject go = Instantiate(taskPrefab, contentTransform);
    }
    public void SaveTasks()
    {
        EveryDayTask[] everyDayTasks = contentTransform.GetComponentsInChildren<EveryDayTask>();
        string datatToSave = "";
        foreach (var everyDayTask in everyDayTasks)
        {
            datatToSave += everyDayTask.inputField.text + "-" + everyDayTask.isChecked.ToString() + "\n";
        }
        SaveLoad.Save(datatToSave, SaveLoad.EveryDayTasksFileName);
    }
}
