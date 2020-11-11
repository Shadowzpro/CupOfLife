using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider sliderSensitivity;
    public Slider sliderVolume;
    void Start()
    {
        //sliderSensitivity.value = playerCharacterController.m_MouseLook.XSensitivity;
        //sliderSensitivity.value = playerCharacterController.m_MouseLook.YSensitivity;

        AudioListener.volume = 1.0f;
        sliderVolume.value = AudioListener.volume;
    }

    void Update()
    {
        //playerCharacterController.m_MouseLook.XSensitivity = sliderSensitivity.value;
        //playerCharacterController.m_MouseLook.YSensitivity = sliderSensitivity.value;

        AudioListener.volume = sliderVolume.value;
    }
}