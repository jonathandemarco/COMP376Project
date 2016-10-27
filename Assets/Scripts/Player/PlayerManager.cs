using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    // Use this for initialization

    public float maxHealth;
    public int numLives;
    public float respawnTime;
    public char playerChar;

    private float health;
    private bool isAlive;
    private bool isEliminated;
    private bool canMove;
    private int score;
    private float timeSinceDeath;
    private PlayerControls playerController;

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

    public void takeDamage(float damage) {
        health -= damage;
        if (health <= 0)
            die();
    }
    public void heal(float heal)
    {
        health += heal;
        if (health > maxHealth)
            health = maxHealth;
    }

    private void die() {

        isAlive = false;
        numLives--;
        timeSinceDeath = 0;
        GetComponent<MeshRenderer>().enabled = false; // replace with mesh child
        if (numLives <= 0)
            isEliminated = true;
    }
    private void respawn() {
        isAlive = true;
        health = maxHealth;
        transform.position = getSpawnPoint();
        GetComponent<MeshRenderer>().enabled = true; // replace with mesh child

    }

    private Vector3 getSpawnPoint() {
        // Get spawn point from level manager
        return Vector3.up;
    }

    public int getScore() {
        return score;
    }
    public int getNumLives() {
        return numLives;
    }
    public char getPlayerChar() {
        return playerChar;
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
            Debug.Log("Recieved Message!");
            die();
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
            respawn();
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
            //press Button2;
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
