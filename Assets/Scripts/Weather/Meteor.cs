using UnityEngine;
using System.Collections;

public class Meteor : HostileTerrain {

    public float speed;
    public ParticleSystem MeteorExplosionParticleSystem;
    public ParticleSystem MeteorShrapnelParticleSystem;
    public Material[] MeteorMaterials;
    public Mesh meteorMesh;

    private Vector3 direction;
    private AudioSource[] audioSources;
    private bool soundPlayed = false;

    // Use this for initialization
    void Start() {

        audioSources = GetComponents<AudioSource>();

        // setup material
        Renderer renderer = GetComponent<Renderer>();
        renderer.sharedMaterial = MeteorMaterials[UnityEngine.Random.Range(0, MeteorMaterials.Length)];

        // setup mesh
        Mesh mesh = meteorMesh;
        GetComponent<MeshFilter>().mesh = mesh;

        // setup trail
        float scale = 1.0f;
        TrailRenderer t = GetComponent<TrailRenderer>();
        t.startWidth = UnityEngine.Random.Range(2.0f, 3.0f) * scale;
        t.endWidth = UnityEngine.Random.Range(0.25f, 0.5f) * scale;
        t.time = UnityEngine.Random.Range(0.25f, 0.5f);

        audioSources[1].Play();
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void setDirection(Vector3 direction) {
        this.direction = direction;
    }

    override public void OnCollisionEnter(Collision col) {
        /*
        //TODO: handle collision with player --> apply damage
        //TODO: handle collision with other meteors --> dont explode
        */
		base.OnCollisionEnter (col);

        if (col.collider.tag == "meteor" || soundPlayed) {
            return;
        }
        Vector3 pos, normal;
        if (col.contacts.Length == 0)
        {
            pos = transform.position;
            normal = -pos;
        }
        else
        {
            pos = col.contacts[0].point;
            normal = col.contacts[0].normal;
        }
        ParticleSystem explosion = Instantiate(MeteorExplosionParticleSystem);
        explosion.transform.position = pos;
        explosion.transform.rotation = Quaternion.LookRotation(normal);
        explosion.Emit(UnityEngine.Random.Range(10, 20));

        ParticleSystem shrapnel = Instantiate(MeteorExplosionParticleSystem);
        shrapnel.transform.position = col.contacts[0].point;
        shrapnel.Emit(UnityEngine.Random.Range(10, 20));

 
        AudioSource s = audioSources[0];
        s.PlayOneShot(s.clip);
        soundPlayed = true;
        

        GetComponent< Renderer>().enabled = false;
        GetComponent<TrailRenderer>().enabled = false;

        Destroy(gameObject, s.clip.length);
        
    }
}
