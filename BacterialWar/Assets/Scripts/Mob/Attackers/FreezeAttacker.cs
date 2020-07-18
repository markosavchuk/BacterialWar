using UnityEngine;
using System.Collections;
using System.Linq;

public class FreezeAttacker : BaseMobAttacker
{
    [SerializeField]
    private float _freezeTime;

    private float _freezeRealTime;

    protected override void ExecuteRound()
    {
        base.ExecuteRound();

        foreach (var mob in EnemyMobsInArea)
        {
            mob.FreezeFight(_freezeRealTime);
            mob.FreezeMovement(_freezeRealTime);
        }

        if (EnemyMobsInArea.Any())
        {
            AddWaveAttackParticle(ParticleCollection.Instance.DamageFreeze, 10, new Vector3(0, 0, 0));
            MobObject.FreezeMovement(Settings.Instance.StepTime * 0.25f);
        }
    }

    public void SetParameters(FreezeFactoryParameters parameters)
    {
        base.SetParameters(parameters);

        _freezeTime = parameters.Freeze;

        _freezeRealTime = _freezeTime * Settings.Instance.StepTime;
    }

    public override bool ShouldMove()
    {
        if (!CanAttackSomeone())
        {
            return true;
        }
        else
        {
            return EnemyMobsInArea.All(m => m.FrozenMovement >= Settings.Instance.StepTime * 0.5f);
        }
    }
}
