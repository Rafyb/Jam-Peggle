using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    public int score = 0;

    public int jaugeNewBall = 0;
    public int newBallNeed = 30;

    public Action addBall;

    public Image jauge3;

    public Image bras; 

    public void AddJauge3(int nb)
    {
        jaugeNewBall += nb;
        UpdateUI();
        if(jaugeNewBall >= newBallNeed)
        {
            addBall?.Invoke();
            jaugeNewBall -= newBallNeed;
            UpdateUI();
        }
    }
    public void UpdateUI()
    {
        jauge3.fillAmount = jaugeNewBall / newBallNeed;
    }

    public void ActiveBras(bool b)
    {
        bras.gameObject.SetActive(b);
    }
}
