using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseMobAttacker : MonoBehaviour
{
    protected MobObject MobObject;

    private float _time = 0f;
    private float _roundPeriod = int.MaxValue;

    protected IEnumerable<MobObject> EnemyMobsInArea;

    private void Awake()
    {
        MobObject = GetComponent<MobObject>();        
    }

    private void Update()
    {
        _time += Time.deltaTime;

        if (_time >= _roundPeriod)
        {
            _time -= _roundPeriod;

            TryExecuteRound();
        }
    }

    private void TryExecuteRound()
    {
        if (StateManager.Instance.GameState == GameState.Preparation)
        {
            return;
        }

        if (MobObject.IsInMotion || MobObject.FrozenFight > 0)
        {
            return;
        }

        ExecuteRound();
    }

    protected virtual void ExecuteRound()
    {
        var richArea = MobAttackerExtension.GetRichArea(MobObject);
        EnemyMobsInArea = MobAttackerExtension.GetEnemyMobsInArea(richArea, MobObject.Player);
    }

    protected void AddPointAttackParticles(GameObject particlePrefab, Vector2Int targetPosition, Vector3 offset) 
    {
        var particleObject = AddParticle(particlePrefab);

        var pointParticleMover = particleObject.AddComponent<PointParticleMovement>();
        pointParticleMover.TargetMapPosition = targetPosition;
        pointParticleMover.Offset = offset;
    }

    protected void AddWaveAttackParticle(GameObject particlePrefab, float radiusMultiplier, Vector3 offset)
    {
        var particleObject = AddParticle(particlePrefab);

        var areaParticleMover = particleObject.AddComponent<AreaParticleMovement>();
        areaParticleMover.RichRange = MobObject.RiachRange;
        areaParticleMover.RadiusMultiplier = radiusMultiplier;
        areaParticleMover.Offset = offset;
    }

    protected void SetParameters(FactoryParameters parameters)
    {
        _roundPeriod = FactoryParameters.AttackSpeedConst / parameters.Speed * Settings.Instance.StepTime;
    }

    private GameObject AddParticle(GameObject particlePrefab)
    {
        var particleObject = Instantiate(particlePrefab, gameObject.transform);
        return particleObject;
    }

    protected bool CanAttackSomeone()
    {
        var richArea = MobAttackerExtension.GetRichArea(MobObject);
        EnemyMobsInArea = MobAttackerExtension.GetEnemyMobsInArea(richArea, MobObject.Player);
        return EnemyMobsInArea.Any();
    }

    public virtual bool ShouldMove()
    {
        return true;
    }

    public void ResetWaitingTime()
    {
        _time = Time.deltaTime;
    }
}
