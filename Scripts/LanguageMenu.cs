using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageMenu : MonoBehaviour
{
    private bool active = false;

    private void Start()
    {
        int ID = PlayerPrefs.GetInt("LocaleKey", 0);
        ChangeLocale(ID);
    }

    public void ChangeLocale(int _localeID)
    {
        if (active == true)
            return;
        StartCoroutine(SetLocale(_localeID));
    }

    IEnumerator SetLocale(int _localeID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
        PlayerPrefs.SetInt("LocaleKey", _localeID);
        active = false;
    }
}
