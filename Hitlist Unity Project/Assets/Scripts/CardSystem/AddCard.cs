using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCard : SkrptrAction
{
    public SkrptrRadioButton milestoneYes;
    public SkrptrRadioButton[] options;
    public int[] targetHours = { 4, 8, 48, 144, 288, 336 };
    public override void Execute()
    {
        if (HitListMain.Instance.currentProject != "")
        {
            bool milestone = false; ;
            if (milestoneYes.isChecked)
                milestone = true;


            int cardTargetHours = 0;
            for (int i = 0; i < options.Length; i++)
            {
                if (options[i].isChecked)
                {
                    cardTargetHours = targetHours[i];
                    break;
                }
            }
            Debug.Log("Adding card: " + milestone + " | " + cardTargetHours);
            HitListMain.Instance.panelCards.AddCard(cardTargetHours, milestone);
        }
    }
}
