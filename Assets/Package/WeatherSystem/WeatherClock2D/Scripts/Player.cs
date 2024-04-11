using UnityEngine;

namespace WeatherControl{
    public class Player : MonoBehaviour
    {

        private Animator anim;
        private Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;
        public int speed = 10;
        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            float inputX = Input.GetAxis("Horizontal");

            Vector3 movement = new Vector3(speed * inputX, 0, 0);

            movement *= Time.deltaTime;

            transform.Translate(movement);
        }

        void Update()
        {
            animationController();   
        }

        void animationController(){
            
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
                anim.SetTrigger("walk");
                spriteRenderer.flipX = false;
            }else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
                anim.SetTrigger("idle");
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
                anim.SetTrigger("walk");
                spriteRenderer.flipX = true;
            }else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
                anim.SetTrigger("idle");
            }
        }

    }
}