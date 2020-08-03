using Skrptr;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_ManageSoldier : MonoBehaviour
{
    public double HP = 50;
    public double drainPerMinute = 0.005f;
    public int balance = 0;
    public int chickenCount =0;
    public double chickenRegen = 50;
    public int maxChicken = 12;
    public int chickenPrice = 50;

    public int customerTarget = 0;
    public int currentCustomers = 0;
    public SkrptrCheckbox holiday;

    public Image[] fillHpBars;
    public Text[] balanceTexts,HPtexts;
    public GameObject chickenIcon;
    public Transform chickenSlotsTransform;
    public List<GameObject> chickens;
    public GameObject[] levels;
    public Text levelLabel, currentCustomersLabel,customerCurrentAndTarget,characterVoiceLabel;
    public InputField targetCustomersInputField;

    public int characterLevel = 1;
    bool holidayBool;
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
            InitLevelText();

            customerTarget = int.Parse(soldierData[6]);
            currentCustomers = int.Parse(soldierData[7]);
            holidayBool = bool.Parse(soldierData[8]);
            StartCoroutine(nameof(InitHoliday));
            InitCustomerData();
            for (int i = 0; i < chickenCount && i<maxChicken; i++)
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
    public void InitLevelText()
    {
        if (characterLevel >= 1 && characterLevel < 4)
        {
            characterVoiceLabel.text = "I AM DYING! can’t hold on much longer.. chicken..";
        }
        else if (characterLevel >= 4 && characterLevel < 7)
        {
            characterVoiceLabel.text = "HELP ME i feel hungry, i have been walking for hours..";
        }
        else if (characterLevel >= 7 && characterLevel < 11)
        {
            characterVoiceLabel.text = "I FEEL AMAZING! WHAT’S IN THIS CHICKEN?";
        }
    }
    public IEnumerator InitHoliday()
    {
        yield return new WaitForSeconds(.6f);
        if (holidayBool)
            holiday.Check();
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
        if (balance >= chickenPrice)
        {
            chickenCount++;
            balance -= chickenPrice;
            if(chickenCount <= maxChicken)
                chickens.Add(Instantiate(chickenIcon, chickenSlotsTransform));
            UpdateBalances();
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
            if (HP > 50 * characterLevel && characterLevel < 10)
                LevelUp();

            if (HP > 50 * characterLevel)
                HP = 50 * characterLevel;

            if (chickenCount <= maxChicken)
            {
                GameObject chicken = chickens[0];
                chickens.RemoveAt(0);
                GameObject.Destroy(chicken);
            }
            chickenCount--;           
            UpdateHPBars();
        }
    }
    public void LevelUp()
    {
        if (characterLevel < 10)
        {
            characterLevel++;
            InitLevels();
        }
        InitLevelText();
    }
    public void Die()
    {

    }
    public void Drain()
    {
        if (holiday.isChecked == false)
        {
            if (HP > 0)
            {
                double drain = (DateTime.Now - lastDrain).TotalSeconds * drainPerMinute / 60f * characterLevel;
                HP -= drain;
                //Debug.Log("Drained: " + drain);              
                UpdateHPBars();
            }
            else if(HP < 0)
            {
                HP = 0;
                Die();
            }
        }
        lastDrain = DateTime.Now;
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
            item.fillAmount = 1 - (float)(HP / (50f * characterLevel));
        }
        foreach (var item in HPtexts)
        {
            item.text = ((int)HP).ToString();
        }
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
            SaveSoldier();
    }
    private void OnApplicationQuit()
    {
        SaveSoldier();
    }
}
