using UnityEngine;

public class PlatformController : MonoBehaviour {
    public float moveSpeed = 1;
    public float maxAngle = 35;
	
	// Update is called once per frame
	void FixedUpdate () {
        float v_expected = Input.GetAxis("Vertical") * maxAngle;
        float h_expected = Input.GetAxis("Horizontal") * maxAngle;
        handleTileRotation(v_expected, h_expected);
    }
 
    private void handleTileRotation(float vertical, float horizontal) {
        Quaternion expectedPosition = Quaternion.Euler(vertical, 0, -horizontal);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, expectedPosition, moveSpeed * Time.deltaTime);
    }
}
