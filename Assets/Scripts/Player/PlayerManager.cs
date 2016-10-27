using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlayerSettings {
    public float dashForce;
    public float jumpForce;
    public float moveSpeed;
    public float rotateSpeed;
    public int inventorySize;

    public float groundDistance;
}
public class PlayerManager : MonoBehaviour {

    // Use this for initialization

    public float maxHealth;
    public int numLives;
    public float respawnTime;
    public char playerChar;
    public char team;


    public PlayerSettings settings;

    private float health;
    private bool isAlive;
    private bool isEliminated;
    private int score;
    private float timeSinceDeath;
    private PlayerControls playerController;

    private bool grounded;
    private bool canMove;




    void Awake() {
        playerController = GetComponentInChildren<PlayerControls>();
        playerController.setPlayer(this);
    }
    void Start () {
        isEliminated = false;
        isAlive = true;
        canMove = true;

        health = maxHealth;
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (!isEliminated)
        {
            if (!isAlive)
            {
                timeSinceDeath += Time.deltaTime;
                if (timeSinceDeath >= respawnTime)
                    respawn();
            }
            else {
                if (canMove)
                    playerController.update();
            }
        }
	}

    private void notify() {
        //HUDManager.currentHud.update(this);  
    }



    public void takeDamage(float damage) {
        health -= damage;
        if (health <= 0)
            die();
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
        GameObject manager = GameObject.FindGameObjectWithTag("LevelManager");
        return manager.GetComponent<LevelManager>().getRespawnPoint();
    }
    
    private void attack(int slot) {

    }
    private void jump() {
        if(checkGround())
        GetComponent<Rigidbody>().AddForce(Vector3.up*settings.jumpForce);
        
    }
    private bool checkGround()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, GetComponentInParent<CapsuleCollider>().height*settings.groundDistance);
        return grounded;
    }

    private void dash() {

        GetComponent<Rigidbody>().AddForce(transform.forward * settings.dashForce);
    }




    public void getMessage(int buttonID, ControlButton.ACTION action)
    {
        switch (buttonID) {
            case 0: button0(action);
                break;
            case 1:
                button1(action);
                break;
            case 2:
                button2(action);
                break;
            case 3:
                button3(action);
                break;
            case 4:
                button4(action);
                break;

        }

    }

    
    public void button0(ControlButton.ACTION action) {
        if (action == ControlButton.ACTION.PRESS) {
            jump();
        }
        else if (action == ControlButton.ACTION.HOLD)
        {
            //hold Button0;
        }
        else if (action == ControlButton.ACTION.RELEASE)
        {
            //release Button0;
        }
    }
    public void button1(ControlButton.ACTION action)
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
    public void button2(ControlButton.ACTION action)
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
    public void button3(ControlButton.ACTION action)
    {
        if (action == ControlButton.ACTION.PRESS)
        {
            //press Button3;
        }
        else if (action == ControlButton.ACTION.HOLD)
        {
            //hold Button3;
        }
        else if (action == ControlButton.ACTION.RELEASE)
        { 
            //release Button3;
        }
    }
    public void button4(ControlButton.ACTION action)
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
