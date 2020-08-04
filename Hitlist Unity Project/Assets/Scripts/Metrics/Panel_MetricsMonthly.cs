using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Skrptr;

public class Panel_MetricsMonthly : MonoBehaviour
{
    public AudioSource sliderAS;
    public SkrptrElement dayMetrics, yearMetrics;
    public Dropdown yearDD, monthDD, graphDD;
    public Slider slider;
    public Text wonLabel, lostLabel,totalWonLabel,totalLostLabel,totalWonLabelValue,totalLostLabelValue;
    public DayBar[] dayBars;
    public GameObject[] positiveDots, negativeDots;
    public Text[] yValueTexts;
    public int maxValueGains, maxValueLosses, daysInMonth;
    public int[] gains, losses;

    private void Awake()
    {
        HitListMain.Instance.panelMetricsMonth = this;
    }
    private void Start()
    {
        for (int i = 0; i < dayBars.Length; i++)
        {
            dayBars[i].day.text = (i + 1).ToString();
        }
        yearDD.onValueChanged.AddListener(delegate { YearDDValueChanged(); });
        monthDD.onValueChanged.AddListener(delegate { MonthDDValueChanged(); });
        graphDD.onValueChanged.AddListener(delegate { GraphDDValueChanged(); });
        slider.onValueChanged.AddListener(delegate { SliderValueChanged(); });
        dayBars = GetComponentsInChildren<DayBar>();
        StartCoroutine(nameof(Init));
    }

    private void GraphDDValueChanged()
    {
        //Lock unlock shit
        if(graphDD.value==0)
        {
            GetComponent<SkrptrElement>().Lock();
            dayMetrics.Unlock();
        }
        else if(graphDD.value == 2)
        {
            GetComponent<SkrptrElement>().Lock();
            yearMetrics.Unlock();
        }
        graphDD.value = 1;
    }

    private IEnumerator Init()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        MonthDDValueChanged();
    }

    private void SliderValueChanged()
    {
        if (Time.realtimeSinceStartup > 6)
            sliderAS.Play();
        for (int i = 0; i < dayBars.Length; i++)
        {
            dayBars[i].activeState.gameObject.SetActive(false);
            positiveDots[i].gameObject.SetActive(false);
            negativeDots[i].gameObject.SetActive(false);
        }       
        dayBars[(int)slider.value].activeState.gameObject.SetActive(true);
        positiveDots[(int)slider.value].gameObject.SetActive(true);
        negativeDots[(int)slider.value].gameObject.SetActive(true);
        UpdateWonLostLabels();
    }
    private void UpdateWonLostLabels()
    {
        lostLabel.text = losses[(int)slider.value].ToString();
        wonLabel.text = gains[(int)slider.value].ToString();
    }
    private void MonthDDValueChanged()
    {     
        Debug.Log("Month changed");
        InitGraph();
        UpdateWonLostLabels();
        totalLostLabel.text = totalWonLabel.text = monthDD.options[monthDD.value].text;
        int totalGains = 0;
        int totalLosses = 0;
        for (int i = 0; i < gains.Length; i++)
        {
            totalGains += gains[i];
            totalLosses -= losses[i];
        }
        totalLostLabelValue.text = totalLosses.ToString();
        totalWonLabelValue.text = totalGains.ToString();
    }

    private void YearDDValueChanged()
    {
        InitGraph();
        UpdateWonLostLabels();
    }
    private void InitGraph()
    {
        CustomerDelta[] deltas = HitListMain.Instance.panelCustomerDelta.customerDeltas.Where(d => d.date.Month == monthDD.value + 1 && d.date.Year == yearDD.value + 2020).ToArray();
        Debug.Log("Found: " + deltas.Length + " deltas for " + monthDD.value + " " + yearDD.value);

        daysInMonth = DateTime.DaysInMonth(yearDD.value + 2020, monthDD.value + 1);
        gains = new int[daysInMonth];
        losses = new int[daysInMonth];
        for (int i = 0; i < daysInMonth; i++)
        {
            dayBars[i].gameObject.SetActive(true);
            CustomerDelta[] deltasInDay = deltas.Where(d => d.date.Day == i).ToArray();
            foreach (var delta in deltasInDay)
            {
                if (delta.value < 0)
                    losses[i] += delta.value;
                else
                    gains[i] += delta.value;
            }
        }
        maxValueGains = gains.Max();
        maxValueLosses = losses.Min();

        StopCoroutine(nameof(LerpFloats));
        StartCoroutine(nameof(LerpFloats));

        yValueTexts[0].text = maxValueGains.ToString();
        yValueTexts[1].text = ((float)maxValueGains * .8f).ToString();
        yValueTexts[2].text = ((float)maxValueGains * .6f).ToString();
        yValueTexts[3].text = ((float)maxValueGains * .4f).ToString();
        yValueTexts[4].text = ((float)maxValueGains * .2f).ToString();
        yValueTexts[5].text = "0";
        yValueTexts[6].text = ((float)maxValueLosses * .5f).ToString();
        yValueTexts[7].text = maxValueLosses.ToString();
        for (int i = daysInMonth; i < 31; i++)
        {
            dayBars[i].gameObject.SetActive(false);
        }
    }
    private IEnumerator LerpFloats()
    {
        float stepValue = 0.05f;
        bool valueChanged = true;
        while (valueChanged)
        {
            yield return new WaitForSecondsRealtime(0.016f);
            valueChanged = false;
            for (int i = 0; i < daysInMonth; i++)
            {

                float gainsTarget = (float)gains[i] / (float)maxValueGains;
                float lossesTarget = (float)losses[i] / (float)maxValueLosses;

                if (maxValueGains == 0)
                    gainsTarget = 0;
                if (maxValueLosses == 0)
                    lossesTarget = 0;
                //positive
                if (dayBars[i].positive.fillAmount > gainsTarget)
                {
                    if (dayBars[i].positive.fillAmount - stepValue > gainsTarget)
                    {
                        dayBars[i].positive.fillAmount -= stepValue;
                    }
                    else
                    {
                        dayBars[i].positive.fillAmount = gainsTarget;
                    }
                    valueChanged = true;
                }
                if (dayBars[i].positive.fillAmount < gainsTarget)
                {
                    if (dayBars[i].positive.fillAmount + stepValue < gainsTarget)
                    {
                        dayBars[i].positive.fillAmount += stepValue;
                    }
                    else
                    {
                        dayBars[i].positive.fillAmount = gainsTarget;
                    }
                    valueChanged = true;
                }
                //negative

                if (dayBars[i].negative.fillAmount > lossesTarget)
                {
                    if (dayBars[i].negative.fillAmount - stepValue > lossesTarget)
                    {
                        dayBars[i].negative.fillAmount -= stepValue;
                    }
                    else
                    {
                        dayBars[i].negative.fillAmount = lossesTarget;
                    }
                    valueChanged = true;
                }
                if (dayBars[i].negative.fillAmount < lossesTarget)
                {
                    if (dayBars[i].negative.fillAmount + stepValue < lossesTarget)
                    {
                        dayBars[i].negative.fillAmount += stepValue;
                    }
                    else
                    {
                        dayBars[i].negative.fillAmount = lossesTarget;
                    }
                    valueChanged = true;
                }

            }
        }

    }
}
