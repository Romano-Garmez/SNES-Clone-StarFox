using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class TextureFinder : MonoBehaviour
{
    [SerializeField, ReadOnly]
    private static List<Material> materials = new List<Material>();

    public static string materialsPath = "Assets/Models/sf64/Materials/";
    
    public static string texturesPath = "Assets/Models/sf64/Textures/";
    
    [Button]
    public static void FindTextures()
    {
        materials = new List<Material>();
        
        var matAddresses = AssetDatabase.FindAssets("t:material", new string[] {materialsPath});
        
        for(int i = 0; i < matAddresses.Length; i++)
        {
            //Debug.Log(matAddresses[i]);
            materials.Add(AssetDatabase.LoadAssetAtPath<Material>(AssetDatabase.GUIDToAssetPath(matAddresses[i])));
            //materials.Add(AssetDatabase.LoadAssetAtPath<Material>(matAddresses[i]));
        }

        var texAddresses = AssetDatabase.FindAssets("t:Texture2D", new string[] {texturesPath});
        
        for (int i = 0; i < materials.Count; i++)
        {
            //materials[i].SetTexture("_MainTex", AssetDatabase.LoadAssetAtPath<Texture2D>(texAddresses[i]));
            materials[i].mainTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(AssetDatabase.GUIDToAssetPath(texAddresses[i]));
        }
    }
}
