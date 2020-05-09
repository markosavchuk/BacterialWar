using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    //todo make helper class for it
    #region Singleton logic    

    public static StateManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

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
