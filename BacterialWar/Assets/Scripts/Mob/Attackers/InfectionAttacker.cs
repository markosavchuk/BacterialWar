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

        if (victim != null && victim is MobObject mobVictom)
        {
            AddPointAttackParticles(ParticleCollection.Instance.DamageInfection, victim.MapPosition, new Vector3(0, 1f, 0));
            MobObject.FreezeMovement(Settings.Instance.StepTime * 0.25f);

            var currentVictimPosition = victim.MapPosition;

            StartCoroutine(CoroutineHelper.ExecuteAfterTime(GetTimeToRichMob(currentVictimPosition), () =>
            {
                if (victim != null && victim.MapPosition.Equals(currentVictimPosition))
                {
                    mobVictom.GotInfected(_permanentDamage);
                }
            }));
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
            return EnemyMobsInArea
                .Select(m => m as MobObject)
                .All(m => m != null && m.Infection > 0);
        }
    }
}
