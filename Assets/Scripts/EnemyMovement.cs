using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Rigidbody2D m_rb;

    [Header("Value")]
    [SerializeField] private float m_speed = 10;

    [Header("Rotation")]
    [SerializeField] private Transform m_rotationHandler;
    [SerializeField] private float m_rotationSpeed = 25;

    [Header("Dash")]
    [SerializeField] private bool m_isDashing = false;
    [SerializeField] private ParticleSystem m_trailVFX;
    [SerializeField] private CharaController m_charaController;
    [SerializeField] private CircleCollider2D m_hitBox;

    [Header("Dash Enemy")]
    public Transform player;
    public Animator animator;
    public float moveSpeed = 3f;
    public float dashSpeed = 10f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 0.5f;
    public float dashAnim = 2.5f;
    private bool isDashing = false;
    private Vector3 dashDirection;
    public ParticleSystem trailVFX;

    [Header("Start")]
    [SerializeField] private ParticleSystem m_spawning;

    private void Start()
    {
        Launch();
        StartCoroutine(DashPeriodically());
    }

    private void Launch()
    {
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        m_rb.AddForce(randomDir * m_speed, ForceMode2D.Impulse);
        m_spawning.Play();
    }

    private void Update()
    {
        if (!isDashing)
        {
            transform.position += dashDirection * moveSpeed * Time.deltaTime;
        }

        m_rotationHandler.Rotate(Vector3.forward * m_rotationSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && !m_isDashing)
        {
            StartCoroutine(Invincibility());
        }
    }

    private IEnumerator Invincibility()
    {
        m_hitBox.enabled = false;

        yield return new WaitForSeconds(0.4f);
        m_hitBox.enabled = true;
    }

    IEnumerator DashPeriodically()
    {
        while (true)
        {
            yield return new WaitForSeconds(dashAnim);
            animator.Play("AnimDash");

            yield return new WaitForSeconds(dashCooldown);

            dashDirection = (player.position - transform.position).normalized;
            trailVFX.Play();
            animator.Play("HoldState");

            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;

        float startTime = Time.time;

        while (Time.time < startTime + dashDuration)
        {
            transform.position += dashDirection * dashSpeed * Time.deltaTime;
            yield return null;
        }

        isDashing = false;
    }
}
