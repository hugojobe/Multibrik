using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(SoundPlayer))]
public class SoundsPlayerEditor : Editor
{
    public override void OnInspectorGUI(){
        
        SoundPlayer player = (SoundPlayer)target;

        //EditorGUI.MinMaxSlider(new Rect(0, 0, position.width, 20), -3f, 3f, -3f, 3f);

        base.OnInspectorGUI();
    }
}
