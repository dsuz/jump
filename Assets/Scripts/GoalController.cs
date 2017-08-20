using UnityEngine;
using System.Collections;

/// <summary>
/// ゴールとなるアイテムに追加するスクリプト
/// </summary>
[RequireComponent(typeof(Collider), typeof(AudioSource), typeof(SpriteRenderer))]
public class GoalController : MonoBehaviour {
    /// <summary>ステージクリアをコントロールするオブジェクト</summary>
    [SerializeField] private StageClearController _stage;
    /// <summary>アイテム取得音</summary>
    [SerializeField] private AudioClip _itemGetsound;
    /// <summary>このオブジェクトのオーディオソース</summary>
    private AudioSource _audioSrc;
    /// <summary>このオブジェクトのレンダラー。アイテム取得時にアイテムを消すために使う</summary>
    private SpriteRenderer _renderer;

    void Start()
    {
        _audioSrc = GetComponent<AudioSource>();
        _renderer = GetComponent<SpriteRenderer>();

        if (_stage == null)
            Debug.LogError("Stage must be set in inspector: " + gameObject.name);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
            if (_renderer.enabled)
                GetItem();
    }

    void GetItem()
    {
        _renderer.enabled = false;

        if (_itemGetsound != null)
        {
            _audioSrc.PlayOneShot(_itemGetsound);
            Destroy(gameObject, _itemGetsound.length);
        }
        else
        {
            Debug.LogError("Item Get Sound must be set in inspector: " + gameObject.name);
            Destroy(gameObject);
        }

        _stage.Clear();
    }
}