using UnityEngine;
using System.Collections;

public class BattlePointAttacker : BaseMobAttacker
{
    

    private float _damage;

    protected override void Awake()
    {
        base.Awake();

        IsAttackFactories = true;
    }

    protected override void ExecuteRound()
    {
        base.ExecuteRound();

        var victim = MobAttackerExtension.ChooseVictim(
            EnemyMobsInArea,
            MobAttackerExtension.ChooseVictimStrategy.TheWeakest);

        if (victim != null)
        {
            AddPointAttackParticles(ParticleCollection.Instance.DamagePoint, victim.MapPosition, new Vector3(0, -1f, 0));
            MobObject.FreezeMovement(Settings.Instance.StepTime);

            var currentVictimPosition = victim.MapPosition;

            StartCoroutine(CoroutineHelper.ExecuteAfterTime(GetTimeToRichMob(currentVictimPosition), () =>
            {
                // Check if victim is still on that position
                if (victim!=null && victim.MapPosition.Equals(currentVictimPosition))
                {
                    victim.GotAttacked(_damage, Settings.Instance.StepTime * 0.5f);
                }
            }));       
        }
    }

    public void SetParameters(BattlePointFactoryParameters parameters)
    {
        base.SetParameters(parameters);

        _damage = parameters.Damage;
    }

    public override bool ShouldMove()
    {
        return !CanAttackSomeone();
    }
}
