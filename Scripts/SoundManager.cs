using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";

    [SerializeField] private GameObject camera;

    public static SoundManager Instance { get; private set; }

    private float volume = 1f;

    [SerializeField] private AudioClip buildingClip;
    [SerializeField] private AudioClip warningClip;

    private void Awake()
    {
        Instance = this;

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
    }

    private void Start()
    {
        if(PlaceForBuilding.Instance != null)
        {
            PlaceForBuilding.Instance.OnBuildingCreated += PlaceForBuilding_OnBuildingCreated;
        }
        if(BuildingBehavior.Instance != null)
        {
            BuildingBehavior.Instance.OnWarningCalled += BuildingBehavior_OnWarningCalled;
        }
    }

    private void PlaceForBuilding_OnBuildingCreated(object sender, System.EventArgs e)
    {
        PlaySound(buildingClip, camera.transform.position);
    }

    private void BuildingBehavior_OnWarningCalled(object sender, System.EventArgs e)
    {
        PlaySound(warningClip, camera.transform.position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    public void ChangeVolume()
    {
        volume += 0.1f;
        if (volume > 1f)
        {
            volume = 0f;
        }

        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
}