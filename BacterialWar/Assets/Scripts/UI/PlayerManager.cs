using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private Button _stopButton;

    [SerializeField]
    private Button _playButton;

    [SerializeField]
    private Button _pauseButton;

    [SerializeField]
    private Button _speedUpTurnOnButton;

    [SerializeField]
    private Button _speedUpTurnOffButton;

    private bool _speededUp = false;

    public void Play()
    {
        StateManager.Instance.StartOrResumeGame();

        _playButton.gameObject.SetActive(false);
        _pauseButton.gameObject.SetActive(true);
        _speedUpTurnOnButton.gameObject.SetActive(!_speededUp);
        _speedUpTurnOffButton.gameObject.SetActive(_speededUp);

        _stopButton.interactable = true;
        _speedUpTurnOnButton.interactable = true;
        _speedUpTurnOffButton.interactable = true;      
    }

    public void Pause()
    {
        StateManager.Instance.PauseGame();

        _playButton.gameObject.SetActive(true);
        _pauseButton.gameObject.SetActive(false);

        _speedUpTurnOnButton.interactable = false;
        _speedUpTurnOffButton.interactable = false;
    }

    public void SpeedUp(bool speedUp)
    {
        StateManager.Instance.SpeedUpGame(speedUp);

        _speededUp = speedUp;

        _speedUpTurnOnButton.gameObject.SetActive(!_speededUp);
        _speedUpTurnOffButton.gameObject.SetActive(_speededUp);
    }

    public void Stop()
    {
        StateManager.Instance.StopGame();

        _speededUp = false;

        _playButton.gameObject.SetActive(true);
        _pauseButton.gameObject.SetActive(false);
        _speedUpTurnOnButton.gameObject.SetActive(!_speededUp);
        _speedUpTurnOffButton.gameObject.SetActive(_speededUp);

        _stopButton.interactable = false;
        _speedUpTurnOnButton.interactable = false;
        _speedUpTurnOffButton.interactable = false;
    }
}
