using Skrptr;
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

    public int customerTarget = 0;
    public int currentCustomers = 0;
    public SkrptrCheckbox holiday;

    public Image[] fillHpBars;
    public Text[] balanceTexts;
    public GameObject chickenIcon;
    public Transform chickenSlotsTransform;
    public List<GameObject> chickens;
    public GameObject[] levels;
    public Text levelLabel, currentCustomersLabel,customerCurrentAndTarget;
    public InputField targetCustomersInputField;

    public int characterLevel = 1;

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
            characterLevel = int.Parse(soldierData[5]);

            customerTarget = int.Parse(soldierData[6]);
            currentCustomers = int.Parse(soldierData[7]);
            bool holidayBool = bool.Parse(soldierData[8]);
            if (holidayBool)
                holiday.Check();

            InitCustomerData();
            for (int i = 0; i < chickenCount; i++)
            {
                chickens.Add(Instantiate(chickenIcon, chickenSlotsTransform));
            }
            InitLevels();
        }
        else
        {
            lastDrain = DateTime.Now;
        }
        UpdateBalances();
        InvokeRepeating(nameof(Drain), 0, 1);
    }
    public void InitCustomerData()
    {
        targetCustomersInputField.text = customerTarget.ToString();
        currentCustomersLabel.text = currentCustomers.ToString();
        customerCurrentAndTarget.text = currentCustomers.ToString() + "/" + customerTarget.ToString();
    }
    public void InitLevels()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(false);
        }
        levelLabel.text = characterLevel.ToString();
        levels[characterLevel-1].SetActive(true);
    }
    public void BuyChicken()
    {
        if (chickenCount < maxChicken && balance >= chickenPrice)
        {
            chickenCount++;
            chickens.Add(Instantiate(chickenIcon, chickenSlotsTransform));
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
        if (holiday.isChecked == false)
        {
            if (HP > 0)
            {
                double drain = (DateTime.Now - lastDrain).TotalSeconds * drainPerMinute / 60f;
                HP -= drain;
                //Debug.Log("Drained: " + drain);
                lastDrain = DateTime.Now;
                SaveSoldier();
                UpdateHPBars();
            }
            else
            {
                HP = 0;
                //die
            }
        }
    }
    public void SaveSoldier()
    {
        string dataToSave = "";
        dataToSave += HP + "|" + lastDrain.ToString() + "|" + balance.ToString() + "|" + chickenCount.ToString()
        + "|" + chickenRegen.ToString() + "|" + characterLevel + "|" + customerTarget.ToString() + "|" + currentCustomers.ToString()
        +"|" + holiday.isChecked.ToString();
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
