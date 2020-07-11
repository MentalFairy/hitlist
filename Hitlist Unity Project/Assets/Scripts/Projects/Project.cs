using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Project : MonoBehaviour
{
    public string projectName;
    public DateTime creationDate;
    public int leadTime = 0;

    public InputField inputField;
    private void Start()
    {
        inputField.onEndEdit.AddListener(delegate { OnEndEdit(); });
    }
    public void OnEndEdit()
    {
        projectName = inputField.text;
        HitListMain.Instance.panelProjectSelection.SaveProjects();
    }
}
