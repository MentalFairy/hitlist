using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CardStage { Backlog, ToDo, Testing, Complete,None };
public enum CardStatus { Accepted, Declined, Neither };
public class Card : MonoBehaviour
{
    public string projectName;
    public CardStage stage = CardStage.Backlog;
    public InputField inputField;
    public DateTime creationDate,startDate;
    public int cardTargetHours = 0;
    public bool milestone = false;
    public GameObject milestoneIcon;
    public Text start, deadline, reward;
    public CardStatus cardStatus = CardStatus.Neither;

    public GameObject backlogItems,toDoItems, testingItems,completeItems;

    public void Init()
    {
        if (milestone)
            milestoneIcon.SetActive(true);
        if (startDate != DateTime.MinValue)
        {
            start.text = "START: " + startDate.Day.ToString() + "-" + startDate.Month.ToString("d2");          
           
        }
        string targetIndicator = "";
        if (cardTargetHours > 12)
        {
            targetIndicator = (cardTargetHours / 24).ToString();
        }
        else
        {
            targetIndicator = cardTargetHours.ToString() + "h";
        }
        if(startDate!=DateTime.MinValue)
        {
            deadline.text = "Target: " + startDate.AddHours(cardTargetHours).Day.ToString("d2") + ":" +
               startDate.AddHours(cardTargetHours).Month.ToString("d2") + "(" + targetIndicator + ")";
        }
        else
        {
            deadline.text = "Target: 00-00 (" + targetIndicator + ")";
        }
    }
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
                "|" + cardTargetHours + "|" + milestone + "|"+ cardStatus.ToString() + "\n" ;
    }
}
