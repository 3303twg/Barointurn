using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : BaseEffect
{
    public float maxRange = 2f;
    public override void Effect()
    {
        PlayerController.Instance.playerStat.AttackPower += Random.Range(0f, maxRange);
    
    }

}
