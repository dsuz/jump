using UnityEngine;
using System.Collections;

/// <summary>
/// 時間経過でプレイヤーの速度を変化させるクラス
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class PlayerSpeedController : MonoBehaviour
{
    /// <summary>速度を変化させるプレイヤーオブジェクトの参照</summary>
    [SerializeField] private UnityStandardAssets._2D.PlatformerCharacter2D _player;
    /// <summary>何秒おきに速度を変化させるか指定する変数</summary>
    [SerializeField] private float _speedupPeriod = 30.0f;
    /// <summary>速度をどれくらい変化させるか指定する変数</summary>
    [SerializeField] private float _speedupUnit = 1.0f;
    /// <summary>プレイヤーの移動速度が変化したらカメラの追従速度も変化させる必要がある。それを行うためのカメラへの参照</summary>
    [SerializeField] private UnityStandardAssets.Cameras.AutoCam _camera;
    /// <summary>時間経過を計上するカウンタ</summary>
    private float _speedupPeriodCounter;
    /// <summary>速度変化時に鳴らすオーディオクリップ</summary>
    [SerializeField] private AudioClip _speedupSound;
    /// <summary>このオブジェクトのオーディオソース</summary>
    private AudioSource _audioSrc;
    /// <summary>最大速度。これを超えると初期速度に戻る</summary>
    [SerializeField] private float _maxSpeed = 20.0f;
    /// <summary>初期速度</summary>
    private float _initialSpeed;
    /// <summary>速度が初期値に戻った時に鳴らすオーディオクリップ</summary>
    [SerializeField] AudioClip _speedDownSound;


    void Start()
    {
        _audioSrc = GetComponent<AudioSource>();

        if (_player == null)
            Debug.LogError("Player must be set in inspector: " + gameObject.name);
        else
            _initialSpeed = _player.MaxSpeed;

        if (_camera == null)
            Debug.LogError("Camera must be set in inspector: " + gameObject.name);
    }

    void Update()
    {
        _speedupPeriodCounter += Time.deltaTime;
        if (_speedupPeriodCounter > _speedupPeriod)
        {
            Speedup(_speedupUnit);
            _speedupPeriodCounter = 0f;
        }
    }

    /// <summary>
    /// プレイヤーの移動速度をアップする
    /// </summary>
    /// <param name="AddSpeed">アップさせる速度</param>
    void Speedup(float AddSpeed)
    {
        if (_player.MaxSpeed < _maxSpeed)
        {
            _audioSrc.PlayOneShot(_speedupSound);
            _player.MaxSpeed += AddSpeed;
            Debug.Log("Speedup! Current speed: " + _player.MaxSpeed);
        }
        else
        {
            _audioSrc.PlayOneShot(_speedDownSound);
            _player.MaxSpeed = _initialSpeed;
        }

        // カメラのスピードも変える
        _camera.MoveSpeed = _player.MaxSpeed - 1.0f;
    }
}
