using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Panel_MetricsRisk : MonoBehaviour
{
    public Dropdown yearDD;
    public int[] targetHours = new int[] {0, 4, 8,48, 96, 144,192,240,288,336};
    public int[] storiesCategorizedCounter;
    public Text[] storiesCounterLabels,percentCounterLabels, riskCounterLabels,monthLabels;
    public Image[] riskImages;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            CountCards();
        }
    }

    private void CountCards()
    {
        storiesCategorizedCounter = new int[targetHours.Length];
        int cardsCount = HitListMain.Instance.panelCards.cards.Count;
        foreach (var item in HitListMain.Instance.panelCards.cards)
        {
            int hours =(int)(item.leadTime / 3600f);
            Debug.Log(hours);
            for (int i = 0; i < targetHours.Length-1; i++)
            {
                if (hours >= targetHours[i] && hours < targetHours[i+1])
                {
                    storiesCategorizedCounter[i]++;
                    break;
                }
                if(hours>targetHours[targetHours.Length-1])
                {
                    storiesCategorizedCounter[storiesCategorizedCounter.Length - 1]++;
                    break;
                }
            }
        }
        for (int i = 0; i < storiesCounterLabels.Length; i++)
        {        
            storiesCounterLabels[i].text = storiesCategorizedCounter[i].ToString();
        }
        for (int i = 0; i < percentCounterLabels.Length; i++)
        {
            percentCounterLabels[i].text = ((float)(storiesCategorizedCounter[i]) / (float)(cardsCount)*100f).ToString("0.00");
        }
        int percentCount = 0;
        int cardsRiskCounter = 0;
        for (int i = 0; i < riskCounterLabels.Length; i++)
        {
            percentCount += (100 * storiesCategorizedCounter[i] / cardsCount);
            cardsRiskCounter += storiesCategorizedCounter[i];
            if (cardsRiskCounter != cardsCount)
            {
                riskCounterLabels[i].text = percentCount.ToString() + "%";
            }
            else
            {
                riskCounterLabels[i].text = "100%";
            }
            if (percentCount < 50)
                riskImages[i].color = Color.red;
            if (percentCount >= 50)
                riskImages[i].color = Color.yellow;
            if (percentCount >= 80)
                riskImages[i].color = Color.green;
        }
        List<Card> yearCards = HitListMain.Instance.panelCards.cards.Where(c => c.creationDate.Year == yearDD.value + 2020).ToList<Card>();
        for (int i = 0; i < monthLabels.Length; i++)
        {
            List<Card> monthCards = yearCards.Where(c => c.creationDate.Month == i + 1).ToList<Card>();
            monthLabels[i].text = monthLabels[i].text = ((float)monthCards.Count / 4f).ToString("0.0");
        }
    }
}
