using Skrptr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_AcceptReject : SkrptrAction
{
    public CardStatus setCardStatusTo;
    public override void Execute()
    {
        Card card = GetComponentInParent<Card>();
        card.cardStatus = setCardStatusTo;
        card.stage = CardStage.Complete;
        card.GetComponent<SkrptrElement>().Lock();
        StartCoroutine(nameof(DeactiveGO), card);
    }
    public IEnumerator DeactiveGO(Card card)
    {
        yield return new WaitForSecondsRealtime(0.25f);
        card.Init();
        card.testingItems.SetActive(false);
        card.completeItems.SetActive(true);
        card.gameObject.SetActive(false);
        HitListMain.Instance.panelCards.SaveCards();
    }
}
