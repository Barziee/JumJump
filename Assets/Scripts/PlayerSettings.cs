using UnityEngine;

[CreateAssetMenu (fileName ="Player_Settings", menuName = "ScriptableObjects")]
public class PlayerSettings : ScriptableObject
{
    [Tooltip("The Amount Added to the jump power each frame it added")]
    public float JumpAdder;

    [Tooltip("Player Movement Speed")]
    public float MoveSpeed;

    [Tooltip("Max Jump Height")]
    public float JumpHeight;
    
}