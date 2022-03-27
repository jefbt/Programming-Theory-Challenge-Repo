using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    [SerializeField] Material[] skyboxMaterials;

    public static int skyboxIndex { get; private set; } = 0;
    public static Material[] _skyboxMaterials { get; private set; }

    private void Awake()
    {
        _skyboxMaterials = skyboxMaterials;

        if (PlayerPrefs.HasKey("Skybox"))
        {
            skyboxIndex = PlayerPrefs.GetInt("Skybox");
        }

        ChangeSkybox(skyboxIndex);
    }

    public static void StaticChangeSkybox(int index)
    {
        skyboxIndex = index % _skyboxMaterials.Length;
        PlayerPrefs.SetInt("Skybox", skyboxIndex);
        SetSkybox();
    }

    public void ChangeSkybox(int index)
    {
        StaticChangeSkybox(index);
    }

    public static void SetSkybox()
    {
        if (_skyboxMaterials == null || _skyboxMaterials.Length == 0) return;
        RenderSettings.skybox = _skyboxMaterials[skyboxIndex];
    }
}
