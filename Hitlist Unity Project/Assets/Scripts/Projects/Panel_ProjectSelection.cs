﻿using Skrptr;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_ProjectSelection : MonoBehaviour
{
    public List<Project> projects;
    public GameObject projectListingPrefab;
    public Transform contentTransform;
    private void Awake()
    {
        projects = new List<Project>();
        HitListMain.Instance.panelProjectSelection = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        string savedData = SaveLoad.Load(SaveLoad.ProjectsListFileName);
        if (savedData != null)
        {
            string[] projectsData = savedData.Split('\n');

            for (int i = 0; i < projectsData.Length - 1; i++)
            {
                string[] projectData = projectsData[i].Split('|');
                Project project = Instantiate(projectListingPrefab, contentTransform).GetComponent<Project>();
                project.GetComponent<SkrptrTrigger>().triggerTargets[0].targetGO = this.gameObject;
                project.projectName = projectData[0];
                project.inputField.text = projectData[0];
                project.creationDate = DateTime.Parse(projectData[1]);
                project.leadTime = double.Parse(projectData[2]);
                project.lastLeadCounter = DateTime.Parse(projectData[3]);
                projects.Add(project);
                Debug.LogError("Project added: " + project.projectName);
            }
            if (projects.Count > 0)
            {
                projects[0].GetComponent<SkrptrElement>().Check();
                HitListMain.Instance.currentProject = projects[0].inputField.text;
            }
        }
    }
    public void DeleteProject()
    {
        if (HitListMain.Instance.projectToBeRemoved != null)
        {
            Project project = HitListMain.Instance.projectToBeRemoved;
            HitListMain.Instance.panelCards.DeleteCards(project.projectName);
            projects.Remove(project);
            Destroy(project.gameObject);
            StartCoroutine(nameof(ForceUpdateUI));
            SaveProjects();
            HitListMain.Instance.projectToBeRemoved = null;
        }
    }
    public void AddProject()
    {
        projects.Add(Instantiate(projectListingPrefab, contentTransform).GetComponent<Project>());
        projects[projects.Count - 1].inputField.text = "Project " + projects.Count;
        projects[projects.Count - 1].projectName = "Project " + projects.Count;
        projects[projects.Count - 1].GetComponent<SkrptrTrigger>().triggerTargets[0].targetGO = this.gameObject;
        SaveProjects();
        StartCoroutine(nameof(ForceUpdateUI));
    }
    public void SaveProjects()
    {
        string datatToSave = "";
        foreach (var project in projects)
        {
            datatToSave += project.ToString();
        }
        if (datatToSave != "")
        {
            SaveLoad.Save(datatToSave, SaveLoad.ProjectsListFileName);
        }
    }
    private IEnumerator ForceUpdateUI()
    {
        yield return null;
        GameObject go = new GameObject();
        go.transform.parent = contentTransform;
        yield return null;
        GameObject.Destroy(go);
    }
    private void OnApplicationQuit()
    {
        SaveProjects();
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveProjects();
        }
    }
}

