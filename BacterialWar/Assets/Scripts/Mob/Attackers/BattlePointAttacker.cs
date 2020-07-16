using UnityEngine;
using System.Collections;

public class BattlePointAttacker : BaseMobAttacker
{
    private float _damage;

    protected override void ExecuteRound()
    {
        base.ExecuteRound();

        var victim = MobAttackerExtension.ChooseVictim(
            EnemyMobsInArea,
            MobAttackerExtension.ChooseVictimStrategy.TheWeakest);

        if (victim != null)
        {
            victim.GotAttacked(_damage, Settings.Instance.StepTime * 0.5f);

            AddPointAttackParticles(ParticleCollection.Instance.DamagePoint, victim.MapPosition, new Vector3(0, -1f, 0));            
        }
    }

    public void SetParameters(BattlePointFactoryParameters parameters)
    {
        base.SetParameters(parameters);

        _damage = parameters.Damage;
    }
}
