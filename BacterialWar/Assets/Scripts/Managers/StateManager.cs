using UnityEngine;
using UnityEngine.UI;

public class StateManager : SingletonMonoBehaviour<StateManager>
{
    [SerializeField]
    private float preparationTime;

    [SerializeField]
    private Text countingText;

    public GameState GameState { get; set; }

    private void Start()
    {
        GameState = GameState.Preparation;

        int countingNumber = (int)preparationTime;
        countingText.text = countingNumber.ToString();

        StartCoroutine(CoroutineHelper.ExecuteAfterTime(preparationTime, () =>
        {
            Destroy(countingText);
            GameState = GameState.Fight;
        },
        ()=>{
            countingNumber--;
            countingText.text = countingNumber.ToString();
        }, 1));
    }
}
