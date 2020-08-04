using Skrptr;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Panel_MetricsYear : MonoBehaviour
{
    public AudioSource sliderAS;
    public LineRenderer lineRenderer;
    public Dropdown graphDD, yearDD;
    public Slider slider;
    public SkrptrElement dayMetrics, monthMetrics;
    public InputField janTarget, decTarget;
    public Text[] yValues;
    public GameObject[] activeStatuses;
    public Text[] winLabels, lossLabels,monthLabels;
    public Text projStartValue, projEndValue, actualStartValue, actualEndValue, resultValue, yearValue;

    public int[] wonByMonth, lostByMonth;
    public int wonThisYear, lostThisYear;

    public int[] clientsActualByMonth;
    public Vector3[] points;
    public float textureWidth = 950f, textureHeight = 410f;
    public Sprite redBorder, greenBorder;
    private void Awake()
    {
        HitListMain.Instance.panelMetricsYear = this;
    }

    private void Start()
    {
        graphDD.onValueChanged.AddListener(delegate { OnGraphDDValueChanged(); });
        yearDD.onValueChanged.AddListener(delegate { OnYearDDValueChanged(); });
        janTarget.onEndEdit.AddListener(delegate { OnEndEditJanTarget(); });
        decTarget.onEndEdit.AddListener(delegate { OnEndEditDecTarget(); });
        slider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });

        wonByMonth = new int[12];
        lostByMonth = new int[12];
        clientsActualByMonth = new int[12];

        string yearStatistics = SaveLoad.Load(SaveLoad.YearStatisticsFileName);
        if (yearStatistics != null)
        {
            string[] yearStatisticsData = yearStatistics.Split('|');
          
            janTarget.text = yearStatisticsData[0];
            decTarget.text = yearStatisticsData[1];
            UpdateYValues();
        }
        Invoke(nameof(OnYearDDValueChanged), 1);
        Invoke(nameof(OnSliderValueChanged), 1.1f);
    }

    private void OnSliderValueChanged()
    {
        if (Time.realtimeSinceStartup > 6)
            sliderAS.Play();
        foreach (var go in activeStatuses)
        {
            go.SetActive(false);
        }
        activeStatuses[(int)slider.value-1].SetActive(true);

        string monthName = new DateTime(2010, (int)(slider.value), 1).ToString("MMM", CultureInfo.InvariantCulture).ToUpper() + ":";
        foreach (var item in monthLabels)
        {
            item.text = monthName;
        }
        yearValue.text = clientsActualByMonth[clientsActualByMonth.Length - 1].ToString();


        projStartValue.text = (int.Parse(decTarget.text) / 12 * (slider.value-1)).ToString();
        projEndValue.text = (int.Parse(decTarget.text) / 12 * (slider.value)).ToString();
        if (slider.value == 1)
        {
            actualStartValue.text = "0";
        }
        else
        {
            actualStartValue.text = clientsActualByMonth[(int)(slider.value - 2)].ToString();
        }
        actualEndValue.text = clientsActualByMonth[(int)(slider.value-1)].ToString();
        resultValue.text = (wonByMonth[(int)slider.value - 1] + lostByMonth[(int)slider.value - 1]).ToString();

        //colors
        if (int.Parse(projStartValue.text) > int.Parse(actualStartValue.text))
            actualStartValue.color = Color.red;
        else
            actualStartValue.color = Color.green;
        if (int.Parse(projEndValue.text) > int.Parse(actualEndValue.text))
            actualEndValue.color = Color.red;
        else
            actualEndValue.color = Color.green;

        if(int.Parse(yearValue.text) > int.Parse(decTarget.text))
        {
            yearValue.transform.parent.GetComponent<Image>().sprite = greenBorder;
            yearValue.color = Color.green;
        }
        else
        {
            yearValue.transform.parent.GetComponent<Image>().sprite = redBorder;
            yearValue.color = Color.red;
        }

    }

    private void OnEndEditDecTarget()
    {
        UpdateYValues();
    }

    private void OnEndEditJanTarget()
    {
        UpdateYValues();        
    }
    private void UpdateYValues()
    {
        int delta = int.Parse(decTarget.text) - int.Parse(janTarget.text);
        yValues[0].text = janTarget.text;
        yValues[1].text = (int.Parse(janTarget.text) + (float)delta * 0.2f).ToString("0");
        yValues[2].text = (int.Parse(janTarget.text) + (float)delta * 0.4f).ToString("0");
        yValues[3].text = (int.Parse(janTarget.text) + (float)delta * 0.6f).ToString("0");
        yValues[4].text = (int.Parse(janTarget.text) + (float)delta * 0.8f).ToString("0");
        yValues[5].text = decTarget.text;
        OnYearDDValueChanged();
        SaveData();
    }
    public void SaveData()
    {
        string dataToSave = "";
        dataToSave += janTarget.text + "|" + decTarget.text;
        SaveLoad.Save(dataToSave, SaveLoad.YearStatisticsFileName);
    }

    private void OnYearDDValueChanged()
    {
        wonByMonth = new int[12];
        lostByMonth = new int[12];
        clientsActualByMonth = new int[12];
        //Debug.Log((yearDD.value + 2020).ToString());
        List<CustomerDelta> customerDeltasThisYear = HitListMain.Instance.panelCustomerDelta.customerDeltas.Where(d => d.date.Year == yearDD.value + 2020).ToList<CustomerDelta>();
        //Debug.Log("Deltas: " + customerDeltasThisYear.Count);
        for (int i = 1; i <= 12; i++)
        {
            List<CustomerDelta> customerDeltasThisMonth = customerDeltasThisYear.Where(d => d.date.Month == i).ToList<CustomerDelta>();
            foreach (var delta in customerDeltasThisMonth)
            {
                if (delta.value < 0)
                    lostByMonth[i-1] += delta.value;
                else
                    wonByMonth[i-1] += delta.value;
            }
            winLabels[i-1].text = wonByMonth[i-1].ToString();
            lossLabels[i-1].text = lostByMonth[i - 1].ToString();
        }
        int sum = 0;
        points = new Vector3[clientsActualByMonth.Length+1];
        points[0] = new Vector3(0, 0,0);
        for (int i = 0; i < 12; i++)
        {
            sum += wonByMonth[i] + lostByMonth[i];
            clientsActualByMonth[i] = sum;
            points[i+1] = new Vector3((i+1) / 12f * textureWidth, clientsActualByMonth[i] / float.Parse(decTarget.text) * textureHeight,0);
        }
        lineRenderer.SetPositions(points);
    }


    private void OnGraphDDValueChanged()
    {
        //Lock unlock shit
        if (graphDD.value == 0)
        {
            GetComponent<SkrptrElement>().Lock();
            dayMetrics.Unlock();
        }
        else if (graphDD.value == 1)
        {
            GetComponent<SkrptrElement>().Lock();
            monthMetrics.Unlock();
        }
        graphDD.value = 2;
    }
}
