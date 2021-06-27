using System;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorBar : MonoBehaviour
{
    [SerializeField] private List<Indicator> indicators;
    [SerializeField] private RectTransform startPoint;

    private float _time;
    private float _offset = 0.2f;

    private int level = 0;

    private float _maxTime = 60f;
    

    public void SetMaxTime(float maxTime)
    {
        _maxTime = maxTime;
    }

    public void SetTimeLeft(float timeLeft)
    {
        _time = _maxTime - timeLeft;
    }

    private void Update()
    {
        level = (int) Math.Floor(_time / _maxTime * 10);
        
        UpdateIndicators();
    }

    private void UpdateIndicators()
    {
        int i = 0;
        foreach (Indicator indicator in indicators)
        {
            int indicatorLevel = level - i;
            if (indicatorLevel > 2) indicatorLevel = 2;
            if (indicatorLevel < 0) indicatorLevel = 0;
          
            indicator.ShowLevel(indicatorLevel);

            i += 2;
        }
    }


    private void Start()
    {
        //InitIndicators();   
    }

    private void InitIndicators()
    {
        Vector3 pos = startPoint.transform.position;
        pos.z = 1;
        foreach (Indicator indicator in indicators)
        {
            indicator.transform.position = pos;
            pos += new Vector3(_offset, 0, 0);
        }
    }
}
