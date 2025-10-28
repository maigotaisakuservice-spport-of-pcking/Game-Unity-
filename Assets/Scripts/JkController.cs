using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class JkController : MonoBehaviour
{
    [Header("Setup")]
    public Transform idlePosition; // 階段下などの待機位置
    public Transform player; // プレイヤーのTransform

    [Header("Settings")]
    [Range(0, 1)]
    public float chaseProbability = 0.3f;

    private NavMeshAgent agent;

    public enum JkState
    {
        Idle,
        Returning, // 待機位置に戻っている途中
        Chasing
    }
    public JkState currentState { get; private set; } = JkState.Idle;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }
    }

    void Update()
    {
        if (currentState == JkState.Chasing && player != null)
        {
            agent.SetDestination(player.position);
        }
        // 待機位置に戻る処理
        else if (currentState == JkState.Returning)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                currentState = JkState.Idle;
            }
        }
    }

    // JkTriggerから呼び出される
    public void OnPlayerPasses()
    {
        if (currentState != JkState.Idle) return;

        // 0.2秒後に判定
        StartCoroutine(DecideActionAfterDelay(0.2f));
    }

    private IEnumerator DecideActionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (Random.value < chaseProbability)
        {
            StartChase();
        }
        else
        {
            // 追跡しない場合は、何もしないか、あるいは企画書通り下に移動する
            // ここでは待機位置に戻るようにしておく
            ReturnToIdlePosition();
        }
    }

    public void StartChase()
    {
        if (player == null) return;

        currentState = JkState.Chasing;

        // 演出の呼び出し（プレースホルダー）
        Debug.Log("狂気的な笑顔で追跡を開始！");
        Debug.Log("演出: 画面端チラつき、影演出、環境音増加");
        if(SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayJkChaseSound();
        }
    }

    public void StopChase()
    {
        if (currentState != JkState.Chasing) return;

        Debug.Log("女子高生が追跡を停止しました。");
        ReturnToIdlePosition();
    }

    private void ReturnToIdlePosition()
    {
        currentState = JkState.Returning;
        if (idlePosition != null)
        {
            agent.SetDestination(idlePosition.position);
        }
    }
}
