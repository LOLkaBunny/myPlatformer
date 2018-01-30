using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class characterController : MonoBehaviour {
	public float maxSpeed = 10f;
	public float jumpForce = 700f;
	bool facingRight = true;
	bool grounded = false;
	public Transform groundCheck;
	public float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float score;
	public float move;
    private Rigidbody2D rigidbody2D;
    public float spawnX, spawnY;

    private GameObject star;
	// Use this for initialization
	void Start () {
        spawnX = transform.position.x;
        spawnY = transform.position.y;
	}
	
	// Update is called once per frame
	void FixedUpdate () {


		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);

		move = Input.GetAxis ("Horizontal");

	}

	void Update(){

        rigidbody2D=GetComponent<Rigidbody2D>();
        if (grounded && (Input.GetKeyDown (KeyCode.W)||Input.GetKeyDown (KeyCode.UpArrow))) {

			rigidbody2D.AddForce (new Vector2(0f,jumpForce));
		}
		rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);
		
		if (move > 0 && !facingRight)
			Flip ();
		else if (move < 0 && facingRight)
			Flip ();



		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}

		if (Input.GetKey(KeyCode.R))
		{
			Application.LoadLevel(Application.loadedLevel);
		}


	}
	
	void Flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "star")
        {
            score++;
            Destroy(col.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if ((col.gameObject.name == "dieCollider") || (col.gameObject.name == "saw"))
        {
            transform.position = new Vector3(spawnX, spawnY, transform.position.z);

        }

        if (col.gameObject.name == "endLevel")
        {
            if (!(GameObject.Find("star")))
                SceneManager.LoadScene("scene2");

        }
        else if (col.gameObject.name == "endLevel2")
        {
            if(!(GameObject.Find("star")))
            SceneManager.LoadScene("scene1");

        }
    }



    /*void OnTriggerEnter2D(Collider2D col){
        if ((col.gameObject.name == "dieCollider") || (col.gameObject.name == "saw"))
        {
            Application.LoadLevel(Application.loadedLevel);
        }

	    if (col.gameObject.name == "star")
        {
						score++;
						Destroy (col.gameObject);
		}

		if (col.gameObject.name == "endLevel")
        {
			if (!(GameObject.Find("star"))) Application.LoadLevel ("scene2");

		}
	}*/

    void OnGUI()
    {
				GUI.Box (new Rect (0, 0, 100, 100), "Stars: " + score);
	}
		
}