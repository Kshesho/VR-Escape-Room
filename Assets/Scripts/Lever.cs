using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{
    Animator _anim;
    string _animationParam = "LeverOn";

    private bool _isOn = false;

    [SerializeField] UnityEvent<bool> _onToggleLever;

    private void Awake()
    {
        _anim = GetComponentInParent<Animator>();
    }

    public void ToggleLever()
    {
        print("toggle lever");
        _isOn = !_isOn;

        if (_anim != null )
            _anim.SetBool(_animationParam, _isOn);

        _onToggleLever?.Invoke(_isOn);
    }
}
