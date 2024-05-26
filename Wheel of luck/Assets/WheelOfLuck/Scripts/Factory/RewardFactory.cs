using Package.Scripts.Reward;
using UnityEngine;

namespace Package.Scripts.Factory
{
    public static class RewardFactory
    {
        private const string Reward = "Reward";

        public static RewardBehaviour CreateReward(Transform parent, RewardType rewardType)
        {
            var reward = Resources.Load<RewardBehaviour>(Reward);
            reward.SetRewardType(rewardType);
            return Object.Instantiate(reward, parent);
        }
    }
}