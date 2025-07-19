using UnityEngine;
using UnityEngine.Events;

public class LightSwitch : MonoBehaviour
{
    Animator _anim;
    string _animationParam = "SwitchOn";

    private bool _isOn = false;

    [SerializeField] UnityEvent _onSwitchToggle;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("player touched");
            _isOn = !_isOn;

            // Trigger animation
            if (_anim != null)
            {
                _anim.SetBool(_animationParam, _isOn);
            }

            _onSwitchToggle?.Invoke();
        }
    }
}

