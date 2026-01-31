using UnityEngine;

public class StairsTrigger : MonoBehaviour
{
    public enum Direction
    {
        Up,
        Down
    }

    [Tooltip("この階段が上りか下りか")]
    public Direction stairDirection = Direction.Up;

    private void OnTriggerEnter(Collider other)
    {
        // トリガーに入ったのがプレイヤーか確認
        if (other.CompareTag("Player"))
        {
            // 追跡中の女子高生がいれば、追跡を停止させる
            JkController jkController = FindObjectOfType<JkController>();
            if (jkController != null && jkController.currentState == JkController.JkState.Chasing)
            {
                jkController.StopChase();
            }

            // GameManagerにプレイヤーの選択を通知
            GameManager.Instance.PlayerChoseStairs(stairDirection);
        }
    }
}
