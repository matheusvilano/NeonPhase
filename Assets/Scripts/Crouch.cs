using UnityEngine;

public class Crouch : MonoBehaviour
 {
	 public bool isCrouched; // is the player pressing/holding down the crouch button B
	 public bool crouchRight; // is the player moving right and crouching
	 public bool crouchLeft; // is the player moving left and crouching

	 public int crouchHeight; // height when crouched
	 public int crouchSpeed; // the speed of the character when crouched horizontally
	 public int crouchJump; // the height of the jump when the player is in crouch
}
