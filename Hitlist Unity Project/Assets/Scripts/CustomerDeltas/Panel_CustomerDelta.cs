using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Panel_CustomerDelta : MonoBehaviour
{
    public Dictionary<DateTime, int> customerDeltas;
    public InputField valueInputField;
    public Sprite buttonTextWin, buttonTextLose, headerWin, headerLose;
    public Image buttonText, headerText;
    private void Awake()
    {
        HitListMain.Instance.panelCustomerDelta = this;
    }
    private void Start()
    {
        valueInputField.text = "1";
        CheckValue();

        customerDeltas = new Dictionary<DateTime, int>();
        string savedData = SaveLoad.Load(SaveLoad.CustomerDeltaFileName);
        if (savedData != null)
        {
            string[] deltas = savedData.Split('\n');
            for (int i = 0; i < deltas.Length - 1; i++)
            {
                string[] delta = deltas[i].Split('|');
                DateTime dateTime = DateTime.Parse(delta[0]);
                int value = int.Parse(delta[1]);
                customerDeltas.Add(dateTime, value);
                Debug.LogError("Added delta: " + dateTime.ToString() + " " + value);
            }
        }
    }
    public void CountCustomers()
    {
        int aux = 0;
        foreach (var item in customerDeltas)
        {
            aux += item.Value;
        }
        HitListMain.Instance.panelManageSoldier.currentCustomers = aux;
        HitListMain.Instance.panelManageSoldier.InitCustomerData();
    }
    public void AddDeltas()
    {
        for (int i = 0; i < 3; i++)
        {
            AddDelta();
        }
    }
    public void CheckValue()
    {
        if (int.Parse(valueInputField.text) > 0)
        {
            headerText.sprite = headerWin;
            buttonText.sprite = buttonTextWin;
        }
        else if (int.Parse(valueInputField.text) < 0)
        {
            headerText.sprite = headerLose;
            buttonText.sprite = buttonTextLose;
        }
    }
    public void AddDelta()
    {
        DateTime currentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        if (customerDeltas.ContainsKey(currentDate))
        {
            //alter
            customerDeltas[currentDate] += int.Parse(valueInputField.text);
            Debug.Log("Delta updated");
        }
        else
        {
            // add new
            customerDeltas.Add(currentDate, int.Parse(valueInputField.text));
            Debug.Log("New Delta added");
        }
        valueInputField.text = "1";
        CheckValue();
        SaveDeltas();
        CountCustomers();
    }
    public void SaveDeltas()
    {
        string dataToSave = "";
        foreach (var item in customerDeltas)
        {
            dataToSave += item.Key.ToString() + "|" + item.Value.ToString() + "\n";
        }
        SaveLoad.Save(dataToSave, SaveLoad.CustomerDeltaFileName);
    }
}
