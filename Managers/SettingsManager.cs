using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.Audio;

public static class SettingsManager 
{
    //---------------------------------------------------

    public static void sfxVolume(int index)
    {
        AudioSource[] sfx = GameObject.FindObjectOfType<AudioManager>().GetComponents<AudioSource>();
        float volume = (index * 1.0f) / 10;
        foreach(AudioSource s in sfx)
        {
            s.volume = volume;
        }
    }

    //---------------------------------------------------

    public static void setQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    //---------------------------------------------------

    public static void setFullscreen(int index)
    {
        bool fullScreen = index == 0 ? true : false;
        Screen.fullScreen = fullScreen;
    }

    //---------------------------------------------------

    public static void setResolution(int index)
    {
        Resolution resolution = Screen.resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    //---------------------------------------------------

    public static void autosave(int index)
    {
        LoadManager.currentSlot = index == 0 ? 0 : 4;
    }

    //-----------------------------------------------------------

    public static void saveSettings(int[] settings)
    {
        string path = Application.persistentDataPath + "/settings.data";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream f = new FileStream(path, FileMode.Create);
        formatter.Serialize(f, settings);
        f.Close();
    }

    //-----------------------------------------------------------

    public static int[] loadSettings()
    {
        string path = Application.persistentDataPath + "/settings.data";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream f = new FileStream(path, FileMode.Open);
        int[] settingsToLoad = formatter.Deserialize(f) as int[];

        if(settingsToLoad.Length!=5)
        {
            f.Close();
            return null;
        }

        f.Close();
        return settingsToLoad;
    }
    
    //-----------------------------------------------------------------

    public static int[] defaultSettings()
    {
        int[] settings = new int[MenuList.settingsOptions.Length];
        settings[0] = 10;
        settings[1] = 0;
        settings[2] = Screen.resolutions.Length - 1;
        settings[3] = 5;
        settings[4] = 0;
        return settings;
    }

    //-----------------------------------------------------------------

    public static void applySettings(int index, int option)
    {
        switch(index)
        {
            case 0:
                {
                    sfxVolume(option);
                    return;
                }
            case 1:
                {
                    setFullscreen(option);
                    return;
                   
                }
            case 2:
                {
                    setResolution(option);
                    return;
                    
                }
            case 3:
                {
                    setQuality(option);
                    return;
                }
            case 4:
                {
                    autosave(option);
                    return;
                }
        }
    }
}
