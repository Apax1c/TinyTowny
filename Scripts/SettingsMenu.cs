using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Button soundEffectsVolumeButton;
    [SerializeField] private Button musicVolumeButton;
    [SerializeField] private SoundVolumeText soundVolumeText;
    [SerializeField] private MusicVolumeText musicVolumeText;

    private void Awake()
    {
        soundEffectsVolumeButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            soundVolumeText.ChangeSoundVolume(Mathf.Round(SoundManager.Instance.GetVolume() * 10f));
        });
        musicVolumeButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            musicVolumeText.ChangeMusicVolume(Mathf.Round(MusicManager.Instance.GetVolume() * 10f));
        });
    }

    private void Start()
    {
        soundVolumeText.ChangeSoundVolume(Mathf.Round(SoundManager.Instance.GetVolume() * 10f));
        musicVolumeText.ChangeMusicVolume(Mathf.Round(MusicManager.Instance.GetVolume() * 10f));
    }
}
