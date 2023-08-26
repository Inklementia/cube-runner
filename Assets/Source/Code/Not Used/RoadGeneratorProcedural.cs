using System.Collections.Generic;
using UnityEngine;

namespace Source.Code.Not_Used
{
	public class RoadGeneratorProcedural : MonoBehaviour {

		[SerializeField] private GameObject roadSegmentPrefab;
		[SerializeField] private float segmentLength = 15;
		[SerializeField] private int activeSegmentsCount = 3;
		[SerializeField] private int maxSegmentsCount = 10;
		[SerializeField] private Transform player;

		private Queue<Transform> _segments;

		// reference to chunk that the player is on
		private Transform _currentSegment;
		private int _currentSegmentIndex = 0;
		private int _currentSegmentPosition = 0;

		private void Awake()
		{
			InitializeSegementsList();
		}

		private void InitializeSegementsList()
		{
			_segments = new Queue<Transform>();
			for (int i = 0; i < maxSegmentsCount; i++)
			{
				GameObject _segment = Instantiate<GameObject>(roadSegmentPrefab);
				_segment.transform.position = NextSegmentPosition();
				if (i != 0)
					_segment.SetActive(false);
				_segments.Enqueue(_segment.transform);
			}
		}

	
		private void FixedUpdate () {

			if (!player) return;

		
			// determine the chunk that the player is on
			_currentSegment = GetCurrentSegment();
			_currentSegmentIndex = GetIndexOfCurrentSegment();

			// Manage chunks based on current chunk that the player is on
			for (int i = _currentSegmentIndex; i < (_currentSegmentIndex+activeSegmentsCount); i++)
			{
				i = Mathf.Clamp(i, 0, _segments.Count-1);
				GameObject _segmentGo = (_segments.ToArray()[i]).gameObject;
				if (!_segmentGo.activeInHierarchy)
					_segmentGo.SetActive(true);
			}

			if (_currentSegmentIndex > 0)
			{
				float _distance = Vector3.Distance(player.position, (_segments.ToArray()[_currentSegmentIndex - 1]).position);
				if(_distance > (segmentLength * .75f))
					SweepPreviousChunk();
			}

		}

		private void SweepPreviousChunk()
		{
			Transform _segment = _segments.Dequeue();
			_segment.gameObject.SetActive(false);
			_segment.position = NextSegmentPosition();
			_segments.Enqueue(_segment);
		}

		private Vector3 NextSegmentPosition()
		{
			float _position = _currentSegmentPosition;
			_currentSegmentPosition += (int)segmentLength;
			return new Vector3(0, 0, _position);
		} 

		private Transform GetCurrentSegment()
		{
			Transform current= null;
			foreach (Transform c in _segments)
			{
				if (Vector3.Distance(player.position, c.position) <= (segmentLength / 2))
				{
					current = c;
					break;
				}
			}
			return current;
		}

		private int GetIndexOfCurrentSegment()
		{
			int index = -1;
			for (int i = 0; i < _segments.Count; i++)
			{
				if ((_segments.ToArray()[i]).Equals(_currentSegment))
				{
					index = i;
					break;
				}
			}
			return index;
		}

	}
}