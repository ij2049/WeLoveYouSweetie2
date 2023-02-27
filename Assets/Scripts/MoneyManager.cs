using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;


public class MoneyManager : MonoBehaviour
{
    [SerializeField] private int startMoney;
    [SerializeField] private TextMeshProUGUI txt_Money;
    [SerializeField] private TextMeshProUGUI txt_MoneyInfo; //Showing the UI animation. (money add/use)
    [SerializeField] private Animator anim_moneyInfo;
    
    //Money
    private int currentMoney;
    
    //Data
    private PhotonView view;
    public static MoneyManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        view = GetComponent<PhotonView>();
        txt_Money.text = startMoney.ToString();
        currentMoney = startMoney;
    }

    //When the player earned money
    public void TryAddMoney(int _moneyAmount)
    {
        view.RPC("AddMoney",RpcTarget.All, _moneyAmount);
    }

    [PunRPC]
    void AddMoney(int _moneyAmount)
    {
        StartCoroutine(AddMoneyCoroutine(_moneyAmount));
    }

    private IEnumerator AddMoneyCoroutine(int _moneyAmount)
    {
        txt_MoneyInfo.text = "+$" + _moneyAmount;
        currentMoney += _moneyAmount;
        txt_Money.text = currentMoney.ToString();
        anim_moneyInfo.SetBool("Play", true);
        yield return new WaitForSeconds(1f);
        anim_moneyInfo.SetBool("Play", false);
    }
    
    //When the player used money
    public void TryUseMoney(int _moneyAmount)
    {
        view.RPC("UseMoney",RpcTarget.All, _moneyAmount);
    }
    
    [PunRPC]
    void UseMoney(int _moneyAmount)
    {
        StartCoroutine(UseMoneyCoroutine(_moneyAmount));
    }
    
    private IEnumerator UseMoneyCoroutine(int _moneyAmount)
    {
        txt_MoneyInfo.text = "-$" + _moneyAmount;
        currentMoney -= _moneyAmount;
        txt_Money.text = currentMoney.ToString();
        anim_moneyInfo.SetBool("Play", true);
        yield return new WaitForSeconds(1f);
        anim_moneyInfo.SetBool("Play", false);
    }
}
