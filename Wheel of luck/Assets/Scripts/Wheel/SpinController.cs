using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Wheel
{
    public class SpinController : MonoBehaviour
    {
        private const float FullSpinAngle = 360f;
        private const float WheelAngleStep = 45f;

        [SerializeField] private float fullSpinsCount;
        [SerializeField] private float spinDuration;
        [SerializeField] private AnimationCurve easeCurve;
        [SerializeField] private List<Transform> winRewardPositions;
        [SerializeField] private List<Transform> rewardPositions;

        public List<Transform> RewardPositions => rewardPositions;

        public event Action<Transform> SpinFinished;

        private Sequence _spinSequence;
        private Transform _currentWinReward;
        private List<Transform> _winRewardsList;

        private void Awake() => 
            _winRewardsList = winRewardPositions;

        public void Spin()
        {
            _spinSequence = DOTween.Sequence();

            var reward = GetReward();
            var spinAngle = GetSpinAngle(reward.transform.eulerAngles.z);
            var currentRotation = transform.eulerAngles.z; 
            var totalSpinAngle = spinAngle - currentRotation;

            _spinSequence.Append(transform.DORotate(new Vector3(0f, 0f, -totalSpinAngle), spinDuration, RotateMode.FastBeyond360).SetEase(easeCurve))
                .OnComplete(() => SpinFinished?.Invoke(reward));
            
            _spinSequence.Play();
        }

        private float GetSpinAngle(float rewardPosition)
        {
            var chosenAngle = fullSpinsCount * FullSpinAngle + rewardPosition;
            var spinAngle = Mathf.Round(chosenAngle / WheelAngleStep) * WheelAngleStep;

            return spinAngle;
        }

        private Transform GetReward()
        {
            if (_winRewardsList.Count == 0)
                return rewardPositions[GetNextIndex()];

            _currentWinReward = _winRewardsList[0].transform;
            _winRewardsList.Remove(_currentWinReward);
            return _currentWinReward;
        }

        private int GetNextIndex()
        {
            var seed = (int)System.DateTime.Now.Ticks;
            var pseudoRandom = new System.Random(seed);
            var currentIndex = pseudoRandom.Next(rewardPositions.Count);

            return currentIndex;
        }
    }
}
