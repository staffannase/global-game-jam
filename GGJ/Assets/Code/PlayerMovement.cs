using UnityEngine;
using System.Collections;

[RequireComponent( typeof( Rigidbody ) )]
[RequireComponent( typeof( CapsuleCollider ) )]

public class PlayerMovement : MonoBehaviour {

    //Movement Variables
    float horizontalMovement = 0f;
    float verticalMovement = 0f;
    public float movementSpeed = 5f;
    public int numberOfJumps = 2;
    int jumpCount = 2;
    bool isJumping = false;
    public float jumpHeight = 2.2f;
    public float maxVelocityChange = 5.0f;
    bool isGrounded = false;

    //Input Variables
    string inputQueue = string.Empty;
    int maxQueueSize = 10;
    float queueTime = 0f;
    float maxQueueTime = 2.5f;
    float inputSensitivity = 0.6f;
    bool vAxisInUse = false;
    bool hAxisInUse = false;
    bool up = false;
    bool down = false;
    bool left = false;
    bool right = false;

    //Animation Variables
    Animator anim;

    //DEBUG VARS
    bool showStats = false;
    bool showInputBuffer = true;

    #region Initialize Region

    void Awake() {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().mass = 60f;
        anim = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start() {
        //Physics.IgnoreLayerCollision( 8, 11 );
        jumpCount = numberOfJumps;
    }

    #endregion

    void Update() {
        InputQueueUpdate();
        Attack();
        Movement();


        //Debugfuncs
        //DebugDrawRays();
        //DebugInput();
        //DebugPrintVars();
    }

    #region Input Queue Handler Region

    void InputQueueUpdate() {
        QueueInputAxis();
        QueueInputButtons();
        CheckQueueSize();
        CheckQueueTime();

        if ( !( inputQueue.Length < 1 ) ) {
            tempInput = inputQueue;
        }
    }

    /* 789
	 * 4 6
	 * 123
	 * Directions
	 * 1 = downleft
	 * 2 = down
	 * 3 = downright
	 * 4 = left
	 * 6 = right
	 * 7 = upleft
	 * 8 = up
	 * 9 = upright
	 */
    //Queue Axis inputs
    void QueueInputAxis() {
        //Diagonals
        if ( Input.GetAxisRaw( "Vertical" ) < -0.6f && Input.GetAxisRaw( "Horizontal" ) > 0.6f ) {
            if ( !vAxisInUse || !hAxisInUse ) {
                vAxisInUse = hAxisInUse = true;
                inputQueue += "9";
            }
        } else if ( Input.GetAxisRaw( "Vertical" ) < -0.6f && Input.GetAxisRaw( "Horizontal" ) < -0.6f ) {
            if ( !vAxisInUse || !hAxisInUse ) {
                vAxisInUse = hAxisInUse = true;
                inputQueue += "7";
            }
        } else if ( Input.GetAxisRaw( "Vertical" ) > 0.6f && Input.GetAxisRaw( "Horizontal" ) > 0.6f ) {
            if ( !vAxisInUse || !hAxisInUse ) {
                vAxisInUse = hAxisInUse = true;
                inputQueue += "3";
            }
        } else if ( Input.GetAxisRaw( "Vertical" ) > 0.6f && Input.GetAxisRaw( "Horizontal" ) < -0.6f ) {
            if ( !vAxisInUse || !hAxisInUse ) {
                vAxisInUse = hAxisInUse = true;
                inputQueue += "1";
            }
        } else {
            vAxisInUse = hAxisInUse = false;
        }
        //Up down left right
        if ( Input.GetAxisRaw( "Vertical" ) < -0.6f && Input.GetAxisRaw( "Horizontal" ) < 0.6f && Input.GetAxisRaw( "Horizontal" ) > -0.6f ) {
            if ( !up ) {
                up = true;
                inputQueue += "8";
            }
        } else if ( Input.GetAxisRaw( "Vertical" ) > 0.6f && Input.GetAxisRaw( "Horizontal" ) < 0.6f && Input.GetAxisRaw( "Horizontal" ) > -0.6f ) {
            if ( !down ) {
                down = true;
                inputQueue += "2";
            }
        } else if ( Input.GetAxisRaw( "Horizontal" ) < -0.6f && Input.GetAxisRaw( "Vertical" ) < 0.6f && Input.GetAxisRaw( "Vertical" ) > -0.6f ) {
            if ( !left ) {
                left = true;
                inputQueue += "4";
            }
        } else if ( Input.GetAxisRaw( "Horizontal" ) > 0.6f && Input.GetAxisRaw( "Vertical" ) < 0.6f && Input.GetAxisRaw( "Vertical" ) > -0.6f ) {
            if ( !right ) {
                right = true;
                inputQueue += "6";
            }
        } else {
            up = down = left = right = false;
        }
        //Just check if any axis is engaged and reset queue time if so.
        if ( Input.GetAxisRaw( "Horizontal" ) > 0.6f || Input.GetAxisRaw( "Vertical" ) > 0.6f || Input.GetAxisRaw( "Horizontal" ) < -0.6f || Input.GetAxisRaw( "Vertical" ) < -0.6f ) {
            queueTime = 0f;
        }
    }

    /* A = attack
	 * B = block?
	 */
    //Queue Button Inputs
    void QueueInputButtons() {
        if ( Input.GetButtonDown( "Fire1" ) ) {
            inputQueue += "A";
            queueTime = 0f;
        }
        if ( Input.GetButtonDown( "Jump" ) ) {
            inputQueue += "J";
            queueTime = 0f;
        }
    }

    //remove first elements of input queue when size is reached
    void CheckQueueSize() {
        if ( inputQueue.Length > maxQueueSize ) {
            int i = inputQueue.Length - maxQueueSize;
            inputQueue = inputQueue.Remove( 0, i );
            inputQueue = inputQueue.Trim();
        }
    }

    //Empty queue if no inputs after some time
    void CheckQueueTime() {
        queueTime += Time.deltaTime;
        if ( queueTime >= maxQueueTime ) {
            queueTime = 0f;
            inputQueue = string.Empty;
        }
    }

    void FlushInputQueue() {
        inputQueue = string.Empty;
    }

    #endregion

    #region Attack Functions

    void Attack() {

        if ( inputQueue.Contains( "236A" ) || inputQueue.Contains( "214A" ) ) {

        }
    }

    //bool doCombo = false;
    //Handle attack input and actions
    /*void Attack() {
		if(Input.GetButtonDown("Fire1")) {
			if( currentAttackState.nameHash == Animator.StringToHash("Attack Layer.SwordAttack") && currentAttackState.normalizedTime >= 0.9f ) {
				doCombo = true;
			} else if (!doCombo) {
				anim.SetBool("doAttack", true);
				sword.collider.enabled = true;
			}
		}


		if(currentAttackState.nameHash == Animator.StringToHash("Attack Layer.SwordAttack") && currentAttackState.normalizedTime >= 0.9f) {
			if(doCombo) {
				anim.SetBool("doCombo1", true);
			} else {
				anim.SetBool("doAttack", false);
				sword.collider.enabled = false;
			}
		} else if(currentAttackState.nameHash == Animator.StringToHash("Attack Layer.SwordAttack2") && currentAttackState.normalizedTime >= 0.8f) {
			anim.SetBool("doCombo1", false);
			anim.SetBool("doAttack", false);
			doCombo = false;
			sword.collider.enabled = false;
		}
	}*/

    /*bool click = false;
	float comboNR = 0f;
	void Attack(){*/
    /*
        1. När ett klick sker så sparar vi det
        2. När vi går till attackidle igen så kollar vi om vi klickat
        3. Om vi klickat så inkrementerar vi combo med 1
        4. Om Combo är större än max antal attacker så börjar vi om (Eller har sista attacken i combo upprepas)
        5. Om vi inte klickat så sätter vi combo till 0

     */

    /*	if(Input.GetButtonDown("Fire1")) {
			click = true;
		}

		if(currentAttackState.nameHash == Animator.StringToHash("Attack Layer.AttackIdle") && click) {
			anim.SetBool("doAttack", true);
			anim.SetFloat("combo", comboNR++);
			click = false;
		} else {
			click = false;
			anim.SetBool("doAttack", false);
		}

	}*/

    #endregion

    #region Movement Functions

    private int direction = 1;

    //Handle movement input
    void Movement() {
        RaycastHit rayHit;
        horizontalMovement = Input.GetAxis( "Horizontal" );
        verticalMovement = Input.GetAxis( "Vertical" );

        if (horizontalMovement > 0)
        {
            direction = 1;
        } 
        else if (horizontalMovement < 0)
        {
            direction = -1;
        }

        anim.SetFloat ("Move", horizontalMovement + direction * Mathf.Abs (verticalMovement));

        if ( Physics.Raycast( this.transform.position, -transform.up, out rayHit, 1.05f, 9 ) ) {
            jumpCount = numberOfJumps;
            isGrounded = true;
        } else {
            isGrounded = false;
            if( isJumping ) {
                jumpCount--;
                isJumping = false;
            }
        }

        //Jump
        if ( Input.GetButtonDown( "Jump" ) && jumpCount > 0 ) {
            //if walljump
            //Do walljump animation and stuff
            //else normal jump
            isJumping = true;
            GetComponent<Rigidbody>().velocity = new Vector3( GetComponent<Rigidbody>().velocity.x, Mathf.Sqrt( 2 * jumpHeight * -Physics.gravity.y ), GetComponent<Rigidbody>().velocity.z );
        }
    }

    #endregion

    #region FixedUpdate Region

    void FixedUpdate() {
        FixedMovement();
    }

    //Apply movement from input.
    void FixedMovement() {
        //Movement
        Vector3 targetVelocity = new Vector3( horizontalMovement, 0, verticalMovement );
        targetVelocity = transform.TransformDirection( targetVelocity );        
        targetVelocity *= movementSpeed;
        Vector3 velocity = GetComponent<Rigidbody>().velocity;
        Vector3 velocityChange = ( targetVelocity - velocity );
        velocityChange.x = Mathf.Clamp( velocityChange.x, -maxVelocityChange, maxVelocityChange );
        velocityChange.y = 0;
        velocityChange.z = Mathf.Clamp( velocityChange.z, -maxVelocityChange, maxVelocityChange );
        GetComponent<Rigidbody>().AddForce( velocityChange, ForceMode.VelocityChange );
        //Add gravity
        GetComponent<Rigidbody>().AddForce( new Vector3( 0, Physics.gravity.y * GetComponent<Rigidbody>().mass, 0 ) );
    }

    #endregion

    #region Collision Region

    void OnCollisionEnter( Collision col ) {
        /*if ( !isGrounded ) {
            //Walljump check
            if ( Physics.Raycast( transform.position, transform.GetChild( 0 ).right, 0.8f ) ) {
                jumpCount = numberOfJumps;
            }
        }*/
    }

    #endregion

    #region Debug Fuctions

    /*
	 * 
	 * DEBUG FUNCTIONS!!!!!
	 * 
	 * 
	 * 

	 */

    void DebugDrawRays() {
        Debug.DrawRay( transform.position, -transform.up * 1.05f, Color.white ); //Grounded ray
        Debug.DrawRay( transform.position, transform.GetChild( 0 ).right * 0.8f, Color.white ); //walljump ray
    }

    void DebugPrintVars() {

        /*Debug.Log( "horizontalMovement: " + horizontalMovement.ToString() );
        Debug.Log( "verticalMovement: " + verticalMovement.ToString() );
        Debug.Log( "movementSpeed: " + movementSpeed.ToString() );
        Debug.Log( "maxVelocityChange: " + maxVelocityChange.ToString() );*/

        //Debug.Log( "numberOfJumps: " + numberOfJumps.ToString() );
        //Debug.Log( "jumpCount: " + jumpCount.ToString() );
        //Debug.Log( "jumpHeight: " + jumpHeight.ToString() );

        //Debug.Log( "isGrounded: " + isGrounded.ToString() );
    }

    void DebugInput() {
        if ( Input.GetKeyDown( KeyCode.F3 ) ) {
            showStats = !showStats;
        }
    }

    [Header( "Input writing, remove later" )]
    public Texture[] arrows = new Texture[10];
    public Texture punch;
    GUIStyle style = new GUIStyle();
    public Font myFont;
    string tempInput = string.Empty;
    Rect r = new Rect( Screen.width / 2f - Screen.width / 4, Screen.height / 2 - 50, 50, 50 );
    void OnGUI() {
        if ( showStats ) {
            GUI.Label( new Rect( 10, 10, Screen.width, 20 ), "INPUT VARIABLES" );
            GUI.Label( new Rect( 10, 30, Screen.width, 20 ), "Axis: h: " + Input.GetAxisRaw( "Horizontal" ).ToString() + ", v: " + Input.GetAxisRaw( "Vertical" ).ToString() );
            GUI.Label( new Rect( 10, 50, Screen.width, 20 ), "InputQueue: " + inputQueue );
            GUI.Label( new Rect( 10, 70, Screen.width, 20 ), "QueueTime: " + queueTime.ToString() + " / " + maxQueueTime.ToString() );

            GUI.Label( new Rect( 10, 90, Screen.width, 20 ), "PHYSICS VARIABLES" );
            GUI.Label( new Rect( 10, 110, Screen.width, 20 ), "Velocity: " + GetComponent<Rigidbody>().velocity.ToString() );
            /*GUI.Label(new Rect(10, 110, Screen.width, 20),
			GUI.Label(new Rect(10, 130, Screen.width, 20),
			GUI.Label(new Rect(10, 150, Screen.width, 20), 
			GUI.Label(new Rect(10, 170, Screen.width, 20), 
			GUI.Label(new Rect(10, 190, Screen.width, 20), 
			GUI.Label(new Rect(10, 50, Screen.width, 20), 
			GUI.Label(new Rect(10, 50, Screen.width, 20), 
			GUI.Label(new Rect(10, 50, Screen.width, 20), 
			GUI.Label(new Rect(10, 50, Screen.width, 20), 
			GUI.Label(new Rect(10, 50, Screen.width, 20), 
			GUI.Label(new Rect(10, 50, Screen.width, 20), 
			GUI.Label(new Rect(10, 50, Screen.width, 20), 
			GUI.Label(new Rect(10, 50, Screen.width, 20), 
			GUI.Label(new Rect(10, 50, Screen.width, 20), 
			GUI.Label(new Rect(10, 50, Screen.width, 20), 
			GUI.Label(new Rect(10, 50, Screen.width, 20), 
			GUI.Label(new Rect(10, 50, Screen.width, 20), 
			GUI.Label(new Rect(10, 50, Screen.width, 20), 
			GUI.Label(new Rect(10, 50, Screen.width, 20), 
			GUI.Label(new Rect(10, 50, Screen.width, 20), 
			*/
        }
        if ( showInputBuffer ) {
            style.font = myFont;
            style.fontSize = 32;
            string s = string.Empty;
            foreach ( char c in tempInput ) {
                if ( c == '1' ) {
                    s += "X ";
                } else if ( c == '2' ) {
                    s += "D ";
                } else if ( c == '3' ) {
                    s += "Y ";
                } else if ( c == '4' ) {
                    s += "L ";
                } else if ( c == '6' ) {
                    s += "R ";
                } else if ( c == '7' ) {
                    s += "x ";
                } else if ( c == '8' ) {
                    s += "U ";
                } else if ( c == '9' ) {
                    s += "y ";
                } else if ( c == 'A' ) {
                    s += "O ";
                } else if ( c == 'J' ) {
                    s += "e ";
                }
            }
            GUI.Label( r, s, style );
        }
    }

    #endregion
}
