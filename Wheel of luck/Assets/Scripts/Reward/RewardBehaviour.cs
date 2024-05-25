using UnityEngine;

namespace Reward
{
    public class RewardBehaviour : MonoBehaviour
    {
        public void Enable(bool isEnable) => 
            gameObject.SetActive(isEnable);
    }
}
