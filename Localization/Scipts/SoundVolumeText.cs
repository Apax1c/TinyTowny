using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class SoundVolumeText : MonoBehaviour
{
    [SerializeField] private LocalizedString localizedString;
    [SerializeField] private TextMeshProUGUI soundVolumeText;

    private float soundVolume;

    private void OnEnable()
    {
        localizedString.Arguments = new object[] { soundVolume };
        localizedString.StringChanged += UpdateText;
    }

    private void OnDisable()
    {
        localizedString.StringChanged -= UpdateText;
    }

    private void UpdateText(string value)
    {
        soundVolumeText.text = value;
    }

    public void ChangeSoundVolume(float value)
    {
        soundVolume = value;
        localizedString.Arguments[0] = soundVolume;
        localizedString.RefreshString();
    }
}
