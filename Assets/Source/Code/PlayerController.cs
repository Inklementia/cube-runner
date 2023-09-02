using System;
using UnityEngine;

namespace Source.Code
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float laneChangeSpeed = 17f;
        [SerializeField] private float laneOffset = 2.5f;
        [SerializeField] private float lerpDuration = 0.5f;

        private Animator _animator;
        private Vector3 _startPos;
        private Vector3 _targetPos;
        private Quaternion _startRot;
        private float timeElapsed;
        
        private static readonly int ToIdle = Animator.StringToHash("toIdle");
        private static readonly int ToRun = Animator.StringToHash("toRun");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _startPos = transform.position;
            _startRot = transform.rotation;
            
            _targetPos = _startPos;
        }

        private void OnEnable()
        {
            GameActions.Instance.OnGameStarted += Run;
            GameActions.Instance.OnGameStopped += Reset;
        }
    
        private void OnDisable()
        {
            GameActions.Instance.OnGameStarted -= Run;
            GameActions.Instance.OnGameStopped -= Reset;
        }


        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.A) && _targetPos.x > -laneOffset)
            {
                timeElapsed = 0;
                _targetPos = new Vector3(_targetPos.x - laneOffset, transform.position.y, transform.position.z);
            }

            if (Input.GetKeyDown(KeyCode.D) && _targetPos.x < laneOffset)
            {
                timeElapsed = 0;
                _targetPos = new Vector3(_targetPos.x + laneOffset, transform.position.y, transform.position.z);
            }
        }

        private void FixedUpdate()
        {
            if (timeElapsed < lerpDuration)
            {
                transform.position =
                    Vector3.Lerp(transform.position, _targetPos, (timeElapsed / lerpDuration));
                timeElapsed += Time.deltaTime;
            }
            else
            {
                transform.position = _targetPos;
            }
        }

        private void Run()
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            _animator.SetTrigger(ToRun);
        }
    
        private void Reset()
        {
            _animator.SetTrigger(ToIdle);
            transform.position = _startPos;
            transform.rotation = _startRot;
            
        }
    }
}
