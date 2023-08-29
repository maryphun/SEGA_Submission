using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float _moveSpeed = 1.0f;

    [Header("AnimationClipReferences")]
    [SerializeField] private AnimationClip _anim_idle;
    [SerializeField] private AnimationClip _anim_moveForward;

    [Header("Debug")]
    [SerializeField] private Vector2 _moveDir = Vector2.zero;
    [SerializeField] private Animator _animator;


    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        Debug.Assert(_animator == null);
    }

    // Update is called once per frame
    void Update()
    {
        // 入力関連
        _moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // 移動関連
        Vector3 originPos = transform.position;
        Vector3 moveVector = (transform.forward * _moveDir.y) + (transform.right * _moveDir.x);
        Vector3 newPos = originPos + (moveVector * Time.deltaTime * _moveSpeed);

        if (originPos != newPos)
        {
            transform.position = newPos;
        }

        // アニメーション関連
        if (originPos == newPos)
        {
            _animator.Play("N_idle");
        }
        else if (_moveDir.y > 0) // 前に前進
        {
            _animator.Play("walk_strafe_front");
        }
        else if (_moveDir.y < 0)
        {
            _animator.Play("walk_strafe_back");
        }
    }
}
