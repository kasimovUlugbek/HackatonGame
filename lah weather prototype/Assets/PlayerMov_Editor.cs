using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerMovement))]
public class PlayerMov_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PlayerMovement movement = (PlayerMovement)target;
        Rigidbody rb = movement.GetComponent<Rigidbody>();

        rb.drag = movement.normalDrag;
    }
}
