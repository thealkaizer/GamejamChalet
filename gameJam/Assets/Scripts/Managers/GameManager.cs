using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    // ------------------------------------------------------------------------
    // Variables
    // ------------------------------------------------------------------------

    /* 
     * Note about ballance system
     * Balance system: il full dogs, balance is -100
     * If full cats, balance is 100
     * If perfect balance, value is 0
     * 
     * Warning: In UI, dogs are at the right since netative rotation is clockwise in our case 
     */

    // Balance variables
    public GameObject ui_balancePivot;

    private int nbAnimals = 0;
    private int nbCats = 0;
    private int nbDogs = 0;

    private float percentNbCats = 0;
    private float percentNbDogs = 0;
    
    public float thresholdSuccess = 20; // % away from total ballance that is still ok
    public float thresholdWarning = 40;
    public float thresholdDanger = 80;
    public float thresholdLoose = 95;

    public float balanceSensibility = 1;
    private float currentBallanceLevel = 0;

    private int currentAnimalTrence = 0; // -1 if dogs, 1 if cats, 0 if perfect balance


    // Spawning variables
    public Transform[] spawnPoints;
    public float spawnSpeedMin = 5f;
    public float spawnSpeedMax = 0.1f;
    public float spawnSpeedCurrent;

    public GameObject catPrefab;
    public GameObject dogPrefab;


    // ------------------------------------------------------------------------
    // Functions
    // ------------------------------------------------------------------------
    public void Start() {
        this.spawnSpeedCurrent = this.spawnSpeedMin;
        this.startSpawning(); //TMP (Must be removed later)
    }
	
	// Update is called once per frame
	void Update () {
        // WARNING TEMPORARY DEBUG
        if(Input.GetKeyDown(KeyCode.I)) {
            this.nbDogs++;
            this.nbAnimals++;
        }
        else if(Input.GetKeyDown(KeyCode.O)) {
            this.nbCats++;
            this.nbAnimals++;
            spawnSpeedCurrent -= 0.1f;
        }
        
        this.updateBalance();
        this.calculateNewBalance();
        this.processBalanceActions();
    }


    // ------------------------------------------------------------------------
    // Balance manager
    // ------------------------------------------------------------------------

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
                //TODO Play event (He just reach this beautiful balance)
            }
            float expectedNewBalance = 0;
            this.currentBallanceLevel = Mathf.Lerp(currentBallanceLevel, expectedNewBalance, balanceSensibility * Time.deltaTime);
            return;
        }

        // Update values
        this.percentNbCats = ((float)nbCats / (float)nbAnimals) * 100;
        this.percentNbDogs = ((float)nbDogs / (float)nbAnimals) * 100;
        int oldTrence = currentAnimalTrence;
        currentAnimalTrence = percentNbCats > percentNbDogs ? 1 : -1;
        
        // Special case of switching balance site (cat / dog or total balance)
        if(oldTrence != currentAnimalTrence) {
            if(currentAnimalTrence == 0) {
                // Perfect balance
                // TODO
            }
            else if (currentAnimalTrence == -1) {
                // Dog side
                // TODO
            }
            else {
                // Cat side
                // TODO
            }
        }
    }
    
    /**
     * Calculate the new Balance value.
     * Must be called after all animal data are up to date.
     */
    private void calculateNewBalance() {
        float balanceGap = nbCats - nbDogs;

        float expectedNewBalance = balanceGap * 10;
        expectedNewBalance = Mathf.Clamp(expectedNewBalance, -90, 90);
        this.currentBallanceLevel = Mathf.Lerp(currentBallanceLevel, expectedNewBalance, balanceSensibility * Time.deltaTime);
    }

    private void processBalanceActions() {
        Quaternion newPosition = Quaternion.Euler(0, 0, this.currentBallanceLevel);
        this.ui_balancePivot.transform.rotation = newPosition;
    }


    // ------------------------------------------------------------------------
    // Spawning
    // ------------------------------------------------------------------------
    public void startSpawning() {
        startSpawning(this.spawnSpeedCurrent);
    }

    public IEnumerator startSpawning(float interval) {
        yield return new WaitForSeconds(interval);
        //GameObject o = Instantiate()
        int i = Random.Range(0, this.spawnPoints.Length);
        this.instanciateRandomAnimal();
        this.calculateNewSpawningTime();
        StartCoroutine(startSpawning(this.spawnSpeedCurrent));
    }

    private void calculateNewSpawningTime() {
        //TODO At the moment, do nothing.
        this.spawnSpeedCurrent -= 0.5f;
        this.spawnSpeedCurrent =  Mathf.Clamp(this.spawnSpeedCurrent, this.spawnSpeedMin, this.spawnSpeedMax);
    }

    private void instanciateRandomAnimal() {
        //TODO At the moment, simple random
        int i = Random.Range(0, 1);
        if(i == 0) {
            GameObject o = Instantiate(catPrefab, spawnPoints[i].position, spawnPoints[i].rotation);
        }
        else {
            GameObject o = Instantiate(dogPrefab, spawnPoints[i].position, spawnPoints[i].rotation);
        }
    }
}