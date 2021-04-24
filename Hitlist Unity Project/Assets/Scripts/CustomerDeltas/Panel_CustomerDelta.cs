using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Panel_CustomerDelta : MonoBehaviour
{
    public List<CustomerDelta> customerDeltas;
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

        customerDeltas = new List<CustomerDelta>();
        string savedData = SaveLoad.Load(SaveLoad.CustomerDeltaFileName);
        if (savedData != null)
        {
            string[] deltas = savedData.Split('\n');
            for (int i = 0; i < deltas.Length - 1; i++)
            {
                string[] delta = deltas[i].Split('|');
                DateTime dateTime = DateTime.Parse(delta[0]);
                int value = int.Parse(delta[1]);
                customerDeltas.Add(new CustomerDelta(dateTime, value));
                //Debug.LogError("Added delta: " + dateTime.ToString() + " " + value);
            }
        }
        //GenerateDeltas();
    }
    public void GenerateDeltas()
    {
        customerDeltas.Clear();
        for (int i = 1; i <= 12; i++)
        {
            for (int j = 1; j <= DateTime.DaysInMonth(2021,i); j++)
            {
                customerDeltas.Add(new CustomerDelta(new DateTime(2021, i, j), UnityEngine.Random.Range(-10, 0)));
                customerDeltas.Add(new CustomerDelta(new DateTime(2021, i, j), UnityEngine.Random.Range(0, 30)));
            }
        }
        SaveDeltas();
    }
    public void CountCustomers()
    {
        int aux = 0;
        foreach (var item in customerDeltas)
        {
            aux += item.value;
        }
        HitListMain.Instance.panelManageSoldier.currentCustomers = aux;
        HitListMain.Instance.panelManageSoldier.InitCustomerData();
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
        customerDeltas.Add(new CustomerDelta(currentDate, int.Parse(valueInputField.text)));

        valueInputField.text = "1";
        CheckValue();
        SaveDeltas();
        CountCustomers();
        HitListMain.Instance.panelMetricsYear.RefreshValues();
    }
    public void SaveDeltas()
    {
        string dataToSave = "";
        foreach (var item in customerDeltas)
        {
            dataToSave += item.date.ToString() + "|" + item.value.ToString() + "\n";
        }
        SaveLoad.Save(dataToSave, SaveLoad.CustomerDeltaFileName);
    }
}
public class CustomerDelta
{
    public DateTime date;
    public int value;

    public CustomerDelta(DateTime date, int value)
    {
        this.date = date;
        this.value = value;
    }
}
