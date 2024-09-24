using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace MusicSystem
{
    public class MusicController : MonoBehaviour
    {
        [SerializeField] private Image _currentMusicImage;
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private Sprite _musicOnImage;
        [SerializeField] private Sprite _musicOffImage;
        private string _musicMixerName = "Music";
        private int _musicIsOn; 

        private const int VolumeOn = 0;
        private const int VolumeOff = -80;

        private void Start()
        {
            _musicIsOn = PlayerPrefs.GetInt("MusicIsOn"); 
            ChangeVolume(_musicIsOn, _musicMixerName, _currentMusicImage, 
                _musicOnImage, _musicOffImage);
        }

    public void ChangeMusicState()
    {
        if (_musicIsOn == 0)
        {
            PlayerPrefs.SetInt("MusicIsOn", 1);
            _musicIsOn = 1;
        }
        else
        {
            PlayerPrefs.SetInt("MusicIsOn", 0);
            _musicIsOn = 0;
        }
        ChangeVolume(_musicIsOn, _musicMixerName, _currentMusicImage,
            _musicOnImage, _musicOffImage);
    }

    private void ChangeVolume(int isOn, string mixerName, Image currentImage,
        Sprite onImage, Sprite offImage)
    {
        if (isOn == 0)
        {
            _mixer.SetFloat(mixerName, VolumeOn);
            currentImage.sprite = onImage;
        }
        else
        {
            _mixer.SetFloat(mixerName, VolumeOff);
            currentImage.sprite = offImage;
        }
    }
    }
}