using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Project : MonoBehaviour
{
    public string projectName;
    public DateTime creationDate,lastLeadCounter;
    public double leadTime = 0;

    public InputField inputField;

    public List<Card> cardsInProject, uncompletedCards;

    private void Start()
    {
        inputField.onEndEdit.AddListener(delegate { OnEndEdit(); });
        InvokeRepeating(nameof(CountLeadtime), 0, 1);
    }
    public void OnEndEdit()
    {
        projectName = inputField.text;
        HitListMain.Instance.panelProjectSelection.SaveProjects();
    }
    public void CountLeadtime()
    {
        cardsInProject = HitListMain.Instance.panelCards.cards.Where(c => c.projectName == projectName).ToList<Card>();
        uncompletedCards = cardsInProject.Where(c => c.stage != CardStage.Complete && c.stage != CardStage.Cleared).ToList<Card>();
        if (uncompletedCards.Count > 0)
        {
            leadTime += (DateTime.Now - lastLeadCounter).TotalSeconds;           
        }
        lastLeadCounter = DateTime.Now;
        if(HitListMain.Instance.currentProject == projectName)
        {
            int days = (int)(leadTime / 60f / 60f / 24f);
            HitListMain.Instance.leadTimeDays.text = "DAYS: " + days.ToString();
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
                minutesString = seconds.ToString();
            if (hours < 10)
                hoursString = "0" + hours.ToString();
            else
                hoursString = hours.ToString();
            HitListMain.Instance.leadTimeHours.text = "HOURS: " + hoursString + ":" + minutesString + ":" + secondsString; 
        }
    }

    public override string ToString()
    {
        return projectName + "|" + creationDate.ToString() + "|" + leadTime.ToString() + "|" + lastLeadCounter.ToString() + "\n";
    }
}
