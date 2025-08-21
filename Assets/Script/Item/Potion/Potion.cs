using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : BaseEffect
{
    public float maxRange = 0.2f;

    
    public float maxDelay = 0.03f;
    public override void Effect()
    {
        PlayerController.Instance.playerStat.AttackSpeed = Mathf.Max (PlayerController.Instance.playerStat.AttackSpeed - Random.Range(0f, maxRange), maxDelay);
    }
}
