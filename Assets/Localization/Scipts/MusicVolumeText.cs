using UnityEngine;
using UnityEngine.Localization;
using TMPro;

public class MusicVolumeText : MonoBehaviour
{
    [SerializeField] private LocalizedString localizedString;
    [SerializeField] private TextMeshProUGUI musicVolumeText;

    private float musicVolume;

    private void OnEnable()
    {
        localizedString.Arguments = new object[] { musicVolume };
        localizedString.StringChanged += UpdateText;
    }

    private void OnDisable()
    {
        localizedString.StringChanged -= UpdateText;
    }

    private void UpdateText(string value)
    {
        musicVolumeText.text = value;
    }

    public void ChangeMusicVolume(float value)
    {
        musicVolume = value;
        localizedString.Arguments[0] = musicVolume;
        localizedString.RefreshString();
    }
}