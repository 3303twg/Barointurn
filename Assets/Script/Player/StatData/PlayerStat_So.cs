using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "GameData/PlayerStat")]
public class PlayerStat_So : ScriptableObject
{
    public float MaxHp;
    public float MoveSpeed;
    public float AttackPower;
    public float AttackSpeed;
    public float AttackRange;

}
