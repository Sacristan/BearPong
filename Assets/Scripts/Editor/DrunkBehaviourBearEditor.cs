using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DrunkBehaviourBear))]
public class DrunkBehaviourBearEditor : Editor
{
    private DrunkBehaviourBear _Ref;
    private float _Drunkenness = 0f;

    public void OnEnable()
    {
        _Ref = (DrunkBehaviourBear)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.Label("Drunkenness 0 to 1");
        _Drunkenness = GUILayout.HorizontalSlider(_Drunkenness, 0, 1);
        if (GUILayout.Button("Apply"))
        {
            _Ref.Drunkenness = _Drunkenness;
        }
    }
}
