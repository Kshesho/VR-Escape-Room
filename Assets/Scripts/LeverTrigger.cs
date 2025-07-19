using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class LeverTrigger : MonoBehaviour
{
    IXRSelectInteractor _currentInteractor;
    XRBaseInteractable _leverInteractable;

    [Header("Lever Angle Thresholds")]
    [SerializeField] float _upAngleThreshold = 69f;
    [SerializeField] float _downAngleThreshold = 55f;

    [Header("Events")]
    [SerializeField] UnityEvent _onLeverUp;
    [SerializeField] UnityEvent _onLeverDown;

    bool _isUp = false;

    void Awake()
    {
        _leverInteractable = GetComponent<XRBaseInteractable>();
    }

    void Update()
    {
        float angle = transform.localEulerAngles.x;

        // Normalize angle to -180 to 180
        if (angle > 180) angle -= 360;

        if (!_isUp && angle > _upAngleThreshold)
        {
            //Set to the up position
            transform.localRotation = Quaternion.Euler(135f, 0, 0);
            //Stop grabbing
            _leverInteractable.interactionManager.SelectExit(_currentInteractor, _leverInteractable);
            _currentInteractor = null;
            _isUp = true;
            _onLeverUp.Invoke();
        }
        else if (_isUp && angle < _downAngleThreshold)
        {
            //Set to the down position
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            //stop grabbing
            _leverInteractable.interactionManager.SelectExit(_currentInteractor, _leverInteractable);
            _currentInteractor = null;
            _isUp = false;
            _onLeverDown.Invoke();
        }
    }

    private void OnEnable()
    {
        _leverInteractable.selectEntered.AddListener(OnGrab);
        _leverInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnDisable()
    {
        _leverInteractable.selectEntered.RemoveListener(OnGrab);
        _leverInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        _currentInteractor = args.interactorObject;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        _currentInteractor = null;
    }

}

