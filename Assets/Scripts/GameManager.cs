using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int secondsUntilThemeChange = 15;
    public List<levelTheme> LevelThemes = new();
    private int index = 0;

    public GameObject groundObject;

    private float timer = 0;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        index++;
        if (index == LevelThemes.Count)
        {
            index = 0;
        }
        if (timer > secondsUntilThemeChange)
        {
            timer = 0;
            groundObject.GetComponent<MeshRenderer>().material = LevelThemes[index].groundMaterial;
            RenderSettings.skybox = LevelThemes[index].skybox;
        }

    }
}

[System.Serializable]
public struct levelTheme
{
    public Material skybox;
    public Material groundMaterial;

}