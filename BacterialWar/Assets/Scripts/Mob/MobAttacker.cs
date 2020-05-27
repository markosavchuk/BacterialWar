using UnityEngine;

public class MobAttacker : MonoBehaviour
{
    private MobObject _mobObject;

    [SerializeField]
    private float _damage = 10f;

    private float _time = 0f;

    private void Awake()
    {
        _mobObject = GetComponent<MobObject>();

        //todo remove it later
        if (_mobObject.Player== Player.Player1)
        {
            _damage *= 2;
        }
    }

    private void Update()
    {
        _time += Time.deltaTime;

        if (_time >= Settings.Instance.StepTime)
        {
            _time -= Settings.Instance.StepTime;

            ExecuteRound();
        }
    }

    private void ExecuteRound()
    {
        if (StateManager.Instance.GameState == GameState.Preparation)
        {
            return;
        }

        if (_mobObject.IsInMotion)
        {
            return;
        }

        var richArea = MobAttackerExtension.GetRichArea(_mobObject);
        var mobsOnArea = MobAttackerExtension.GetMobsOnArea(richArea);
        var victim = MobAttackerExtension.ChooseVictim(mobsOnArea, _mobObject.Player);

        if (victim != null)
        {
            victim.GotAttacked(_damage);
        }
    }
}
