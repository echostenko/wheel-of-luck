using System.Collections.Generic;
using Factory;
using Reward;
using UnityEngine;
using UnityEngine.UI;

namespace Wheel
{
    public class WheelView : MonoBehaviour
    {
        [SerializeField] private Button spinButton;
        [SerializeField] private SpinController spinController;
        [SerializeField] private List<RewardType> rewardTypes;

        private List<RewardBehaviour> _rewardBehaviours = new();
        private Dictionary<Transform, RewardBehaviour> _rewards = new ();
        private RewardBehaviour _rewardBehaviour;

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
            reward.UpdateReward(false);
            reward.SetRewardType(RewardType.Item);
            reward.UpdateReward(true);
        }

        private void SpinWheel() => 
            spinController.Spin();

        private void SetRewards()
        {
            var index = 0;
            
            foreach (var rewardPosition in spinController.RewardPositions)
            {
                _rewardBehaviour = RewardFactory.CreateReward(rewardPosition, rewardTypes[index]);
                _rewards.Add(rewardPosition, _rewardBehaviour);
                _rewardBehaviours.Add(_rewardBehaviour);
                index++;
            }
        }
    }
}
