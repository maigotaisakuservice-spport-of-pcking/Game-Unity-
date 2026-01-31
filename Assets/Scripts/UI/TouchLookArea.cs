using UnityEngine;
using UnityEngine.EventSystems;

// このスクリプトは、タッチ可能なUI要素（透明なImageなど）にアタッチします。
public class TouchLookArea : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    // ドラッグによる入力値を格納するVector2
    public Vector2 LookInput { get; private set; }

    [Tooltip("毎フレーム入力をリセットするかどうか")]
    public bool resetInputOnUpdate = true;

    public void OnPointerDown(PointerEventData eventData)
    {
        // ポインターが押されたときにドラッグを開始するために、何もしなくても良い
    }

    public void OnDrag(PointerEventData eventData)
    {
        // ドラッグ中のマウス/タッチの移動量（デルタ）を入力値として設定
        LookInput = eventData.delta;
    }

    private void LateUpdate()
    {
        // 毎フレーム入力をリセットしないと、指を離しても最後の入力が残り続けてしまう
        if(resetInputOnUpdate)
        {
            LookInput = Vector2.zero;
        }
    }
}
