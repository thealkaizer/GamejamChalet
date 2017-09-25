using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour {
    // ------------------------------------------------------------------------
    // Variables
    // ------------------------------------------------------------------------
    internal Player CharacterPlayer;
    public Animator playerAnim;
    HammerAttack hammerScript;

    public float walkingSpeed = 0.2f;
    private bool isWalking;


    // ------------------------------------------------------------------------
    // Functions
    // ------------------------------------------------------------------------

    void Start() {
        CharacterPlayer = ReInput.players.GetPlayer(0);
        hammerScript = GetComponent<HammerAttack>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        float horizontal    = CharacterPlayer.GetAxisRaw("Character Horizontal Axis");
        float vertical      = CharacterPlayer.GetAxisRaw("Character Vertical Axis");
        this.handleMovement(horizontal, vertical);
    }

    private void handleMovement(float horizontal, float vertical) {
        this.isWalking = false;
        if(horizontal != 0.0f || vertical != 0.0f) {
            playerAnim.SetInteger("AnimInt", 1);
            this.isWalking = true;
            float angle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;
            transform.localRotation = Quaternion.Euler(new Vector3(0f, angle, 0f));
            transform.localPosition += transform.forward * Time.deltaTime * walkingSpeed;
        } else {
            if (playerAnim.GetInteger("AnimInt") == 1) {
                playerAnim.SetInteger("AnimInt", 0);
            }
        }
    }
}