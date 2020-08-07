using UnityEngine;
using System.Collections;
using System.Linq;

public class BattleAreaAttacker : BaseMobAttacker
{
    private float _damage;

    protected override void Awake()
    {
        base.Awake();

        IsAttackFactories = true;

        AddAudioSource(AudioCollection.Instance.WaveAttackSound);
    }

    protected override void ExecuteRound()
    {
        base.ExecuteRound();        

        if (EnemyMobsInArea.Any())
        {
            AddWaveAttackParticle(ParticleCollection.Instance.DamageArea, 10, new Vector3(0, -1f, 0));
            MobObject.FreezeMovement(Settings.Instance.StepTime);

            if (StateManager.Instance.GameState != GameState.Won)
            {
                SoundAudioSource.Play();
            }
        }

        foreach (var victim in EnemyMobsInArea)
        {
            var currentVictimPosition = victim.MapPosition;

            StartCoroutine(CoroutineHelper.ExecuteAfterTime(GetTimeToRichMob(currentVictimPosition), () =>
            {
                if (victim != null && RichArea.Contains(victim.MapPosition)) 
                {
                    victim.GotAttacked(_damage, 0);
                }
            }));            
        }
    }

    public void SetParameters(BattleAreaFactoryParameters parameters)
    {
        base.SetParameters(parameters);

        _damage = parameters.WaveDamage;
    }

    public override bool ShouldMove()
    {
        return !CanAttackSomeone();
    }
}
