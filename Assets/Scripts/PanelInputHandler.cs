using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

/// <summary>
/// 入力をコントロールする。画面全体を透明なパネルで覆い、クリック or タップでプレイヤーをジャンプさせる。
/// </summary>
public class PanelInputHandler : MonoBehaviour, IPointerClickHandler
{
    /// <summary>プレイヤーへの参照</summary>
    [SerializeField] PlayerMoveController _player;

    void Start()
    {
        if (_player == null)
            Debug.LogError("Player must be set in inspector: " + gameObject.name);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _player.Jump();
    }
}
