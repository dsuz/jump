using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

/// <summary>
/// プレイヤーをコントロールするクラス
/// </summary>
[RequireComponent(typeof(UnityStandardAssets._2D.PlatformerCharacter2D), typeof(AudioSource))]
public class PlayerMoveController : MonoBehaviour {
    /// <summary>このオブジェクトのStandard Assetsのクラスへの参照</summary>
    private UnityStandardAssets._2D.PlatformerCharacter2D _player;
    [SerializeField] private float _initialPlayerSpeed = 3.0f;
    /// <summary>この値が正の時は右に進む。負の時は左に進む</summary>
    private float _dirX = 1.0f;
    /// <summary>ジャンプ処理フラグ</summary>
    private bool _jump;
    /// <summary>このオブジェクトのオーディオソース</summary>
    private AudioSource _audioSrc;
    /// <summary>ジャンプ時に鳴らすオーディオクリップ</summary>
    [SerializeField] private AudioClip _jumpSound;

	void Start () {
        _player = GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>();
        _player.MaxSpeed = _initialPlayerSpeed;
        _audioSrc = GetComponent<AudioSource>();
	}
	
    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
            Jump();
    }

    void FixedUpdate()
    {
        _player.Move(_dirX, false, _jump);
        _jump = false;
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        SwitchDirection(col);
    }

    void OnCollisionStay2D(Collision2D col)
    {
        SwitchDirection(col);
    }

    /// <summary>
    /// 方向転換する
    /// </summary>
    /// <param name="col">このオブジェクトのコリジョン。強制的に方向転換したい時はnullでよい</param>
    void SwitchDirection(Collision2D col)
    {
        if (col != null)
        {
            Transform collisionParent = col.transform.parent;
            // 左右の壁に衝突したら方向転換する
            if (collisionParent.gameObject.name.Equals("RightWalls"))
                _dirX = -1.0f;
            else if (collisionParent.gameObject.name.Equals("LeftWalls"))
                _dirX = 1.0f;
        }
        else
            _dirX = _dirX * (-1);
    }

    /// <summary>
    /// ジャンプする
    /// </summary>
    public void Jump()
    {
        Debug.Log("Jump!");

        if (!_jump)
        {
            _audioSrc.PlayOneShot(_jumpSound);
            _jump = true;
        }
    }
}
