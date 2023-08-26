using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;


namespace Source.Code
{
    public class SwipeInputManager : SingletonClass<SwipeInputManager>
    {
        private Vector2 _touchPosition;
        private Vector2 _swipeDelta;
        private bool _isTouched;
        
        const float MIN_SWIPE_DISTANCE = 125f;
        
        private bool[] _swipeDirections = new bool[4];
        
        
        public delegate void MoveDelegate(bool[] directions);
        public MoveDelegate OnMove;
        
        public delegate void ClickDelegate(Vector2 pos);
        public ClickDelegate OnClick;
        

        private Vector2 TouchPosition() => Input.mousePosition;

        private bool IsTouchStarted => Input.GetMouseButtonDown(0);
        private bool IsTouchEnded => Input.GetMouseButtonUp(0);
        private bool IsTouched => Input.GetMouseButton(0);


        private void Awake()
        {
            Reset();
        }
        private void Update()
        {
            if(EventSystem.current.IsPointerOverGameObject()) return;
            
            if (IsTouchStarted)
            {
                _touchPosition = TouchPosition();
                _isTouched = true;
            }
            else if (IsTouchEnded && _isTouched)
            {
                Swipe();
                _isTouched = false;
            }

            CalcSwipeDistance();
            CheckSwipe();

        }

        private void Swipe()
        {
            if(_swipeDirections[0] || _swipeDirections[1] || _swipeDirections[2] || _swipeDirections[3])
            {
                OnMove?.Invoke(_swipeDirections);
                Debug.Log("Swipe" );
            }
            else
            {
                OnClick?.Invoke(TouchPosition());
                Debug.Log("Click");
            }
         
        }

        private void CalcSwipeDistance()
        {
            _swipeDelta = Vector2.zero;
            if (_isTouched && IsTouched)
            {
                _swipeDelta = TouchPosition() - _touchPosition;
            }
        }

        private void CheckSwipe()
        {
            if(_swipeDelta.magnitude > MIN_SWIPE_DISTANCE)
            {
                if (Mathf.Abs(_swipeDelta.x) > Mathf.Abs(_swipeDelta.y))
                {
                      _swipeDirections[(int) SwipeDirection.Left] = (_swipeDelta.x < 0);
                                    _swipeDirections[(int) SwipeDirection.Right] = (_swipeDelta.x > 0);
                }else
                {
                    _swipeDirections[(int) SwipeDirection.Up] = (_swipeDelta.y > 0);
                    _swipeDirections[(int) SwipeDirection.Down] = (_swipeDelta.y < 0);
                }
                Swipe();
            }
        }
        
        
        private void Reset()
        {
            
            _touchPosition = Vector2.zero;
            _swipeDelta = Vector2.zero;
            _isTouched = false;
            for(int i =0; i < _swipeDirections.Length; i++)
            {
                _swipeDirections[i] = false;
            }
        }
    }
}
