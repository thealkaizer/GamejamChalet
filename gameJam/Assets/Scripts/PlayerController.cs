using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour {
    // ------------------------------------------------------------------------
    // Variables
    // ------------------------------------------------------------------------
    internal Player CharacterPlayer;

    public float walkingSpeed = 5f;
    private bool isWalking;


    // ------------------------------------------------------------------------
    // Functions
    // ------------------------------------------------------------------------

    void Start() {
        CharacterPlayer = ReInput.players.GetPlayer(0);
    }

    // Update is called once per frame
    void Update() {
        float horizontal    = CharacterPlayer.GetAxisRaw("Character Horizontal Axis");
        float vertical       = CharacterPlayer.GetAxisRaw("Character Vertical Axis");
        this.handleMovement(horizontal, vertical);
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