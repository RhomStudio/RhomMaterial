using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class RhomMaterialScript : MonoBehaviour {
    public ComputeShader shader;
    public string materialPath = "Assets/RhomMaterial/Materials/Material#1";
    public List<Layer> layers = new List<Layer>();
    public bool albedo = true;
    public bool metallic = false;
    public bool normalMap = false;
    public bool heightMap = true;
    public bool oclussion = false;
    public bool emission = false;    

    private Renderer renderer;
    private RenderTexture result;

    public void SaveMaterial()
    {
        Material material = GetComponent<Renderer>().sharedMaterial;
        Texture _albedo = material.GetTexture("_MainTex");
        Texture _heightMap = material.GetTexture("_ParallaxMap");
        //other textures field
        AssetDatabase.CreateAsset(_albedo, materialPath + "_Albedo.tex");
        AssetDatabase.CreateAsset(_heightMap, materialPath + "_HeightMap.tex");
        //save other textures
        AssetDatabase.CreateAsset(material, materialPath + ".mat");
    }

    public void AddLayer()
    {
        layers.Add(new Layer());
    }

    public void DeleteLayer(int index)
    {
        layers.RemoveAt(index);
    }

    public void Initialization()
    {
        result = new RenderTexture(1024, 1024, 24);
        result.enableRandomWrite = true;
        result.Create();

        //texture1 = material1.GetTexture("_MainTex");
        //texture2 = material2.GetTexture("_MainTex");

        renderer = GetComponent<Renderer>();
        renderer.enabled = true;        
    }

    public void FindComputeShader()
    {
        string[] assetResults;
        assetResults = AssetDatabase.FindAssets("RhomComputeShader");        
        if (assetResults.Length > 0)
        {
            assetResults[0] = AssetDatabase.GUIDToAssetPath(assetResults[0]);
            shader = (ComputeShader)AssetDatabase.LoadAssetAtPath(assetResults[0], typeof(ComputeShader));
        }
        //"Assets/RhomMaterial"
        //RhomComputeShader
    }
}

public class Layer
{
    public Material material;
    public int offset;
    public int feather;
    Texture albedo;
    Texture metallic;
    Texture normalMap;
    Texture heightMap;
    Texture oclussion;
    Texture emission;

    public void Initialization()
    {
        if (albedo == true)
            albedo = material.GetTexture("_MainTex");
        if (metallic == true)
            metallic = material.GetTexture("_MetallicGlossMap");
        if (normalMap == true)
            normalMap = material.GetTexture("_BumpMap");
        if (heightMap == true)
            heightMap = material.GetTexture("_ParallaxMap");
        if (oclussion == true)
            oclussion = material.GetTexture("_OclussionMap");
        if (emission == true)
            emission = material.GetTexture("_EmissionMap");
    }
}
