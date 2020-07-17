using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_ManageSoldier : MonoBehaviour
{
    public double HP = 100;
    public double drainPerMinute = 0.002f;
    public int balance = 0;
    public int chickenCount =0;
    public double chickenRegen = 50;
    public int maxChicken = 12;
    public int chickenPrice = 50;

    public Image[] fillHpBars;
    public Text[] balanceTexts;
    public GameObject chickenIcon;
    public List<GameObject> chickens;
    public Transform chickenSlotsTransform;


    public DateTime lastDrain;
    private void Awake()
    {
        HitListMain.Instance.panelManageSoldier = this;
    }
    private void Start()
    {
        chickens = new List<GameObject>();
        string savedData = SaveLoad.Load(SaveLoad.SoldierFileName);
        if (savedData != null)
        {
            string[] soldierData = savedData.Split('|');
            HP = float.Parse(soldierData[0]);
            lastDrain = DateTime.Parse(soldierData[1]);

            balance = int.Parse(soldierData[2]);
            chickenCount = int.Parse(soldierData[3]);
            chickenRegen = double.Parse(soldierData[4]);
        }
        else
        {
            lastDrain = DateTime.Now;
        }
        UpdateBalances();
        InvokeRepeating(nameof(Drain), 0, 1);
    }
    public void BuyChicken()
    {
        if (chickenCount < maxChicken)
        {
            chickenCount++;
            Instantiate(chickenIcon, chickenSlotsTransform);
            balance -= chickenPrice;
            UpdateBalances();
            SaveSoldier();
        }
    }
    public void UpdateBalances()
    {
        foreach (var balanceText in balanceTexts)
        {
            balanceText.text = "$" + balance.ToString();
        }
    }
    public void ConsumeChicken()
    {
        if (chickenCount > 0)
        {
            HP += chickenRegen;
            if (HP > 100)
                HP = 100;
            chickenCount--;
            GameObject chicken = chickens[0];
            chickens.RemoveAt(0);
            GameObject.Destroy(chicken);
            SaveSoldier();
        }
    }
    public void Drain()
    {
        double drain = (DateTime.Now - lastDrain).TotalSeconds * drainPerMinute / 60f;
        HP -= drain;
        Debug.Log("Drained: " + drain);
        lastDrain = DateTime.Now;
        SaveSoldier();
        UpdateHPBars();
    }
    public void SaveSoldier()
    {
        string dataToSave = "";
        dataToSave += HP + "|" + lastDrain.ToString() + "|" + balance.ToString() + "|" + chickenCount.ToString()
        + "|" + chickenRegen.ToString();
        SaveLoad.Save(dataToSave, SaveLoad.SoldierFileName);
    }
    public void UpdateHPBars()
    {
        foreach (var item in fillHpBars)
        {
            item.fillAmount = 1 - (float)(HP / 100f);
        }
    }
}
