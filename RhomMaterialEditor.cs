using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RhomMaterialScript))]
public class RhomMaterialEditor : Editor { 
    public override void OnInspectorGUI()
    {
        RhomMaterialScript RMS = (RhomMaterialScript)target;

        RMS.shader = (ComputeShader)EditorGUILayout.ObjectField(RMS.shader, typeof(ComputeShader), true);

        if (GUILayout.Button("Find ComputeShader"))
        {
            RMS.FindComputeShader();
        }

        EditorGUILayout.LabelField("Textures");
        EditorGUILayout.BeginVertical("Box");
            EditorGUI.BeginChangeCheck();
                EditorGUILayout.Toggle("Albedo", true);
                RMS.metallic = EditorGUILayout.Toggle("Metallic", RMS.metallic);
                RMS.normalMap = EditorGUILayout.Toggle("Normal Map", RMS.normalMap);
                EditorGUILayout.Toggle("Height Map", true);
                RMS.oclussion = EditorGUILayout.Toggle("Oclussion", RMS.oclussion);
                RMS.emission = EditorGUILayout.Toggle("Emission", RMS.emission);
            if (EditorGUI.EndChangeCheck())
            {
                //event on checkboxes change
            }
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Layers");
            if (GUILayout.Button("Add Layer"))
            {
                RMS.AddLayer();
            }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginVertical("Window");
            for (int i = RMS.layers.Count - 1; i >= 0; i--)
            {
                EditorGUILayout.Space();
                EditorGUILayout.BeginVertical("Box");
                    EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Layer #" + i.ToString());
                        if (GUILayout.Button("Delete Layer"))
                        {
                            RMS.DeleteLayer(i);
                            return;
                        }
                    EditorGUILayout.EndHorizontal();
                    EditorGUI.BeginChangeCheck();
                        RMS.layers[i].material = (Material)EditorGUILayout.ObjectField(RMS.layers[i].material, typeof(Material), true);
                    if (EditorGUI.EndChangeCheck())
                    {
                        //event on material change
                    }
                    EditorGUI.BeginChangeCheck();
                        RMS.layers[i].offset = EditorGUILayout.IntSlider("Offset", RMS.layers[i].offset, -255, 255);
                        RMS.layers[i].feather = EditorGUILayout.IntSlider("Feather", RMS.layers[i].feather, 0, 255);
                    if (EditorGUI.EndChangeCheck())
                    {
                        //event on ofsset and feather change
                    }
                EditorGUILayout.EndVertical();
            }
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Save Materail"))
            {
                RMS.SaveMaterial();
            }
            RMS.materialPath = EditorGUILayout.TextField(RMS.materialPath);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.HelpBox("Some stuf", MessageType.Info, true);
    }
}
