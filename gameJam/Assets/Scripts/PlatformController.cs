using UnityEngine;
using Rewired;

public class PlatformController : MonoBehaviour {

    internal Player PlatformPlayer;

    public float moveSpeed = 1;
    public float maxAngle = 35;
	
	// Update is called once per frame

    void Start() {
        PlatformPlayer = ReInput.players.GetPlayer(1);
    }

	void FixedUpdate () {
        float v_expected = PlatformPlayer.GetAxis("Vertical Tilt") * maxAngle;
        float h_expected = PlatformPlayer.GetAxis("Horizontal Tilt") * maxAngle;
        handleTileRotation(v_expected, h_expected);
    }
 
    private void handleTileRotation(float vertical, float horizontal) {
        Quaternion expectedPosition = Quaternion.Euler(vertical, 0, -horizontal);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, expectedPosition, moveSpeed * Time.deltaTime);
    }
}
