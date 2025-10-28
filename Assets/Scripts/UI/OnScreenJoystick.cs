using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnScreenJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("UI References")]
    [Tooltip("ジョイスティックの背景画像")]
    public Image joystickArea;
    [Tooltip("ジョイスティックの操作部分の画像")]
    public Image joystickHandle;

    // ジョイスティックの入力値を格納するVector2
    public Vector2 InputDirection { get; private set; }

    private Vector2 joystickAreaOriginalPos;
    private Vector2 joystickHandleOriginalPos;

    void Start()
    {
        if (joystickArea == null || joystickHandle == null)
        {
            Debug.LogError("OnScreenJoystickのUI参照が設定されていません。");
            return;
        }

        // 初期位置を保存
        joystickAreaOriginalPos = joystickArea.rectTransform.anchoredPosition;
        joystickHandleOriginalPos = joystickHandle.rectTransform.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // タッチした位置にジョイスティックを移動させる（オプション）
        // joystickArea.rectTransform.position = eventData.position;
        OnDrag(eventData); // ドラッグ処理を呼び出してハンドルを更新
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickArea.rectTransform, eventData.position, eventData.pressEventCamera, out localPoint))
        {
            float areaRadius = joystickArea.rectTransform.sizeDelta.x / 2;

            // ジョイスティックの移動範囲を円形に制限
            if (localPoint.magnitude > areaRadius)
            {
                localPoint = localPoint.normalized * areaRadius;
            }

            // ハンドルの位置を更新
            joystickHandle.rectTransform.anchoredPosition = localPoint;

            // 入力方向を正規化して保存
            InputDirection = localPoint / areaRadius;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // タッチを離したらハンドルと入力をリセット
        joystickHandle.rectTransform.anchoredPosition = joystickHandleOriginalPos;
        InputDirection = Vector2.zero;

        // ジョイスティックエリアを元の位置に戻す（オプション）
        // joystickArea.rectTransform.anchoredPosition = joystickAreaOriginalPos;
    }
}
