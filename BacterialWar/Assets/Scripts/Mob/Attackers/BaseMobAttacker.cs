using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseMobAttacker : MonoBehaviour
{
    private const float RichTimeConst = 2.5f;

    protected MobObject MobObject;

    private float _time = 0f;
    private float _roundPeriod = int.MaxValue;

    protected IEnumerable<HexContent> EnemyMobsInArea;
    protected IEnumerable<Vector2Int> RichArea;
    protected bool IsAttackFactories = false;

    protected virtual void Awake()
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
        RichArea = MobAttackerExtension.GetRichArea(MobObject);
        EnemyMobsInArea = MobAttackerExtension.GetEnemyInArea(RichArea, MobObject.Player, IsAttackFactories);
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
        _roundPeriod = parameters.AttackSpeed * Settings.Instance.StepTime;
    }

    private GameObject AddParticle(GameObject particlePrefab)
    {
        var particleObject = Instantiate(particlePrefab, gameObject.transform);
        return particleObject;
    }

    protected bool CanAttackSomeone()
    {
        RichArea = MobAttackerExtension.GetRichArea(MobObject);
        EnemyMobsInArea = MobAttackerExtension.GetEnemyInArea(RichArea, MobObject.Player, IsAttackFactories);
        return EnemyMobsInArea.Any();
    }

    protected float GetTimeToRichMob(Vector2Int vectimMobPosition)
    {
        return Vector2Int.Distance(MobObject.MapPosition, vectimMobPosition) * Settings.Instance.StepTime / RichTimeConst;
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
