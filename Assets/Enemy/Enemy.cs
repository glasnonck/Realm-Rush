using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Tooltip("Amount player earns for defeating this enemy.")]
    [SerializeField] int goldReward = 25;
    [Tooltip("Amount player loses for this enemy reaching their base.")]
    [SerializeField] int goldPenalty = 25;

    Bank bank;
    
    private void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void RewardGold()
    {
        if (bank == null) { return; }
        bank.Deposit(goldReward);
    }

    public void StealGold()
    {
        if (bank == null) { return; }
        bank.Withdraw(goldPenalty);
    }
}
