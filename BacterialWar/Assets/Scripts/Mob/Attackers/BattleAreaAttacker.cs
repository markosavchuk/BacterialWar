using UnityEngine;
using System.Collections;
using System.Linq;

public class BattleAreaAttacker : BaseMobAttacker
{
    private float _damage;

    protected override void ExecuteRound()
    {
        base.ExecuteRound();

        foreach (var victim in EnemyMobsInArea)
        {
            victim.GotAttacked(_damage, 0);
        }

        if (EnemyMobsInArea.Any())
        {
            AddWaveAttackParticle(ParticleCollection.Instance.DamageArea, 10, new Vector3(0, -1f, 0));
        }
    }

    public void SetParameters(BattleAreaFactoryParameters parameters)
    {
        base.SetParameters(parameters);

        _damage = parameters.WaveDamage;
    }
}
