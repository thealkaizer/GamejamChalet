using UnityEngine;

public class PlayerController : MonoBehaviour {
    // ------------------------------------------------------------------------
    // Variables
    // ------------------------------------------------------------------------
    public float walkingSpeed = 5f;
    private bool isWalking;


    // ------------------------------------------------------------------------
    // Functions
    // ------------------------------------------------------------------------

    // Update is called once per frame
    void Update() {
        float horizontal    = Input.GetAxisRaw("Horizontal");
        float verical       = Input.GetAxisRaw("Vertical");
        this.handleMovement(horizontal, verical);
    }

    private void handleMovement(float horizontal, float vertical) {
        this.isWalking = false;
        if(horizontal != 0.0f || vertical != 0.0f) {
            this.isWalking = true;
            float angle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0f, angle, 0f));
            transform.position += transform.forward * Time.deltaTime * walkingSpeed;
        }
    }
}