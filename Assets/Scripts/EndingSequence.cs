using UnityEngine;
using System.Collections;

public class EndingSequence : MonoBehaviour
{
    [Header("Sequence Objects")]
    public GameObject busStopAndPassengers;
    public GameObject bus;
    public Transform playerSeatPosition;

    private UIManager uiManager;
    private Transform player;

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        // 開始時は非表示にしておく
        if (busStopAndPassengers != null) busStopAndPassengers.SetActive(false);
        if (bus != null) bus.SetActive(false);
    }

    public void StartEnding()
    {
        GameManager.Instance.currentState = GameManager.GameState.Ending;
        StartCoroutine(PlayEndingSequence());
    }

    private IEnumerator PlayEndingSequence()
    {
        // プレイヤーの操作を無効化
        if(player != null) player.GetComponent<PlayerController>().enabled = false;

        // 0.0s: バス停と乗客を登場させる
        Debug.Log("ENDING: バス停が出現");
        if (busStopAndPassengers != null) busStopAndPassengers.SetActive(true);
        if (bus != null) bus.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        // 0.5s: プレイヤーをバスに乗車させる（座席に移動させる）
        Debug.Log("ENDING: プレイヤーがバスに乗車");
        if (player != null && playerSeatPosition != null)
        {
            player.position = playerSeatPosition.position;
            player.rotation = playerSeatPosition.rotation;
        }

        yield return new WaitForSeconds(0.5f); // 累計1.0s

        // 1.0s: バスの走行開始
        Debug.Log("ENDING: バスが走行開始");
        if (SoundManager.Instance != null) SoundManager.Instance.PlayBusSound();
        // TODO: ここでバスを動かすアニメーションなどを再生

        yield return new WaitForSeconds(2.0f); // 累計3.0s

        // 3.0s: 画面を暗転
        Debug.Log("ENDING: 画面暗転");
        if (uiManager != null)
        {
            // FadeToBlackの完了を待つ想定 (コルーチンにするのが望ましい)
            uiManager.FadeToBlack(1.0f);
        }

        yield return new WaitForSeconds(1.0f); // フェード時間

        // 4.0s: エンドクレジット表示
        Debug.Log("ENDING: 「帰還エンド」表示");
        if (uiManager != null) uiManager.ShowEndingScreen();
    }
}
