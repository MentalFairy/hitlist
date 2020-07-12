﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skrptr;
public class HitListMain : Singleton<HitListMain>
{
    public Panel_EveryDayTasks panelEveryDayTasks;
    public Panel_ProjectSelection panelProjectSelection;
    public Panel_Cards panelCards;

    public string currentProject;
    public CardStage currentStage;
}
