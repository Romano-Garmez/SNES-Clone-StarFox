using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //how often to change themes
    public int secondsUntilThemeChange = 15;
    public List<levelTheme> LevelThemes = new();

    //object to change terrain material of
    public GameObject groundObject;

    private float timer = 0;
    private int index = 0;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        index++;
        //if index too high, set back to zero
        if (index == LevelThemes.Count)
        {
            index = 0;
        }
        //after "secondsUntilThemeChange" has elapsed, set skybox and ground material to that of next theme
        if (timer > secondsUntilThemeChange)
        {
            timer = 0;
            groundObject.GetComponent<MeshRenderer>().material = LevelThemes[index].groundMaterial;
            RenderSettings.skybox = LevelThemes[index].skybox;
        }

    }
}

//struct for creating game themes
[System.Serializable]
public struct levelTheme
{
    public Material skybox;
    public Material groundMaterial;

}