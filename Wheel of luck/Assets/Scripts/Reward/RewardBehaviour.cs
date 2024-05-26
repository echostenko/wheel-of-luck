using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Reward
{
    public class RewardBehaviour : MonoBehaviour
    {
        private const string GemPath = "Gem";
        private const string CoinPath = "Coin";
        private const string HatPath = "Hat";
        
        [SerializeField] private Image image;
        public RewardType RewardType { get; private set; }

        public void SetRewardType(RewardType rewardType)
        {
            RewardType = rewardType;
            SetImage();
        }

        private void SetImage()
        {
            var path = RewardType switch
            {
                RewardType.Coin => CoinPath,
                RewardType.Gem => GemPath,
                _ => HatPath
            };

            var sprite = Resources.Load<Sprite>(path);
            image.sprite = sprite;
        }

        public void UpdateReward(RewardType rewardType)
        {
            StartFade(0, 2);
            
            RewardType = rewardType;
            SetImage();

            image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
            StartFade(1, 2);
        }

        private void StartFade(float endValue, float duration) => 
            image.DOFade(endValue, duration).SetDelay(1f);
    }
}
