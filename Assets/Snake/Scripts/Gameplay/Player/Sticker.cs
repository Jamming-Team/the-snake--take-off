using System;
using Snake;
using UnityEngine;
using XTools;

public class Sticker : MonoBehaviour
{
    [SerializeField] private string playerLayerName = "Player";
    [SerializeField] private bool stopMovingPlayer = false;
    [SerializeField] float deltaMod = 10f;
    
    private Transform _player;
    private Vector3 _lastPosition;
    

    EventBinding<JumpPressed> _jumpPressedBinding;
    
    
    private void Start()
    {
        _lastPosition = transform.position;
    }

    void OnEnable() {
        _jumpPressedBinding = new EventBinding<JumpPressed>(SetBool);
        EventBus<JumpPressed>.Register(_jumpPressedBinding);

    }

    void OnDisable() {
        EventBus<JumpPressed>.Deregister(_jumpPressedBinding);
    }

    void SetBool() {
        stopMovingPlayer = true;
    }



    private void FixedUpdate()
    {
        if (_player != null && !stopMovingPlayer)
        {
            Debug.Log("STICKER Check");
            Vector3 delta = transform.position - _lastPosition;
            _player.position += delta;
        }

        _lastPosition = transform.position;
    }

    void OnTriggerStay(Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer(playerLayerName)) {
            stopMovingPlayer = false;
            _player = other.transform;
        }
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.layer == LayerMask.NameToLayer(playerLayerName)) {
    //         stopMovingPlayer = false;
    //         _player = other.transform;
    //     }
    // }
    //
    // private void OnTriggerExit(Collider other)
    // {
    //     if (_player != null && other.transform == _player)
    //     {
    //         _player = null;
    //     }
    // }
}