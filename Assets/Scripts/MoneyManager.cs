using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private int startMoney;
    [SerializeField] private TextMeshProUGUI txt_Money;

    private void Start()
    {
        txt_Money.text = startMoney.ToString();
    }
}
