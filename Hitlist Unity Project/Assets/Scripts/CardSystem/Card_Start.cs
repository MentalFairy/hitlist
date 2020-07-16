using Skrptr;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Start : SkrptrAction
{
    public override void Execute()
    {
        Card card = GetComponentInParent<Card>();
        card.stage = CardStage.ToDo;
        card.startDate = DateTime.Now;
        card.GetComponent<SkrptrElement>().Lock();        
        StartCoroutine(nameof(DeactiveGO), card);        
    }
    public IEnumerator DeactiveGO(Card card)
    {
        yield return new WaitForSecondsRealtime(0.25f);
        card.Init();
        card.backlogItems.SetActive(false);
        card.toDoItems.SetActive(true);
        card.gameObject.SetActive(false);
        HitListMain.Instance.panelCards.SaveCards();
    }
}
