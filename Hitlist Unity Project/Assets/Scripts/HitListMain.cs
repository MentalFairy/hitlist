﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skrptr;
using UnityEngine.UI;

public class HitListMain : Singleton<HitListMain>
{
    public Panel_EveryDayTasks panelEveryDayTasks;
    public Panel_ProjectSelection panelProjectSelection;
    public Panel_Cards panelCards;
    public Panel_ManageSoldier panelManageSoldier;
    public Panel_CustomerDelta panelCustomerDelta;

    public Text leadTimeDays,leadTimeHours;

    public string currentProject;
    public CardStage currentStage;
    public CardStatus currentCardStatusFilter = CardStatus.Accepted;
}
