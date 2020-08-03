using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalStatisticUI : Singleton<TotalStatisticUI>
{
    public Text timerDisplay;

    public float gameTime;
    private bool goTimer;
    private IEnumerator commandTimer;

    void OnDisable()
    {
        StopTimer();
    }

    public void StartTimer()
    {
        commandTimer = Timer();
        StartCoroutine(commandTimer);
    }
    public void StopTimer()
    {
        if (commandTimer != null)
        {
            StopCoroutine(commandTimer);
        }
    }
    public IEnumerator Timer()
    {
        while (true)
        {
            gameTime += Time.deltaTime;
            timerDisplay.text = ((int)gameTime).ToString();
            yield return null;
        }
    }

}
