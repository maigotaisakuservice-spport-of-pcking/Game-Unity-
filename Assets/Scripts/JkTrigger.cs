using UnityEngine;

public class JkTrigger : MonoBehaviour
{
    private JkController jkController;
    private bool hasBeenTriggered = false;

    private void Start()
    {
        jkController = FindObjectOfType<JkController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasBeenTriggered && jkController != null)
        {
            // この階で一度だけトリガーされるようにする
            hasBeenTriggered = true;
            jkController.OnPlayerPasses();
        }
    }

    // 新しい階になったらリセットする
    public void ResetTrigger()
    {
        hasBeenTriggered = false;
    }
}
