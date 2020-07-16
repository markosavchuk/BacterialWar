using UnityEngine;
using System.Collections;

public class InfectionAttacker : BaseMobAttacker
{
    [SerializeField]
    private float _permanentDamage;

    protected override void ExecuteRound()
    {
        base.ExecuteRound();

        var victim = MobAttackerExtension.ChooseVictim(
            EnemyMobsInArea,
            MobAttackerExtension.ChooseVictimStrategy.TheStrongest);

        if (victim != null)
        {
            victim.GotInfected(_permanentDamage);

            AddPointAttackParticles(ParticleCollection.Instance.DamageInfection, victim.MapPosition, new Vector3(0, 1f, 0));
        }
    }

    public void SetParameters(InfectionFactoryParameters parameters)
    {
        base.SetParameters(parameters);

        _permanentDamage = parameters.Infection;
    }
}
