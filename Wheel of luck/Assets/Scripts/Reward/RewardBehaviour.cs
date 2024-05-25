using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Reward
{
    public class RewardBehaviour : MonoBehaviour
    {
        private const string GemPath = "Gem";
        private const string CrownPath = "Coin";
        
        [SerializeField] private Image image;
        public RewardType RewardType { get; private set; }

        public void SetRewardType(RewardType rewardType)
        {
            RewardType = rewardType;
            SetImage();
        }

        private void SetImage()
        {
            var x = Resources.Load<Sprite>(RewardType == RewardType.Gem ? GemPath : CrownPath);
            image.sprite = x;
        }

        public void UpdateReward(bool isEnable)
        {
            if (isEnable)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0f); 
                image.DOFade(1f, 2f).SetDelay(1f);
            }
            else
            {
                image.DOFade(0f, 2f).SetDelay(1f);
            }
        }
    }
}
