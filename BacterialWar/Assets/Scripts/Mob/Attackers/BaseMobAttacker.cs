using System.Collections.Generic;
using UnityEngine;

public class BaseMobAttacker : MonoBehaviour
{
    protected MobObject MobObject;

    private float _time = 0f;
    private float _roundSpeed;

    protected IEnumerable<MobObject> EnemyMobsInArea;

    public float FightPeriod = 1;

    private void Awake()
    {
        MobObject = GetComponent<MobObject>();

        _roundSpeed = Settings.Instance.StepTime * FightPeriod;
    }

    private void Update()
    {
        _time += Time.deltaTime;

        if (_time >= _roundSpeed)
        {
            _time -= _roundSpeed;

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
}
