using UnityEngine;
using System.Collections;
using System.Linq;

public class FreezeAttacker : BaseMobAttacker
{
    [SerializeField]
    private float _freezeTime;

    private float _freezeRealTime;

    protected override void Awake()
    {
        base.Awake();

        AddAudioSource(AudioCollection.Instance.FreezeSound);
    }

    protected override void ExecuteRound()
    {
        base.ExecuteRound();
       
        if (EnemyMobsInArea.Any())
        {
            AddWaveAttackParticle(ParticleCollection.Instance.DamageFreeze, 10, new Vector3(0, 0, 0));
            MobObject.FreezeMovement(Settings.Instance.StepTime * 0.25f);

            if (StateManager.Instance.GameState != GameState.Won)
            {
                SoundAudioSource.Play();
            }
        }

        foreach (MobObject victim in EnemyMobsInArea)
        {
            var currentVictimPosition = victim.MapPosition;

            StartCoroutine(CoroutineHelper.ExecuteAfterTime(GetTimeToRichMob(currentVictimPosition), () =>
            {
                if (victim != null && RichArea.Contains(victim.MapPosition))
                {
                    victim.FreezeFight(_freezeRealTime);
                    victim.FreezeMovement(_freezeRealTime);
                }
            }));            
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
            return EnemyMobsInArea
                .Select(m => m as MobObject)
                .All(m => m!=null && m.FrozenMovement >= Settings.Instance.StepTime * 0.5f);
        }
    }
}
