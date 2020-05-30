using UnityEngine;
using System.Collections;
using System.Linq;

public class BattleAreaAttacker : BaseMobAttacker
{
    [SerializeField]
    private float _damage = 3;

    protected override void ExecuteRound()
    {
        base.ExecuteRound();

        foreach (var victim in EnemyMobsInArea)
        {
            victim.GotAttacked(_damage, 0);
        }
    }
}
