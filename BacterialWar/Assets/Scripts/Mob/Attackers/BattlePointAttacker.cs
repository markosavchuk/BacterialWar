using UnityEngine;
using System.Collections;

public class BattlePointAttacker : BaseMobAttacker
{
    //todo make a balance model with connected all damage values between each other
    [SerializeField]
    private float _damage = 10;

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
}
