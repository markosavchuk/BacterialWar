using UnityEngine;
using System.Collections;
using System.Linq;

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
            MobObject.FreezeMovement(Settings.Instance.StepTime * 0.25f);
        }
    }

    public void SetParameters(InfectionFactoryParameters parameters)
    {
        base.SetParameters(parameters);

        _permanentDamage = parameters.Infection;
    }

    public override bool ShouldMove()
    {
        if (!CanAttackSomeone())
        {
            return true;
        }
        else
        {
            return CanAttackSomeone() && EnemyMobsInArea.All(m => m.Infection > 0);
        }
    }
}
