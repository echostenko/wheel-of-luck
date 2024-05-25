using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WheelView : MonoBehaviour
{
        private const float FullSpinAngle = 360f;
        private const float WheelAngleStep = 45f;

        [SerializeField] private float fullSpinsCount;
        [SerializeField] private float spinDuration;
        [SerializeField] private AnimationCurve easeCurve;
        [SerializeField] private Transform wheelTransform;
        [SerializeField] private Button spinButton;
        [SerializeField] private List<Transform> rewardPositions;

        private Sequence _spinSequence;

        private void Awake() => 
            spinButton.onClick.AddListener(Spin);

        private void OnDestroy() => 
            spinButton.onClick.RemoveListener(Spin);

        private void Spin()
        {
            _spinSequence = DOTween.Sequence();

            var spinAngle = GetSpinAngle(GetRandomRewardPosition());

            _spinSequence.Append(wheelTransform.DORotate(new Vector3(0f, 0f, -spinAngle), spinDuration, RotateMode.FastBeyond360).SetEase(easeCurve));

            _spinSequence.Play();
        }

        private float GetSpinAngle(Vector3 rewardPosition)
        {
            var angleDifference = Quaternion.FromToRotation(Vector3.up, rewardPosition).eulerAngles.z;
            var chosenAngle = fullSpinsCount * FullSpinAngle + angleDifference;
            var spinAngle = Mathf.Round(chosenAngle / WheelAngleStep) * WheelAngleStep;

            return spinAngle;
        }

        private Vector3 GetRandomRewardPosition() => 
            rewardPositions.Count == 0 ? Vector3.zero : rewardPositions[Random.Range(0, rewardPositions.Count)].position;
}
