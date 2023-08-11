using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Normal Attack")]
public class AttackScriptableObject : ScriptableObject
{
    public AnimatorOverrideController _animatorOV;
    public float duration = 1;
}
