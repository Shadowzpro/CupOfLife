using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Arm playerHand;
    public Slider horizontalSlider;
    public Slider verticalSlider;
    public Slider scrollSlider;
    public Slider sliderVolume;

    void Start()
    {
        horizontalSlider.value = playerHand.leftRightMovementSpeed;
        verticalSlider.value = playerHand.upDownSpeed;
        scrollSlider.value = playerHand.forwardBackMovementSpeed;

        AudioListener.volume = 1.0f;
        sliderVolume.value = AudioListener.volume;
    }

    void Update()
    {
        playerHand.leftRightMovementSpeed = horizontalSlider.value;
        playerHand.upDownSpeed = verticalSlider.value;
        playerHand.forwardBackMovementSpeed = scrollSlider.value;

        AudioListener.volume = sliderVolume.value;
    }
}