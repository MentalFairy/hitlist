using Skrptr;
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
                //load data

                Debug.Log(card.ToString());
                cards.Add(card);
            }
        }
    }
    public void DeleteCard(Card card)
    {
        cards.Remove(card);
        Destroy(card.gameObject);
        SaveCards();
    }
    public void AddCard(int cardTargetHours, bool milestone)
    {
        Card card = Instantiate(cardPrefab, contentTransform).GetComponent<Card>();
        card.projectName = HitListMain.Instance.currentProject;
        card.milestone = milestone;
        card.stage = CardStage.Backlog;
        card.inputField.text = "Edit me by longpress";
        card.creationDate = DateTime.Now;
        card.cardTargetHours = cardTargetHours;
        cards.Add(card);        
        SaveCards();
    }
    public void SaveCards()
    {
        string dataToSave = "";
        for (int i = 0; i < cards.Count; i++)
        {
            Debug.Log("i: " + i);
            dataToSave += cards[i].ToString();
            Debug.Log(dataToSave);
        }
        SaveLoad.Save(dataToSave, SaveLoad.CardsFileName);
    }


    public void FilterCards(CardStage filter)
    {
        StartCoroutine(nameof(CloseCards),filter);
    }
    public IEnumerator CloseCards(CardStage filter)
    {
        foreach (var card in cards)
        {
            card.GetComponent<SkrptrElement>().Lock();
            yield return new WaitForSecondsRealtime(delayBetweenCards);
        }
        StartCoroutine(nameof(OpenCards), filter);
    }
    public IEnumerator OpenCards(CardStage filter)
    {
        foreach (var card in cards)
        {
            if (card.stage == filter)
            {
                card.GetComponent<SkrptrElement>().Unlock();
                yield return new WaitForSecondsRealtime(delayBetweenCards);
            }
        }
    }
}
