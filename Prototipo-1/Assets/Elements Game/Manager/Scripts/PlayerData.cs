using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    public float lifePlayer;
    public float maxLifePlayer;
    //[HideInInspector]
    public float score;
    [HideInInspector]
    public float auxScore;
    public float scoreForHit;
    public float scoreForEnemyDead;
}
