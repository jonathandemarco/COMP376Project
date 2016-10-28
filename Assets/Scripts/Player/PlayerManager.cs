using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlayerSettings {
    public float dashSpeed;
    public float dashTime;
    public float maxHeldTimeDash;
    public float dashCoolDownTime;
    public float jumpForce;
    public float jumpCooldownTime;
    public float moveSpeed;
    public float airMoveForce;
    public float rotateSpeed;
    public int inventorySize;

    public float groundDistance;
    public float frontDistance;

    public float tapTime;
}
public class PlayerManager : MonoBehaviour {

    // Use this for initialization

    public float maxHealth;
    public int numLives;
    public float respawnTime;
    public char playerChar;
    public char team;

    public bool noSetup;

    public PlayerSettings settings;
    public InventoryManager inventory;
    private PlayerControls playerController;
    private Rigidbody rb;

    private float health;
    private bool isAlive;
    private bool isEliminated;
    private int score;
    private float timeSinceDeath;

    private bool grounded;
    private bool canMove;
    private bool charging;

    private float nextDash = 0;
    private float dashStopTime;

    private float nextJump = 0;

    private float dashSpeedRatio;

    private Vector3 lastPosition;

    Vector3 velocity;


    void Awake() {
        playerController = GetComponentInChildren<PlayerControls>();
        rb = GetComponent<Rigidbody>();
        if (noSetup)
            setPlayerChar(playerChar);
    }
    void Start () {
        isEliminated = false;
        isAlive = true;
        canMove = true;
        charging = false;
        grounded = true;
        health = maxHealth;
        score = 0;
	}

    // Update is called once per frame

    public void setPlayerChar(char c) {
        playerChar = c;
        playerController.setPlayer(this);
        GetComponent<Renderer>().sharedMaterial = (Material)Resources.Load("Player_"+c, typeof(Material));
        
    
    }
    void Update () {
        if (!isEliminated)
        {
            velocity = (transform.position - lastPosition)/Time.deltaTime;

            lastPosition = transform.position;
            if (!isAlive)
            {
                timeSinceDeath += Time.deltaTime;
                if (timeSinceDeath >= respawnTime)
                    respawn();
            }
            else {
                playerController.readInput();
                if (canMove && grounded)
                {
                    playerController.move();
                }
                if (canMove && !grounded && !frontCollision()) {
                    playerController.moveInAir();
                }
            }
            if (GetComponent<Rigidbody>().velocity.y != 0)
                checkGround();
            manageAbilies();
        }
	}

    private void notify() {
        HUDManager.currentHUD.update(this.GetComponent<PlayerManager>());  
    }



    public void takeDamage(float damage) {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            die();
        }
        notify();
    }
    public void heal(float heal)
    {
        health += heal;
        if (health > maxHealth)
            health = maxHealth;
        notify();
    }
    public void addScore(int s) {
        score += s;
        notify();
    }
    public void resetScore() {
        score = 0;
        notify();
    }

    public int getScore() {
        return score;
    }
    public float getHealth() {
        return health;
    }
    public float getMaxHealth() {
        return maxHealth;
    }
    public int getNumLives() {
        return numLives;
    }
    public char getPlayerChar() {
        return playerChar;
    }
    public char getTeam() {
        return team;
    }
    public void setTeam(char c) {
        team = c;
    }
    
    private void die()
    {
        isAlive = false;
        numLives--;
        timeSinceDeath = 0;
        GetComponent<MeshRenderer>().enabled = false; // replace with mesh child
        if (numLives <= 0)
            isEliminated = true;

        notify();

    }
    private void respawn()
    {
        isAlive = true;
        health = maxHealth;
        transform.position = getSpawnPoint();
        GetComponent<MeshRenderer>().enabled = true; // replace with mesh child
        notify();
    }

    private Vector3 getSpawnPoint()
    {
        // GameObject manager = GameObject.FindGameObjectWithTag("LevelManager");
        //return manager.GetComponent<LevelManager>().getRespawnPoint();
        return new Vector3(0, 0, 0);
    }
    
    private void attack(int slot) {

    }
    private void jump() {
        if (checkGround() && Time.time > nextJump )
        {
            nextJump = Time.time + settings.jumpCooldownTime;
            rb.velocity = velocity*0.7f;
            rb.AddForce(Vector3.up * settings.jumpForce);
            grounded = false;
        }
    }
    private void land() {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }
    private bool checkGround()
    {
        bool previousState = grounded;
        grounded = Physics.Raycast(transform.position, Vector3.down, GetComponentInParent<CapsuleCollider>().height*settings.groundDistance*transform.localScale.y);

        if (previousState == false && grounded == true)
            land();
        return grounded;
    }
    private bool frontCollision() {
        bool collision = Physics.Raycast(transform.position, transform.forward, GetComponentInParent<CapsuleCollider>().radius * settings.frontDistance);
        if (collision) {
            dashStopTime = 0;
        }
        return collision;

    }

    private void dash()
    {
        if (Time.time >nextDash )
        {
            if (!grounded)
            {
                dashStopTime = Time.time + settings.dashTime / 2;
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
            }
            else
                dashStopTime = Time.time + settings.dashTime;
            nextDash = dashStopTime+ settings.dashCoolDownTime;
            canMove = false;
        }
    }
    


    private void manageAbilies() {

        if (dashStopTime > Time.time && !frontCollision())
        {
            transform.Translate(Vector3.forward * settings.dashSpeed * Time.deltaTime);
        }
        else if (!charging)
        {
            canMove = true;

        }
    }

    public void getMessage(ControlButton button)
    {
        int buttonID = button.getID();
        ControlButton.ACTION action = button.getLastState();
        switch (buttonID) {
            case 0: button0(button, action);
                break;
            case 1:
                button1(button, action);
                break;
            case 2:
                button2(button, action);
                break;
            case 3:
                button3(button, action);
                break;
            case 4:
                button4(button, action);
                break;
        }
    }

    
    public void button0(ControlButton butto, ControlButton.ACTION action) {
        if (action == ControlButton.ACTION.PRESS) {
            jump();
        }
        else if (action == ControlButton.ACTION.HOLD)
        {
            //hold Button0;
        }
        else if (action == ControlButton.ACTION.RELEASE)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if(rb.velocity.y >0)
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y/2, rb.velocity.z);
            
        }
    }
    public void button1(ControlButton button, ControlButton.ACTION action)
    {
        if (action == ControlButton.ACTION.PRESS)
        {
            //press Button1;
            dash();
        }
        else if (action == ControlButton.ACTION.HOLD)
        {
            //hold Button1;
        }
        else if (action == ControlButton.ACTION.RELEASE)
        {
            //release Button1;
        }
    }
    public void button2(ControlButton button, ControlButton.ACTION action)
    {
        if (action == ControlButton.ACTION.PRESS)
        {
            takeDamage(10);
        }
        else if (action == ControlButton.ACTION.HOLD)
        {
            //hold Button2;
        }
        else if (action == ControlButton.ACTION.RELEASE)
        {
            //release Button2;
        }
    }
    public void button3(ControlButton button, ControlButton.ACTION action)
    {
        if (action == ControlButton.ACTION.PRESS)
        {
            inventory.GetWeapon(0);
        }
        else if (action == ControlButton.ACTION.HOLD)
        {

            
        }
        else if (action == ControlButton.ACTION.RELEASE)
        {

        }
    }
    public void button4(ControlButton button, ControlButton.ACTION action)
    {
        if (action == ControlButton.ACTION.PRESS)
        {
            //press Button4;
        }
        else if (action == ControlButton.ACTION.HOLD)
        {
            //hold Button4;
        }
        else if (action == ControlButton.ACTION.RELEASE)
        {
            //release Button4;
        }
    }



}
