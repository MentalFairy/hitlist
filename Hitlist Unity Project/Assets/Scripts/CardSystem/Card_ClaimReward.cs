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
