using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int currentFloor = -1; // B1F

    public enum GameState
    {
        Title,
        Playing,
        Ending
    }
    public GameState currentState;

    private AnomalyGenerator anomalyGenerator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentState = GameState.Playing; // For now, start directly in playing state
        anomalyGenerator = FindObjectOfType<AnomalyGenerator>();
        ChangeFloor(-1); // Start at B1F
    }

    public void ChangeFloor(int floor)
    {
        currentFloor = floor;
        Debug.Log("現在の階層: " + currentFloor + "F");

        if (FindObjectOfType<UIManager>() != null)
        {
            FindObjectOfType<UIManager>().UpdateFloorText(currentFloor);
        }

        // 新しい階のJkTriggerをリセット
        foreach (var trigger in FindObjectsOfType<JkTrigger>())
        {
            trigger.ResetTrigger();
        }

        if (currentFloor == 8)
        {
            // 8階到達ロジック
            Debug.Log("8階に到達！エンディングを開始します。");
            EndingSequence ending = FindObjectOfType<EndingSequence>();
            if (ending != null)
            {
                ending.StartEnding();
            }
            else
            {
                Debug.LogError("EndingSequenceが見つかりません！");
            }
        }
        else
        {
            // 新しい階の異変を生成
            anomalyGenerator.GenerateAnomalyForFloor(currentFloor);
        }
    }

    public void PlayerChoseStairs(StairsTrigger.Direction direction)
    {
        bool hasAnomaly = anomalyGenerator.currentAnomaly != AnomalyGenerator.AnomalyType.None;
        bool choseUp = direction == StairsTrigger.Direction.Up;

        if (hasAnomaly && choseUp)
        {
            // 正解：異変があり、上に進んだ
            Debug.Log("正解！異変を発見し、先に進みます。");
            ChangeFloor(currentFloor + 1);
        }
        else if (!hasAnomaly && !choseUp)
        {
            // 正解：異変がなく、下に戻った
            Debug.Log("正解。異変がないため、下の階に戻ります。");
            // B1Fより下には行かないようにする
            int nextFloor = Mathf.Max(-1, currentFloor - 1);
            ChangeFloor(nextFloor);
        }
        else
        {
            // 不正解
            if(hasAnomaly && !choseUp)
            {
                Debug.Log("不正解！異変があったのに下に戻ったため、地下1階に戻されます。");
            }
            else if(!hasAnomaly && choseUp)
            {
                Debug.Log("不正解！異変がないのに上に進んだため、地下1階に戻されます。");
            }
            ChangeFloor(-1);
        }
    }
}
