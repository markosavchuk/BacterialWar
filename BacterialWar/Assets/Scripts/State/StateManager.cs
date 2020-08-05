using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StateManager : SingletonMonoBehaviour<StateManager>
{
    [SerializeField]
    private float _preparationTime;

    public GameState GameState { get; private set; }

    private bool _speededUp = false;

    protected override void OnAwake()
    {
        base.OnAwake();

        GameState = GameState.NotStarted;
        Time.timeScale = 0;
    }

    public void StartOrResumeGame()
    {
        Time.timeScale = _speededUp ? 2 : 1;

        if (GameState == GameState.NotStarted) 
        {
            GameState = GameState.Preparation;
            StartCoroutine(CoroutineHelper.ExecuteAfterTime(_preparationTime, () =>
            {
                GameState = GameState.Fight;
            }));
        }
    }

    public void StopGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void SpeedUpGame(bool speedUp)
    {
        Time.timeScale = speedUp ? 2 : 1;
        _speededUp = speedUp;
    }

    public void DestroyedCrystal(CrystalObject crystalObject)
    {
        GameState = crystalObject.Player == Player.MyPlayer
            ? GameState.Lost
            : GameState.Won;
    }
}
