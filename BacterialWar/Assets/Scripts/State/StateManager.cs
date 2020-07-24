﻿using UnityEngine;
using UnityEngine.UI;

public class StateManager : SingletonMonoBehaviour<StateManager>
{
    [SerializeField]
    private float preparationTime;

    [SerializeField]
    private Text countingText;

    public GameState GameState { get; private set; }

    private void Start()
    {
        GameState = GameState.Preparation;

        int readPreparationTime = (int)preparationTime * (int)Settings.Instance.StepTime;
        int countingNumber = (int)readPreparationTime;
        countingText.text = countingNumber.ToString();

        StartCoroutine(CoroutineHelper.ExecuteAfterTime(readPreparationTime, () =>
        {
            Destroy(countingText);
            GameState = GameState.Fight;
        },
        ()=>{
            countingNumber--;
            countingText.text = countingNumber.ToString();
        }, 1));
    }

    public void DestroyedCrystal(CrystalObject crystalObject)
    {
        GameState = crystalObject.Player == Player.Player1
            ? GameState.Lost
            : GameState.Won;
    }
}
