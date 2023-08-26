using System.Collections.Generic;
using UnityEngine;

namespace Source.Code.Not_Used
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject roadSegmentPrefab;
        [SerializeField] private float segmentLength = 15;
        [SerializeField] private int activeSegmentsCount = 3;
        [SerializeField] private int maxSegmentsCount = 10;
    
        [SerializeField] private float speed;

        private Queue<Transform> _segments;
        private List<GameObject> _allSegments = new List<GameObject>();

        // reference to chunk that the player is on
        private Transform _currentSegment;
        private int _currentSegmentIndex = 0;
        private int _currentSegmentPosition = 0;
        private float currentSpeed;

        private void Awake()
        {
            InitializeSegementsList();
        }

        private void Start()
        {
            currentSpeed = speed;
        }

        private void Update()
        {
            if (currentSpeed == 0) return;
            foreach (Transform segment in _segments)
            {
                segment.position -= new Vector3(0, 0, currentSpeed * Time.deltaTime);
            }

            if (_segments.Peek().position.z < -segmentLength)
            {
                Transform _segment = _segments.Dequeue();
                _segment.position = NextSegmentPosition();
                _segments.Enqueue(_segment);

                AddNewSegmentInFront();
            }
        }

        private void InitializeSegementsList()
        {
            _segments = new Queue<Transform>();
            for (int i = 0; i < maxSegmentsCount; i++)
            {
                GameObject _segment = Instantiate<GameObject>(roadSegmentPrefab);
                _segment.transform.position = NextSegmentPosition();
                _segment.transform.SetParent(transform);
                if (i >= activeSegmentsCount)
                    _segment.SetActive(false);
                _segments.Enqueue(_segment.transform);
                _allSegments.Add(_segment);
            }
        }
        private Vector3 NextSegmentPosition()
        {
            float _position = _currentSegmentPosition;
            _currentSegmentPosition += (int)segmentLength;
            return new Vector3(0, 0, _position);
        } 
    
        private void AddNewSegmentInFront()
        {
            //activate new segment
            GameObject _segment = _allSegments.Find(segment => !segment.activeInHierarchy);
            _segment.transform.position = NextSegmentPosition();
            _segment.SetActive(true);
            _segments.Enqueue(_segment.transform);
        
        }
    }
}   