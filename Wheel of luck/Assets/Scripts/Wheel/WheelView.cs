using UnityEngine;
using UnityEngine.UI;

namespace Wheel
{
    public class WheelView : MonoBehaviour
    {
        [SerializeField] private Button spinButton;
        [SerializeField] private SpinController spinController;


        private void Awake()
        {
            spinButton.onClick.AddListener(SpinWheel);
        }

        private void OnDestroy() => 
            spinButton.onClick.RemoveListener(SpinWheel);

        private void SpinWheel() => 
            spinController.Spin();
    }
}
