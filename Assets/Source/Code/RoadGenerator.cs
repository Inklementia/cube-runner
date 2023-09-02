using System;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Code
{
    public class RoadGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject roadSegmentPrefab;
        [SerializeField] private float segmentLength = 10f;
        [SerializeField] private float scrollSpeed = 5f;
        [SerializeField] private int maxSegmentsCount = 5;
    
        private  List<GameObject> _segments = new List<GameObject>();
        private float _currentScrollSpeed;
        
 
        private void Awake()
        {
            ResetLevel();
            //StartLevel();
        }
        
        private void OnEnable()
        {
            GameActions.Instance.OnGameStarted += StartLevel;
            GameActions.Instance.OnGameStopped += ResetLevel;
        }
    
        private void OnDisable()
        {
            GameActions.Instance.OnGameStarted -= StartLevel;
            GameActions.Instance.OnGameStopped -= ResetLevel;
        }

        private void FixedUpdate()
        {
            if (_currentScrollSpeed == 0) return;
            foreach (GameObject road in _segments)
            {
                road.transform.position -= new Vector3(0, 0, scrollSpeed * Time.fixedDeltaTime);
            }
        
            if (_segments[0].transform.position.z < -segmentLength )
            {
                Destroy(_segments[0]);
                _segments.RemoveAt(0);
                CreateNextSegment();
            }
        }

        private void StartLevel()
        {
            _currentScrollSpeed = scrollSpeed;
            SwipeInputManager.Instance.enabled = true;
        }
    
        private void CreateNextSegment()
        {
            Vector3 pos = Vector3.zero;
            if (_segments.Count > 0)
            {
                pos = _segments[_segments.Count - 1].transform.position + new Vector3(0, 0, segmentLength);
            }
        
            GameObject roadGo = Instantiate(roadSegmentPrefab, pos, Quaternion.identity);
            roadGo.transform.SetParent(transform);
            _segments.Add(roadGo);
        }

        private void ResetLevel()
        {
            SwipeInputManager.Instance.enabled = false;
            _currentScrollSpeed = 0;
            while (_segments.Count > 0)
            {
                Destroy(_segments[0]);
                _segments.RemoveAt(0);
            }
        
            for (int i = 0; i < maxSegmentsCount; i++)
            {
                CreateNextSegment();
            }
        }
    }
}
