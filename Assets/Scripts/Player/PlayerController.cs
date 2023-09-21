using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float _moveSpeed = 1.0f;

    [Header("Debug")]
    [SerializeField] private Vector2 _moveDir = Vector2.zero;
    [SerializeField] private Vector2 _lastMousePosition = Vector2.zero;
    [SerializeField] private Vector3 _animationMoveDir = Vector2.zero;
    [SerializeField] private Animator _animator;

    [SerializeField] public bool IsGrounded { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        Debug.Assert(_animator != null);

        IsGrounded = true;
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
        _animationMoveDir = Vector3.MoveTowards(_animationMoveDir, moveVector, 5.0f * Time.deltaTime);
        _animator.SetFloat("Vertical", _animationMoveDir.z);
        _animator.SetFloat("Horizontal", _animationMoveDir.x);
    }
}
