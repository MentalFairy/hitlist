using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CardStage { Backlog, ToDo, Testing, Complete };
public class Card : MonoBehaviour
{
    public CardStage cardStage = CardStage.Backlog;

    public GameObject backlogItems,toDoItems, testingItems,completeItems;


    public void Start()
    {
        
    }
}
