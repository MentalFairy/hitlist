using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Progress : MonoBehaviour
{
    public Image progressFill;
    public Text progressPercentage;
    public List<Card> currentProjectCards;
    public float percent = -1;
    private void Update()
    {
        currentProjectCards = HitListMain.Instance.panelCards.cards.Where(card => card.projectName == HitListMain.Instance.currentProject && card.stage != CardStage.Cleared).ToList();
        if (currentProjectCards != null && currentProjectCards.Count > 0)
        {
            percent = (float)(currentProjectCards.Where(card => card.stage == CardStage.Complete).ToArray().Length) / (float)currentProjectCards.Count;
            progressPercentage.text = ((int)(percent * 100f)).ToString() + "%";
            progressFill.fillAmount = percent;
        }
        else
        {
            progressFill.fillAmount = 0;
            progressPercentage.text = "0%";
        }
    }
}
