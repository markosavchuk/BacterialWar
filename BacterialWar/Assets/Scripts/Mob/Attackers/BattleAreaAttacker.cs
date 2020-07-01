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

        if (EnemyMobsInArea.Any())
        {
            AddAttackParticles(MobObject.RiachRange);
        }
    }

    private void AddAttackParticles(int richRange)
    {
        var particlePrefab = ParticleCollection.Instance.DamageArea;

        var particleObject = Instantiate(particlePrefab);
        particleObject.transform.position = gameObject.transform.position;
        particleObject.transform.parent = gameObject.transform;

        var pointParticleMover = particleObject.AddComponent<AreaParticleMovement>();
        pointParticleMover.RichRange = richRange;
    }
}
