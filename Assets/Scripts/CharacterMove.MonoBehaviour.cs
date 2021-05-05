using UnityEngine;

public partial class CharacterMove : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) => EvaluateCollision(ref other);
    private void Update() => UpdateCooldown();
}
