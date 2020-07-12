using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CardStage { Backlog, ToDo, Testing, Complete,None };
public class Card : MonoBehaviour
{
    public string projectName;
    public CardStage stage = CardStage.Backlog;
    public InputField inputField;
    public DateTime creationDate,startDate;
    public int cardTargetHours = 0;
    public bool milestone = false;

    public GameObject backlogItems,toDoItems, testingItems,completeItems;


    public void Start()
    {
        inputField.onEndEdit.AddListener(delegate { OnInputFieldEndEdit(); });
    }
    public void OnInputFieldEndEdit()
    {
        HitListMain.Instance.panelCards.SaveCards();
    }
    public override string ToString()
    {
        return projectName + "|" + stage.ToString() + "|" + inputField.text +
                "|" + creationDate.ToString() + "|" + startDate.ToString() +
                "|" + cardTargetHours + "|" + milestone + "\n";
    }
}
