using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EveryDayTask : MonoBehaviour
{
    public InputField inputField;
    public GameObject greenCheckmark;
    public GameObject redCheckMark;
    public bool isChecked = false;

    private void Start()
    {
        inputField.onEndEdit.AddListener(delegate { OnEndEdit(); }) ;
    }

    private void OnEndEdit()
    {
        HitListMain.Instance.panelEveryDayTasks.SaveTasks();
    }

    public void Check()
    {
        if(isChecked)
        {
            redCheckMark.SetActive(true);
            greenCheckmark.SetActive(false);
        }
        else
        {
            greenCheckmark.SetActive(true);
            redCheckMark.SetActive(false);
        }
        isChecked = !isChecked;
    }
}
