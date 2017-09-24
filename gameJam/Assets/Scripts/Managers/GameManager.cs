using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
    public Text ui_dogCounter;
    public Text ui_catCounter;

    private int nbAnimals = 0;
    private int nbCats = 0;
    private int nbDogs = 0;

    private float percentNbCats = 0;
    private float percentNbDogs = 0;

    public float balanceSensibility = 1;
    private float currentBallanceLevel = 0;

    private int currentAnimalTrence = 0; // -1 if dogs, 1 if cats, 0 if perfect balance


    // Spawning variables
    public Transform[] spawnPoints;
    public float spawnSpeedMin = 5f;
    public float spawnSpeedMax = 0.1f;
    public float spawnAccelerationSpeed = 0.2f;

    public GameObject catPrefab;
    public GameObject dogPrefab;


    // Hole management variables
    public GameObject[] listHoles;
    
    public float minOpenFrequency;
    public float maxOpenFrequency;
    public int maxOpenedHolesConcurrently = 1;

    private int openedHoleCounter = 0;



    // ------------------------------------------------------------------------
    // Functions
    // ------------------------------------------------------------------------
    void Start() {
        // TMP (Probably to be moved later)
        this.startSpawning();
        this.startOpeningHoles();
        this.ui_catCounter.text = "Cats: 0";
        this.ui_dogCounter.text = "Dogs: 0";
    }
	
	// Update is called once per frame
	void Update () {
        this.updateBalance();
        this.calculateNewBalance();
        this.processBalanceActions();
    }


    // ------------------------------------------------------------------------
    // Balance manager
    // ------------------------------------------------------------------------

    public void addOneCat() {
        this.nbAnimals++;
        this.nbCats++;
        this.ui_catCounter.text = "Cats: " + nbCats;
        AkSoundEngine.PostEvent("Play_Hole_Animals", gameObject);
        
    }

    public void addOneDog() {
        this.nbAnimals++;
        this.nbDogs++;
        this.ui_dogCounter.text = "Dogs: " + nbDogs;
        AkSoundEngine.PostEvent("Play_Hole_Animals", gameObject);
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
                // TODO Play sound or visual effect ?
            }
            else if (currentAnimalTrence == -1) {
                // Dog side
                // TODO Play sound or visual effect ?
            }
            else {
                // Cat side
                // TODO Play sound or visual effect ?
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
        // Update UI
        Quaternion newPosition = Quaternion.Euler(0, 0, this.currentBallanceLevel);
        this.ui_balancePivot.transform.rotation = newPosition;
    }


    // ------------------------------------------------------------------------
    // Spawning
    // ------------------------------------------------------------------------
    public void startSpawning() {
        StartCoroutine(spawnOneAnimal(this.spawnSpeedMin));
    }

    private IEnumerator spawnOneAnimal(float interval) {
        yield return new WaitForSeconds(interval);
        this.instanciateRandomAnimal();
        float newinterval = this.calculateNextSpawningTime(interval);
        StartCoroutine(spawnOneAnimal(newinterval));
    }

    private float calculateNextSpawningTime(float currentSpeed) {
        //TODO At the moment, do nothing.
        currentSpeed -= spawnAccelerationSpeed;
        currentSpeed = (currentSpeed <= this.spawnSpeedMax) ? this.spawnSpeedMax : currentSpeed;
        return currentSpeed;
    }

    private void instanciateRandomAnimal() {
        //TODO At the moment, simple random
        int pos = Random.Range(0, this.spawnPoints.Length);
        int i = Random.Range(0, 2);
        GameObject prefab = (i == 0) ? catPrefab : dogPrefab;
        Instantiate(prefab, spawnPoints[pos].position, spawnPoints[pos].rotation);
        if (i == 0) {
            AkSoundEngine.PostEvent("Play_Falling_Cat", gameObject);
        } else {
            AkSoundEngine.PostEvent("Play_Falling_Dog", gameObject);
        }
    }


    // ------------------------------------------------------------------------
    // Hole
    // ------------------------------------------------------------------------
    private void startOpeningHoles() {
        openRandomHole();
        StartCoroutine(openOneHole(this.maxOpenFrequency));
    }

    public IEnumerator openOneHole(float interval) {
        yield return new WaitForSeconds(interval);
        if(this.openedHoleCounter < this.maxOpenedHolesConcurrently) {
            this.openRandomHole();
        }
        interval = calculateNextHoleTime();
        StartCoroutine(openOneHole(interval));
    }

    private float calculateNextHoleTime() {
        return Random.Range(this.maxOpenFrequency, this.minOpenFrequency);
    }

    private void openRandomHole() {
        int pos = Random.Range(0, listHoles.Length -1);
        GameObject o = listHoles[pos];
        HoleControl holeControl = o.GetComponent<HoleControl>();
        holeControl.openHole();
    }
}