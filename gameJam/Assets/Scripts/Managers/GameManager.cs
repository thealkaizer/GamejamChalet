using UnityEngine;

public class GameManager : MonoBehaviour {
    // ------------------------------------------------------------------------
    // Variables
    // ------------------------------------------------------------------------

    /* 
     * Note about ballance system
     * Balance system: il full dogs, balance is -100
     * If full cats, balance is 100
     * If perfect balance, value is 0
     */

    public GameObject ui_balancePivot;

    public Transform[] spawnPoints;
    public float spawnSpeedMinInSecond = 5f;
    public float spawnSpeedMaxInSecond = 0.1f;
    public float spawnSpeedCurrentInSecond;

    public int nbAnimals = 0;
    public int nbCats = 0;
    public int nbDogs = 0;

    public float percentNbCats = 0;
    public float percentNbDogs = 0;
    
    public float thresholdSuccess = 20; // % away from total ballance that is still ok
    public float thresholdWarning = 40;
    public float thresholdDanger = 80;
    public float thresholdLoose = 95;

    public float balanceSensibility = 1;
    public float currentBallanceLevel = 0;

    public int currentAnimalTrence = 0; // -1 if dogs, 1 if cats, 0 if perfect balance


    // ------------------------------------------------------------------------
    // Functions
    // ------------------------------------------------------------------------
    public void Start() {
        this.spawnSpeedCurrentInSecond = this.spawnSpeedMinInSecond;
    }
	
	// Update is called once per frame
	void Update () {
        //TODO TEMPORARY DEBUG

        // Handle inputs
        if(Input.GetKeyDown(KeyCode.I)) {
            this.nbDogs++;
            this.nbAnimals++;
            //Debug.Log("Add dog" + this.nbDogs);
        }
        else if(Input.GetKeyDown(KeyCode.O)) {
            this.nbCats++;
            this.nbAnimals++;
            //Debug.Log("Add cat");
            //Debug.Log("Add cat" + this.nbCats);
        }
        
        this.updateBalance();
        this.processBalanceActions();
    }

    /**
     * Update data and the balance value
     */
    private void updateBalance() {
        // Special case of 0 animals
        if(nbAnimals == 0) {
            int old = currentAnimalTrence;
            this.currentBallanceLevel = 0;
            this.percentNbCats = 0;
            this.percentNbDogs = 0;
            this.currentAnimalTrence = 0;
            if(old != currentAnimalTrence) {
                //TODO Play event
            }
            float expectedNewBalance = 0;
            Debug.Log(balanceSensibility * Time.deltaTime);
            this.currentBallanceLevel = Mathf.Lerp(currentBallanceLevel, expectedNewBalance, balanceSensibility * Time.deltaTime);
            return;
        }
        this.percentNbCats = ((float)nbCats / (float)nbAnimals) * 100;
        this.percentNbDogs = ((float)nbDogs / (float)nbAnimals) * 100;

        // Update balance trence
        int oldTrence = currentAnimalTrence;
        currentAnimalTrence = percentNbCats > percentNbDogs ? 1 : -1;

        // Update current balance value
        if(percentNbCats == percentNbDogs) {
            currentAnimalTrence = 0; // Special case (The perfect ballance, but really rare)
            if(oldTrence != currentAnimalTrence) {
                //TODO Play special event like sound or something (To emphazise the change)
            }
            float expectedNewBalance = 0;
            this.currentBallanceLevel = Mathf.Lerp(currentBallanceLevel, expectedNewBalance, balanceSensibility * Time.deltaTime);
        }
        else if(percentNbDogs > percentNbCats) {
            // You are a dog lover, but the ballance may be broken
            if(oldTrence != currentAnimalTrence) {
                //TODO
            }
            float expectedNewBalance = -percentNbDogs;
            this.currentBallanceLevel = Mathf.Lerp(currentBallanceLevel, expectedNewBalance, balanceSensibility * Time.deltaTime);
        }
        else {
            // You are a cat lover, but the ballance may be broken
            if(oldTrence != currentAnimalTrence) {
                //TODO
            }
            float expectedNewBalance = percentNbCats;
            this.currentBallanceLevel = Mathf.Lerp(currentBallanceLevel, expectedNewBalance, balanceSensibility * Time.deltaTime);
        }
    }

    private void processBalanceActions() {
        Quaternion newPosition = Quaternion.Euler(0, 0, this.currentBallanceLevel);
        this.ui_balancePivot.transform.rotation = newPosition;
    }
}
