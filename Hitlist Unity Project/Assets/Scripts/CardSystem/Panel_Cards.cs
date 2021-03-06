﻿using Skrptr;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_Cards : MonoBehaviour
{
    public List<Card> cards;
    public GameObject cardPrefab;
    public Transform contentTransform;

    public float delayBetweenCards = 0.05f;
    private void Awake()
    {
        cards = new List<Card>();
        HitListMain.Instance.panelCards = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        string savedData = SaveLoad.Load(SaveLoad.CardsFileName);
        if (savedData != null)
        {
            string[] cardsData = savedData.Split('\n');

            for (int i = 0; i < cardsData.Length - 1; i++)
            {
                string[] cardData = cardsData[i].Split('|');
                Card card = Instantiate(cardPrefab, contentTransform).GetComponent<Card>();
                card.projectName = cardData[0];
                card.stage = (CardStage)Enum.Parse(typeof(CardStage), cardData[1]);
                card.inputField.text = cardData[2];
                card.creationDate = DateTime.Parse(cardData[3]);
                card.startDate = DateTime.Parse(cardData[4]);
                card.cardTargetHours = int.Parse(cardData[5]);
                card.milestone = bool.Parse(cardData[6]);
                card.cardStatus = (CardStatus)Enum.Parse(typeof(CardStatus), cardData[7]);
                card.leadTime = double.Parse(cardData[8]);
                card.lastLeadCounter = DateTime.Parse(cardData[9]);

                // dependencies
                card.Init();
                //load data

                switch (card.stage)
                {
                    case CardStage.Backlog:
                        card.backlogItems.SetActive(true);
                        break;
                    case CardStage.ToDo:
                        card.toDoItems.SetActive(true);
                        break;
                    case CardStage.Testing:
                        card.testingItems.SetActive(true);
                        break;
                    case CardStage.Complete:
                        card.completeItems.SetActive(true);
                        break;
                    case CardStage.None:
                        break;
                    default:
                        break;
                }

                //Debug.Log(card.ToString());
                cards.Add(card);
            }
        }
    }
    public void DeleteCards(string projectName)
    {
        List<Card> cardsToBeRemoved = new List<Card>();
        foreach (var card in cards)
        {
            if(card.projectName == projectName)
            {
                cardsToBeRemoved.Add(card);
            }
        }
        foreach (var card in cardsToBeRemoved)
        {
            cards.Remove(card);
            Destroy(card.gameObject);
        }
    }
    public void DeleteCard()
    {
        if (HitListMain.Instance.cardToBeDeleted != null)
        {
            Card card = HitListMain.Instance.cardToBeDeleted;
            cards.Remove(card);
            Destroy(card.gameObject);
        }
        
        SaveCards();
    }
    public void ClearCompletedCards()
    {
        StartCoroutine(nameof(LockAndDisableActiveCards));
    }
    public IEnumerator LockAndDisableActiveCards()
    {
        Card[] activeCards = contentTransform.GetComponentsInChildren<Card>();
        foreach (var card in activeCards)
        {
            card.stage = CardStage.Cleared;
            card.GetComponent<SkrptrElement>().Lock();
        }
        yield return new WaitForSecondsRealtime(0.25f);
        foreach (var card in activeCards)
        {
            card.gameObject.SetActive(false);
        }
        SaveCards();
    }
    public void AddCard(int cardTargetHours, bool milestone)
    {
        Card card = Instantiate(cardPrefab, contentTransform).GetComponent<Card>();
        if (HitListMain.Instance.currentStage != CardStage.Backlog)
            card.gameObject.SetActive(false);

        card.projectName = HitListMain.Instance.currentProject;
        card.milestone = milestone;
        card.stage = CardStage.Backlog;
        card.creationDate = DateTime.Now;
        card.cardTargetHours = cardTargetHours;
        card.backlogItems.SetActive(true);
        card.Init();
        cards.Add(card);        
        SaveCards();
    }
    public void SaveCards()
    {
        string dataToSave = "";
        if (HitListMain.Instance.hierarchyChanged)
        {
            cards.Clear();
            for (int i = 0; i < contentTransform.childCount; i++)
            {
                if (contentTransform.GetChild(i).GetComponent<Card>() != null)
                    cards.Add(contentTransform.GetChild(i).GetComponent<Card>());
            }
            HitListMain.Instance.hierarchyChanged = false;
        }
        for (int i = 0; i < cards.Count; i++)
        {
            dataToSave += cards[i].ToString();
        }
        if (dataToSave != "")
        {
            SaveLoad.Save(dataToSave, SaveLoad.CardsFileName);
        }
    }

    public void FilterCards()
    {
        StartCoroutine(nameof(CloseCards));
    }
    public IEnumerator CloseCards()
    {
        Card[] activeCards = contentTransform.GetComponentsInChildren<Card>();
        foreach (var card in activeCards)        {
            
            card.GetComponent<SkrptrElement>().Lock();
            yield return new WaitForSecondsRealtime(delayBetweenCards);
        }
        yield return new WaitForSecondsRealtime(0.25f);
        foreach (var card in activeCards)
        {
            card.gameObject.SetActive(false);
        }
        StartCoroutine(nameof(OpenCards));
    }
    public IEnumerator OpenCards()
    {
        foreach (var card in cards)
        {
            if (card.stage == HitListMain.Instance.currentStage && HitListMain.Instance.currentProject == card.projectName)
            {
                if (HitListMain.Instance.currentStage != CardStage.Complete)
                {
                    card.gameObject.SetActive(true);
                    card.GetComponent<SkrptrElement>().Unlock();
                    yield return new WaitForSecondsRealtime(delayBetweenCards);
                }
                else
                {
                    if(card.cardStatus == HitListMain.Instance.currentCardStatusFilter)
                    {
                        card.gameObject.SetActive(true);
                        card.GetComponent<SkrptrElement>().Unlock();
                        yield return new WaitForSecondsRealtime(delayBetweenCards);
                    }
                }
            }
        }
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
            SaveCards();
    }
    private void OnApplicationQuit()
    {
        SaveCards();
    }
}
