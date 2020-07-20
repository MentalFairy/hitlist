using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_ClaimReward : SkrptrAction
{
    public override void Execute()
    {
        Card card = GetComponentInParent<Card>();
        card.stage = CardStage.Testing;

        if(card.milestone)
        {
            HitListMain.Instance.panelManageSoldier.balance += 150;
            for (int i = 0; i < 3; i++)
            {
                HitListMain.Instance.panelManageSoldier.BuyChicken();
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
