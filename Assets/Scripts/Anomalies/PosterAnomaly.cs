using UnityEngine;

// このスクリプトは、壁に配置されたポスターオブジェクトにアタッチされることを想定しています。
public class PosterAnomaly : MonoBehaviour, IAnomaly
{
    [Header("異変の種類")]
    [Tooltip("ポスターの目がプレイヤーを追いかける")]
    public GameObject movingEyesObject; // 例: 目の部分だけの別オブジェクト

    [Tooltip("ポスターから血が垂れるパーティクルエフェクトなど")]
    public ParticleSystem bloodDripsEffect;

    [Tooltip("ポスターから手などが飛び出す3Dモデル")]
    public GameObject extendingHandObject;

    private enum PosterAnomalyType
    {
        MovingEyes,
        BloodDrips,
        ExtendingHand
    }
    private PosterAnomalyType anomalyToTrigger;
    private bool isMovingEyesActive = false;
    private Transform playerCamera;

    private void Awake()
    {
        playerCamera = Camera.main?.transform;

        // 最初は全ての異変オブジェクトを非表示にしておく
        if(movingEyesObject != null) movingEyesObject.SetActive(false);
        if(bloodDripsEffect != null) bloodDripsEffect.Stop();
        if(extendingHandObject != null) extendingHandObject.SetActive(false);
    }

    public void Activate()
    {
        // 3種類のポスター異変の中からランダムに1つを選ぶ
        anomalyToTrigger = (PosterAnomalyType)Random.Range(0, 3);

        Debug.Log("ポスターの異変を開始: " + anomalyToTrigger.ToString());

        switch (anomalyToTrigger)
        {
            case PosterAnomalyType.MovingEyes:
                if (movingEyesObject != null)
                {
                    movingEyesObject.SetActive(true);
                    isMovingEyesActive = true;
                }
                break;
            case PosterAnomalyType.BloodDrips:
                // 例: 血が垂れるパーティクルを再生
                if (bloodDripsEffect != null)
                {
                    bloodDripsEffect.Play();
                }
                break;
            case PosterAnomalyType.ExtendingHand:
                // 例: 手のオブジェクトをアクティブにする
                if (extendingHandObject != null)
                {
                    extendingHandObject.SetActive(true);
                }
                break;
        }
    }

    public void Deactivate()
    {
        isMovingEyesActive = false;

        // 全ての異変をリセットする
        if (movingEyesObject != null) movingEyesObject.SetActive(false);
        if (bloodDripsEffect != null)
        {
            bloodDripsEffect.Stop();
            bloodDripsEffect.Clear();
        }
        if (extendingHandObject != null) extendingHandObject.SetActive(false);
    }

    private void Update()
    {
        if (isMovingEyesActive && movingEyesObject != null && playerCamera != null)
        {
            // 目のオブジェクトを常にプレイヤーのカメラに向ける
            movingEyesObject.transform.LookAt(playerCamera);
        }
    }
}
