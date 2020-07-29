using System.Collections;
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
    public Panel_ClaimRewardSafety panelClaimRewardSafety;
    public Panel_RemoveProjectSafety panelRemoveProjectSafety;
    public Panel_RemoveCardSafety panelRemoveCardSafety;

    public Panel_MetricsYear panelMetricsYear;
    public Panel_MetricsMonthly panelMetricsMonth;
    public Panel_MetricsRisk panelMetricsDay;

    public Card cardToClaimReward;
    public Project projectToBeRemoved;
    public Card cardToBeDeleted;

    public Text leadTimeDays,leadTimeHours;

    public bool hierarchyChanged = false;
    public string currentProject;
    public CardStage currentStage;
    public CardStatus currentCardStatusFilter = CardStatus.Accepted;


}
