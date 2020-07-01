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

            AddAttackParticles(victim.MapPosition);
        }
    }

    private void AddAttackParticles(Vector2Int targetPosition)
    {
        var particlePrefab = ParticleCollection.Instance.DamagePoint;

        var particleObject = Instantiate(particlePrefab);
        particleObject.transform.position = gameObject.transform.position;
        particleObject.transform.parent = gameObject.transform;

        var pointParticleMover = particleObject.AddComponent<PointParticleMovement>();
        pointParticleMover.TargetMapPosition = targetPosition;
    }
}
