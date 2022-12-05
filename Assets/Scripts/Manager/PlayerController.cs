using BaseTemplate.Behaviours;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody _rb;
    [SerializeField] int _speed, _rotateSpeed, _jumpForce;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] ParticleSystem _nitroFX;
    public UIManager _UIManager;
    public Runner Runner;

    [SerializeField] List<WheelCollider> _wheelColliders;

    RaceInput _raceInput;

    int _maxSpeed;
    bool _isAccelerating, _isGrounded, _isRotating, _isJumping, _isNitro, _isRaceStarted;
    float _currentAccelerationValue, _currentRotationValue;
    ParticleSystem.EmissionModule em;

    public Rigidbody Rb { get => _rb; set => _rb = value; }

    public string PrefixPlayer = "P";
    public int PlayerID = 1;

    InputActionMap inputActionMap; 
    InputAction InputAction_jump, InputAction_respawn, InputAction_nitro;


    public void Init()
    {
        _raceInput = new RaceInput();
        _raceInput.Enable();

        inputActionMap =  _raceInput.asset.FindActionMap(PrefixPlayer + PlayerID);

        InputAction accelerate = inputActionMap.FindAction("Accelerate");

        accelerate.started += OnAcceleratePressed;
        accelerate.canceled += OnAcceleratePressed;
        accelerate.performed += OnAcceleratePressed;

        InputAction turn = inputActionMap.FindAction("Turn");

        turn.started += OnRotatePressed;
        turn.canceled += OnRotatePressed;
        turn.performed += OnRotatePressed;

        InputAction_jump = inputActionMap.FindAction("Jump");
        InputAction_respawn = inputActionMap.FindAction("Respawn");
        InputAction_nitro = inputActionMap.FindAction("Nitro");

        _maxSpeed = _speed;
        em = _nitroFX.emission;

        StartCoroutine(_UIManager.GameCanvas.DoStartCountdown());
        StartCoroutine(StartRun());
    }

    void OnAcceleratePressed(InputAction.CallbackContext context)
    {
        _currentAccelerationValue = context.ReadValue<float>();
        _isAccelerating = _currentAccelerationValue != 0;
    }

    void OnRotatePressed(InputAction.CallbackContext context)
    {
        _currentRotationValue = context.ReadValue<float>();
        _isRotating = _currentRotationValue != 0;
    }

    private void Update()
    {
        if (_isRaceStarted == false) return;


        if (InputAction_nitro.IsPressed())
        {
            em.enabled = true;
            _isNitro = true;
        }
        else
        {
            em.enabled = false;
            _isNitro = false;
        }

        if (InputAction_respawn.triggered)
        {
            RaceManager.Instance.RespawnAtLastCheckPoint(this);
        }

        if (InputAction_jump.triggered) _isJumping = true;
        else _isJumping = false;
    }

    private void FixedUpdate()
    {
        if (_isRaceStarted == false) return;

        if (_isNitro) _speed = _maxSpeed * 2;
        else _speed = _maxSpeed;

        _isGrounded = false;

        foreach (var wheel in _wheelColliders)
        {
            if (wheel.isGrounded)
            {
                _isGrounded = true;
            }
        }

        if (_isGrounded && _isAccelerating) _rb.velocity += transform.forward * _speed * Time.fixedDeltaTime * _currentAccelerationValue;


        if (_isGrounded && _isRotating)
        {
            var t = _rotateSpeed * 10f * _currentRotationValue * Time.fixedDeltaTime;
            transform.Rotate(transform.up * t);
        }

        if (_isGrounded && _isJumping)
        {
            _isGrounded = false;
            _rb.velocity += new Vector3(0, _jumpForce, 0);
        }


        _UIManager.GameCanvas.UpdateSpeedText(Mathf.RoundToInt(_rb.velocity.magnitude));

        _audioSource.volume = .5f + _rb.velocity.magnitude / 10;
        _audioSource.pitch = 1f + _rb.velocity.magnitude / 50;
    }

    IEnumerator StartRun()
    {
        _audioSource.Play();
        _audioSource.DOFade(.5f, 1);
        yield return new WaitForSeconds(3f);
        _isRaceStarted = true;
        _UIManager.GameCanvas._isRacing = true;
    }

    public void EndRace()
    {
        _isRaceStarted = false;

        _audioSource.DOFade(0, .3f).OnComplete(() => _audioSource.Stop());

        _UIManager.GameCanvas._isRacing = false;
    }
}
