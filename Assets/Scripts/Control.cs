using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    Animator anim;
    [SerializeField] float speed_ahead;
    [SerializeField] float speed_back;
    [SerializeField] float rotate_speed;
    [SerializeField] float fall_time;
    [SerializeField] Joystick left;
    [SerializeField] Joystick right;
    [SerializeField] Event_object aim;
    [SerializeField] Event_object jump;
    [SerializeField] GameObject step_left;
    [SerializeField] GameObject step_right;
    [SerializeField] GameObject prefab_steps;
    [SerializeField] GameObject main_collider;
    [SerializeField] GameObject trigger_collider;
    bool stop;
    Rigidbody rb;
    bool is_ground;
    Vector3 save_position;
    float min_distance = 0.5f;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Jump") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Run_jump"))
        {
            rb.drag = 0;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Run_jump"))
        {
            rb.drag = 20;
            anim.SetBool("is_fall", true);
            if(Input.GetKey(KeyCode.W) || left.Direction.y > 0.3 && !stop) transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, speed_ahead);
        }
        else
        {
            anim.SetBool("is_fall", true);
            rb.drag = 20;
        }

        if (Input.GetKey(KeyCode.D) || left.Direction.x > 0.5 || right.Direction.x > 0.5) transform.Rotate(Vector3.up, rotate_speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.A) || left.Direction.x < -0.5 || right.Direction.x < -0.5) transform.Rotate(Vector3.up, -rotate_speed * Time.deltaTime);

        if (rb.velocity.y < -1 && !is_ground)
        {
            anim.SetBool("is_fall", true);
            anim.SetBool("is_ground", false);
            anim.Play("Falling", 0);
        }
        else if ((Input.GetKey(KeyCode.W) || left.Direction.y > 0.3)  && (Input.GetKey(KeyCode.Space) || jump.push) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Jump") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Run_jump") && is_ground)
        {
            anim.Play("Run_jump", 0);
        }
        else if ((Input.GetKey(KeyCode.Space) || jump.push) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Jump") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Run_jump") && is_ground)
        {
            anim.Play("Jump", 0);
        }
        else if ((aim.push) && (Input.GetKey(KeyCode.W) || left.Direction.y > 0.3) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Jump") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Run_jump") && is_ground)
        {
            anim.Play("Shoot_run", 0);
           if (!stop) transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, speed_ahead);
        }
        else if ((aim.push) && (Input.GetKey(KeyCode.S) || left.Direction.y < -0.3) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Jump") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Run_jump") && is_ground)
        {
            anim.Play("Back_aim", 0);
            if (!stop) transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, speed_back);
        }
        else if ((Input.GetKey(KeyCode.S) || left.Direction.y < -0.3) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Jump") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Run_jump") && is_ground)
        {
            anim.Play("Back", 0);
            if (!stop) transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, speed_back);
        }
        else if ((aim.push) && (Input.GetKey(KeyCode.A) || left.Direction.x < -0.5 || right.Direction.x < -0.5) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Jump") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Run_jump") && is_ground)
        {
            anim.Play("Aim", 0);
            transform.Rotate(Vector3.up, -rotate_speed * Time.deltaTime);
        }
        else if ((aim.push) && (Input.GetKey(KeyCode.D) || left.Direction.x > 0.5 || right.Direction.x > 0.5) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Jump") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Run_jump") && is_ground)
        {
            anim.Play("Aim", 0);
            transform.Rotate(Vector3.up, rotate_speed * Time.deltaTime);
        }
        else if ((Input.GetKey(KeyCode.W) || left.Direction.y > 0.3) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Jump") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Run_jump") && is_ground)
        {
            anim.Play("Run", 0);
            if (!stop) transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, speed_ahead);
        }
        else if ((Input.GetKey(KeyCode.A) || left.Direction.x < -0.5 || right.Direction.x < -0.5) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Jump") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Run_jump") && is_ground)
        {
            anim.Play("Left", 0);
            transform.Rotate(Vector3.up, -rotate_speed * Time.deltaTime);
        }
        else if ((Input.GetKey(KeyCode.D) || left.Direction.x > 0.5 || right.Direction.x > 0.5) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Jump") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Run_jump") && is_ground)
        {
            anim.Play("Right", 0);
            transform.Rotate(Vector3.up, rotate_speed * Time.deltaTime);
        }
        else if ((aim.push) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Jump") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Run_jump") && is_ground)
        {
            anim.Play("Aim", 0);
        }
        else if (!Input.anyKey && !anim.GetCurrentAnimatorStateInfo(0).IsName("Jump") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Falling") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Run_jump") && is_ground)
        {
            anim.Play("Idle", 0);
        }
        RaycastHit hit;
        Ray ray = new Ray(step_left.transform.position, Vector3.down);
        Physics.Raycast(ray, out hit);

        float distance_left =  Vector3.Distance(hit.point, step_left.transform.position);
        ray = new Ray(step_left.transform.position, Vector3.down);
        Physics.Raycast(ray, out hit);
        float distance_right = Vector3.Distance(hit.point, step_right.transform.position);
        if(distance_left > 1 && distance_right > 1) is_ground = false;
    }

    public void check_fall()
    {
        StartCoroutine(start_fall());
    }

    IEnumerator start_fall()
    {
        yield return new WaitForSecondsRealtime(fall_time);
        if (!is_ground)
        {
            anim.SetBool("is_fall", true);
            anim.SetBool("is_ground", false);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        is_ground = true;
        anim.SetBool("is_fall", false);
        anim.SetBool("is_ground", true);

        GameObject faced = collision.contacts[0].thisCollider.gameObject;
        if (faced.Equals(trigger_collider))
        {
            stop = true;
            transform.position = save_position;
            stop = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject faced = collision.contacts[0].thisCollider.gameObject;

        if (faced.Equals(step_left) || faced.Equals(step_right))
        {
            Instantiate(prefab_steps, faced.transform);
        }
        else if (faced.Equals(main_collider) && !stop)
        {
            save_position = transform.position;
        }
    }
}
