using Skrptr;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CardStage { Backlog, ToDo, Testing, Complete,Cleared,None };
public enum CardStatus { Accepted, Declined, Neither };
public class Card : MonoBehaviour
{
    public string projectName;
    public double leadTime = 0;
    public CardStage stage = CardStage.Backlog;
    public InputField inputField;
    public DateTime creationDate,startDate,lastLeadCounter;
    public int cardTargetHours = 0;
    public bool milestone = false;
    public GameObject milestoneIcon;
    public Text startLabel, deadlineLabel, rewardLabel,leadLabel;
    public CardStatus cardStatus = CardStatus.Neither;

    public GameObject backlogItems,toDoItems, testingItems,completeItems;
    public GameObject[] glowStatuses;
    public int[] targetHours = {0, 4, 8, 48, 144, 288, 336 };
    public Image glowFill;
    public GameObject cycleStatusItems, leadTimeItems;
    public GameObject greenRemove, redRemove;
    public GameObject editIcon;
    public Sprite redGlow, blueGlow;

    public void Init()
    {
        if (milestone)
            milestoneIcon.SetActive(true);
        if (startDate != DateTime.MinValue)
        {
            startLabel.text = "START: " + startDate.Day.ToString() + "-" + startDate.Month.ToString("d2");          
           
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
            deadlineLabel.text = "Target: " + startDate.AddHours(cardTargetHours).Day.ToString("d2") + "-" +
               startDate.AddHours(cardTargetHours).Month.ToString("d2") + "(" + targetIndicator + ")";
        }
        else
        {
            deadlineLabel.text = "Target: 00-00 (" + targetIndicator + ")";
        }
        if (stage == CardStage.Backlog)
        {
            cycleStatusItems.SetActive(false);
            leadTimeItems.SetActive(false);
        }
        else
        {
            cycleStatusItems.SetActive(true);
            leadTimeItems.SetActive(true);
        }
        if(cardStatus == CardStatus.Accepted)
        {
            greenRemove.SetActive(true);
            redRemove.SetActive(false);
        }
        else if(cardStatus == CardStatus.Declined)
        {
            redRemove.SetActive(true);
            greenRemove.SetActive(false);
        }
    }
    public void Start()
    {
        inputField.onEndEdit.AddListener(delegate { OnInputFieldEndEdit(); });
        InvokeRepeating(nameof(CountLeadtime), 0, 1);
    }
    private void Update()
    {
        if (inputField.text != "")
            editIcon.SetActive(false);
        else
            editIcon.SetActive(true);
    }
    public void CountLeadtime()
    {
        if (stage == CardStage.ToDo)
        {
            leadTime += (DateTime.Now - lastLeadCounter).TotalSeconds;
        }
        lastLeadCounter = DateTime.Now;
        if (gameObject.activeSelf)
        {
            int days = (int)(leadTime / 60f / 60f / 24f);
            int hours = (int)leadTime / 60 / 60 % 24;
            int minutes = (int)leadTime / 60 % 60;
            int seconds = (int)leadTime % 60;

            string secondsString, minutesString, hoursString;
            if (seconds < 10)
                secondsString = "0" + seconds.ToString();
            else
                secondsString = seconds.ToString();
            if (minutes < 10)
                minutesString = "0" + minutes.ToString();
            else
                minutesString = minutes.ToString();
            if (hours < 10)
                hoursString = "0" + hours.ToString();
            else
                hoursString = hours.ToString();
            leadLabel.text = "DAYS: " + days.ToString() + " HOURS: " + hoursString + ":" + minutesString + ":" + secondsString;

            TimeSpan currentTimeSpan = startDate.AddSeconds(leadTime) - startDate;
            TimeSpan totalTimeSpan = startDate.AddHours(cardTargetHours) - startDate;
           
            glowFill.fillAmount = (float)(currentTimeSpan.TotalSeconds / totalTimeSpan.TotalSeconds);
            for (int i = 0; i < glowStatuses.Length; i++)
            {
                if (currentTimeSpan.TotalHours > targetHours[i] && currentTimeSpan.TotalHours < targetHours[i+1])
                {
                    glowStatuses[i].GetComponent<Image>().enabled = true;
                }
                else
                {
                    glowStatuses[i].GetComponent<Image>().enabled = false;
                }
            }
            if(glowFill.fillAmount>=1)
            {
                glowFill.sprite = redGlow;
            }
            else
            {
                glowFill.sprite = blueGlow;
            }
        }

    }
    public void OnInputFieldEndEdit()
    {
        HitListMain.Instance.panelCards.SaveCards();
    }
    public override string ToString()
    {
        return projectName + "|" + stage.ToString() + "|" + inputField.text +
                "|" + creationDate.ToString() + "|" + startDate.ToString() +
                "|" + cardTargetHours + "|" + milestone + "|"+ cardStatus.ToString() +
                "|" + leadTime.ToString() + "|" + lastLeadCounter.ToString() + "\n" ;
    }
    public void UpPress()
    {
        StartCoroutine(nameof(UpPressedDelayed));
    }
    public IEnumerator UpPressedDelayed()
    {
        int nextActiveKidIndex = 0;
        for (int i = transform.GetSiblingIndex() - 1; i > 0; i--)
        {
            if (transform.parent.GetChild(i).gameObject.activeSelf)
            {
                nextActiveKidIndex = i;
                break;
            }
        }
        GetComponent<SkrptrElement>().Lock();
        SkrptrElement nextActiveSkrptr = transform.parent.GetChild(nextActiveKidIndex).GetComponent<SkrptrElement>();
        nextActiveSkrptr.Lock();
        yield return new WaitForSecondsRealtime(0.25f);
        transform.SetSiblingIndex(nextActiveKidIndex);
        nextActiveSkrptr.Unlock();
        GetComponent<SkrptrElement>().Unlock();
        HitListMain.Instance.hierarchyChanged = true;
    }
    public void DownPress()
    {
        StartCoroutine(nameof(DownPressDelayed));
    }
    public IEnumerator DownPressDelayed()
    {
        int nextActiveKidIndex = -1;
        for (int i = transform.GetSiblingIndex() + 1; i < transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i).gameObject.activeSelf)
            {
                nextActiveKidIndex = i;
                Debug.Log("Found: " + i);
                break;
            }
        }

        GetComponent<SkrptrElement>().Lock();
        SkrptrElement nextActiveSkrptr = transform.parent.GetChild(nextActiveKidIndex).GetComponent<SkrptrElement>();
        nextActiveSkrptr.Lock();
        yield return new WaitForSecondsRealtime(0.25f);
        transform.SetSiblingIndex(nextActiveKidIndex);
        nextActiveSkrptr.Unlock();
        GetComponent<SkrptrElement>().Unlock();

        transform.SetSiblingIndex(nextActiveKidIndex);
        HitListMain.Instance.hierarchyChanged = true;
    }
}
