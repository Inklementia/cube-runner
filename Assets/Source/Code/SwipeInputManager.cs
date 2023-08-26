using UnityEngine;
using System;
using System.Collections.Generic;


namespace Source.Code
{
    public class SwipeInputManager : SingletonClass<SwipeInputManager>
    {
        private Vector2 _touchPosition;
        private Vector2 _swipeDelta;
        private bool _isTouched;
        
        const float MIN_SWIPE_DISTANCE = 125f;
        private Dictionary<SwipeDirection, bool> _swipeDirections = new Dictionary<SwipeDirection, bool>();

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
            if (IsTouchStarted)
            {
                _touchPosition = TouchPosition();
                _isTouched = true;
            }
            else if (IsTouchEnded)
            {
                Swipe();
                _isTouched = false;
            }

            CalcSwipeDistance();
            CheckSwipe();

        }

        private void Swipe()
        {
          if(_swipeDirections[SwipeDirection.Left])
              Debug.Log("Swipe Left");
          
            if(_swipeDirections[SwipeDirection.Right])
                Debug.Log("Swipe Right");
            
            if(_swipeDirections[SwipeDirection.Up])
                Debug.Log("Swipe Up");
            
            if(_swipeDirections[SwipeDirection.Down])
                Debug.Log("Swipe Down");
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
                float x = _swipeDelta.x;
                float y = _swipeDelta.y;
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    if (x < 0)
                    {
                        _swipeDirections[SwipeDirection.Left] = true;
                    }
                    else
                    {
                        _swipeDirections[SwipeDirection.Right] = true;
                    }
                }
                else
                {
                    if (y < 0)
                    {
                        _swipeDirections[SwipeDirection.Down] = true;
                    }
                    else
                    {
                        _swipeDirections[SwipeDirection.Up] = true;
                    }
                }
                
                Swipe();
            }
        }
        
        
        private void Reset()
        {
            _swipeDirections.Add(SwipeDirection.Left, false);
            _swipeDirections.Add(SwipeDirection.Right, false);
            _swipeDirections.Add(SwipeDirection.Up, false);
            _swipeDirections.Add(SwipeDirection.Down, false);
            _touchPosition = Vector2.zero;
            _swipeDelta = Vector2.zero;
            _isTouched = false;
            
        }
    }
}
