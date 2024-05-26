using System.Collections.Generic;
using Package.Scripts.Factory;
using Package.Scripts.Reward;
using UnityEngine;
using UnityEngine.UI;

namespace Package.Scripts.Wheel
{
    public class WheelView : MonoBehaviour
    {
        [SerializeField] private Button spinButton;
        [SerializeField] private SpinController spinController;
        [SerializeField] private List<RewardType> rewardTypes;

        private List<RewardBehaviour> _rewardBehaviours = new();
        private Dictionary<Transform, RewardBehaviour> _rewards = new ();
        private RewardBehaviour _rewardBehaviour;
        private int _index;

        private void Awake()
        {
            spinButton.onClick.AddListener(SpinWheel);
            spinController.SpinFinished += OnSpinFinished;
            SetRewards();
        }
        
        private void OnDestroy()
        {
            spinController.SpinFinished -= OnSpinFinished;
            spinButton.onClick.RemoveListener(SpinWheel);
        }

        private void OnSpinFinished(Transform rewardTransform)
        {
            var reward = _rewards[rewardTransform];

            ResetIndex();
            reward.UpdateReward(rewardTypes[_index]);
            _index++;

            ButtonInteractable(true);
        }

        private void SpinWheel()
        {
            ButtonInteractable(false);
            spinController.Spin();
        }

        private void ButtonInteractable(bool interactable) => 
            spinButton.interactable = interactable;

        private void SetRewards()
        {
            foreach (var rewardPosition in spinController.RewardPositions)
            {
                ResetIndex();
                
                _rewardBehaviour = RewardFactory.CreateReward(rewardPosition, rewardTypes[_index]);
                _rewards.Add(rewardPosition, _rewardBehaviour);
                _rewardBehaviours.Add(_rewardBehaviour);
                _index++;
            }
        }

        private void ResetIndex()
        {
            if (_index >= rewardTypes.Count)
                _index = 0;
        }
    }
}
