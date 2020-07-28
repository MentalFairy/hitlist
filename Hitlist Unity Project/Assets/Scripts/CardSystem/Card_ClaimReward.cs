using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_ClaimReward : SkrptrAction
{
    public override void Execute()
    {
        if (HitListMain.Instance.cardToClaimReward != null)
        {
            Card card = HitListMain.Instance.cardToClaimReward;
            card.stage = CardStage.Testing;

            if (card.milestone)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (HitListMain.Instance.panelManageSoldier.chickenCount <= HitListMain.Instance.panelManageSoldier.maxChicken)
                    {
                        HitListMain.Instance.panelManageSoldier.balance += 50;
                        HitListMain.Instance.panelManageSoldier.BuyChicken();
                    }
                }
            }
            else
            {
                HitListMain.Instance.panelManageSoldier.balance += 50;
            }
            HitListMain.Instance.panelManageSoldier.SaveSoldier();
            HitListMain.Instance.panelManageSoldier.UpdateBalances();
            card.GetComponent<SkrptrElement>().Lock();
            StartCoroutine(nameof(DeactiveGO), card);
            HitListMain.Instance.cardToClaimReward = null;
        }
    }
    public IEnumerator DeactiveGO(Card card)
    {
        yield return new WaitForSecondsRealtime(0.25f);   
        card.toDoItems.SetActive(false);
        card.testingItems.SetActive(true);
        card.gameObject.SetActive(false);
        HitListMain.Instance.panelCards.SaveCards();
    }
}
