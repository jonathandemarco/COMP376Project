using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlayerSettings
{
    public float dashSpeed;
    public float dashTime;
    public float maxHeldTimeDash;
    public float dashCoolDownTime;
    public float jumpForce;
	public float jumpFowardVelocity;
    public float jumpCooldownTime;
    public float moveSpeed;
    public float groundDragForce;
    public float airDragForce;
    public float airMoveForce;
    public float rotateSpeed;
	public float maxSpeed;
    public int inventorySize;

    public float invinsibilityTime;
	public float respawnInvisibleTime;
    public float invinsibilityFlickerRate;
    public float groundDistance;
    public float frontDistance;
    public float pushbackParalizationTime;

    public float pushbackFactor;
    public float tapTime;
    public float buttonCooldown;

}
public class PlayerManager : MonoBehaviour, MessagePassing
{

    // Use this for initialization

    public float maxHealth;
    public int numLives;
    public float respawnTime;
    public char playerChar;
    public char team;

    public bool noSetup;

    public PlayerSettings settings;

    public GameObject respawnEffect;

    private PlayerControls playerController;

    private InventoryManager inventory;
    private Rigidbody rb;

    private float health;
    private bool isAlive;
    private bool isEliminated;
    private int score;
    private float timeSinceDeath;


    private bool grounded;
    private bool canMove;
    private bool charging;
    private bool invulnerable;
    private float nextDamage = 0;
    private float nextFlicker = 0;
    private float endParalysisTime = 0;


    private float nextDash = 0;
    private float dashStopTime;

    private float nextJump = 0;
	private float timeAtJump = 0;

    private float dashSpeedRatio;

    private Vector3 lastPosition;
	private Vector3 frontVector;

    Vector3 tempVelocity;
    private MeshRenderer mesh;



    void Awake()
    {
        playerController = GetComponentInChildren<PlayerControls>();
        inventory = GetComponentInChildren<InventoryManager>();
        rb = GetComponent<Rigidbody>();
        if (noSetup)
            setPlayerChar(playerChar);
        mesh = transform.Find("Model").GetComponent<MeshRenderer>();

    }
    void Start()
    {
        isEliminated = false;
        isAlive = true;
        canMove = true;
        charging = false;
        grounded = true;
        health = maxHealth;
        score = 0;

    }

    // Update is called once per frame

    public void setPlayerChar(char c)
    {
        playerChar = c;
        playerController.setPlayer(this);
        GetComponentInChildren<Renderer>().sharedMaterial = (Material)Resources.Load("Player_" + c, typeof(Material));


    }
    void Update()
    {
        if (!isEliminated)
        {
			tempVelocity = (transform.position - lastPosition) / Time.deltaTime;
            lastPosition = transform.position;
			frontVector = tempVelocity.normalized;
            if (!isAlive)
            {
                timeSinceDeath += Time.deltaTime;
                if (timeSinceDeath >= respawnTime) {
                    respawn();
                    GameObject obj = (GameObject)Instantiate(respawnEffect);
                    
                    obj.transform.position = transform.position - new Vector3(0, transform.position.y - 1, 0);
                    Destroy(obj, 3.0f);
                }
            }
            else
            {
                playerController.readInput();
                if (canMove && grounded)
                {
                    playerController.move();
                }
                if (canMove && !grounded && !frontCollision())
                {
                    playerController.moveInAir();
                }
            }
            if (GetComponent<Rigidbody>().velocity.y != 0)
                checkGround();
            manageStates();
        }
    }

    public void notify()
    {
        if (HUDManager.currentHUD != null)
            HUDManager.currentHUD.update(this.GetComponent<PlayerManager>());
    }



    public void takeDamage(float damage, Vector3 direction)
    {
        if (!invulnerable)
        {
            invulnerable = true;
            nextDamage = Time.time + settings.invinsibilityTime;
            endParalysisTime = Time.time + settings.pushbackParalizationTime;
            health -= damage;
            rb.AddForce(direction.normalized * damage * settings.pushbackFactor);
            if (health <= 0)
            {
                health = 0;
                die();
            }
            notify();
        }
    }
    public void heal(float heal)
    {
        health += heal;
        if (health > maxHealth)
            health = maxHealth;
        notify();
    }
    public void addScore(int s)
    {
        score += s;
        notify();
    }
    public void resetScore()
    {
        score = 0;
        notify();
    }

    public int getScore()
    {
        return score;
    }
    public float getHealth()
    {
        return health;
    }
    public bool getAlive() {
        return isAlive;
    }
    public float getMaxHealth()
    {
        return maxHealth;
    }
    public int getNumLives()
    {
        return numLives;
    }
    public char getPlayerChar()
    {
        return playerChar;
    }
    public char getTeam()
    {
        return team;
    }
    public void setTeam(char c)
    {
        team = c;
    }


    public InventoryManager getInventory()
    {
        return inventory;
    }
    private void die()
    {
        isAlive = false;
        numLives--;
        timeSinceDeath = 0;
        if (numLives <= 0)
            isEliminated = true;
        notify();
        disableModelRender(); // replace with mesh child
        inventory.resetInventory(false);


    }
    private void respawn()
    {
        isAlive = true;
        health = maxHealth;
        transform.position = getSpawnPoint();
        rb.velocity = new Vector3(0, 0, 0);
        enableModelRender(); // replace with mesh child
		invulnerable = true;
		nextDamage = Time.time + settings.respawnInvisibleTime;
        notify();
    }

    private Vector3 getSpawnPoint()
    {
        if (GameState.currentLevelManager != null)
            return GameState.currentLevelManager.getRespawnPoint(0);
            
        else return new Vector3(0, 5, 0);
    }

    private void attack(int slot)
    {

    }
    public void jump()
    {
        if (checkGround() && Time.time > nextJump)
        {
			timeAtJump = Time.time;
            nextJump = Time.time + settings.jumpCooldownTime;
            grounded = false;
			if (tempVelocity.magnitude != 0) {
				rb.velocity = tempVelocity;

			}
			
			rb.AddForce(Vector3.up * settings.jumpForce);
			
        }
    }
    private void land()
    {
       rb.velocity/=2;
    }
    private bool checkGround()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, GetComponentInParent<CapsuleCollider>().height * settings.groundDistance * transform.localScale.y);

		if (Time.time-timeAtJump>0.1 && grounded == true)
            land();
        return grounded;
    }
    private bool frontCollision()
    {
        bool collision = Physics.Raycast(transform.position, transform.forward, GetComponentInParent<CapsuleCollider>().radius * settings.frontDistance);
        if (collision)
        {
            dashStopTime = 0;
        }
        return collision;

    }

    public void dash()
    {
        if (Time.time > nextDash)
        {
            if (!grounded)
            {
                dashStopTime = Time.time + settings.dashTime;
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
            }
            else
                dashStopTime = Time.time + settings.dashTime;
            nextDash = dashStopTime + settings.dashCoolDownTime;
            canMove = false;
        }
    }

    private void flipModelRender() {

        mesh.enabled = !mesh.enabled;
    }
    private void enableModelRender()
    {
        mesh.enabled = true;
    }
    private void disableModelRender()
    {
        mesh.enabled = false;
    }

    private void manageStates()
    {

        if (dashStopTime > Time.time && !frontCollision())
        {
            transform.Translate(Vector3.forward * settings.dashSpeed * Time.deltaTime);
        }
        else if (!charging)
        {
            canMove = true;

        }


        if (invulnerable && isAlive)
        {
            if (Time.time > nextDamage)
            {
                invulnerable = false;
                enableModelRender();
            }
            else if (Time.time > nextFlicker)
            {
                flipModelRender();
                nextFlicker = Time.time + settings.invinsibilityFlickerRate;
            }
        }

        if (Time.time < endParalysisTime)
        {
            canMove = false;
        }
        else canMove = true;

        if (grounded)
        {
            rb.drag = settings.groundDragForce;
        }
        else
            rb.drag = settings.airDragForce;

		if (rb.velocity.magnitude > settings.maxSpeed) {
			rb.velocity = rb.velocity.normalized * settings.maxSpeed;
		}
    }

    public void drop(int index) {
        inventory.dropWeapon(index);
        notify();
    }



	void MessagePassing.collisionWith(Collider c)
	{
		Vector3 direction = transform.position - c.transform.position;
		if (c.gameObject.GetComponent<Weapon> ()) {
			if (c.gameObject.GetComponent<Latch> () != null && c.gameObject.GetComponent<Latch> ().getPlayerChar () != getPlayerChar ()) {
				takeDamage (c.gameObject.GetComponent<Latch> ().damage, new Vector3(0,0,0));
			}
			else if (c.gameObject.GetComponent<Weapon> ().getPlayerChar () != getPlayerChar ()) {
				takeDamage (c.gameObject.GetComponent<Weapon> ().damage, direction);
			}
		}
		else if (c.gameObject.GetComponent<HostileTerrain> ()) {
			Debug.Log (direction);
			takeDamage (c.gameObject.GetComponent<HostileTerrain> ().damage, direction);
		}
	}
}
