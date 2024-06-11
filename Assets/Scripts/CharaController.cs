using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharaController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_rb;
    [SerializeField] private float m_acceleration = 10;
    [SerializeField] private float m_maxSpeed = 5;
    [SerializeField] private float m_defaultDrag = 8;
    [SerializeField] private float m_movementDrag = 0;
    [SerializeField] private float m_dashForce = 20;
    [SerializeField] private float m_dashDuration = 0.2f;
    [SerializeField] private ParticleSystem m_trailVFX;
    [SerializeField] private CharaController m_charaController;
    [SerializeField] private Timer m_timer;

    public GameObject m_player;

    [Header("VFX")]
    [SerializeField] private ParticleSystem m_deathVFX;

    private bool m_isAlive = true;
    private bool m_isDashing = false;

    private Vector2 m_currentInput;
    private bool m_isMoving = false;

    public GameObject button;
    public GameObject button1;

    private void Update()
    {
        SetDrag();

        m_currentInput.x = Input.GetAxisRaw("Horizontal");
        m_currentInput.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && !m_isDashing)
        {
            StartCoroutine(Dash());
        }

        if (m_currentInput != Vector2.zero && !m_isDashing)
            StartMoving();
        else
            StopMoving();
    }

    private IEnumerator Dash()
    {       
        m_isDashing = true;
        m_rb.velocity = Vector2.zero;
        m_trailVFX.Play();

        m_rb.AddForce(m_currentInput.normalized * m_dashForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(m_dashDuration);

        m_isDashing = false;

    }

    private void FixedUpdate()
    {
        if (!m_isDashing && m_currentInput != Vector2.zero)
        {
            float reduceFactor = 0.9f;
            if (Mathf.Sign(m_currentInput.x) != Mathf.Sign(m_rb.velocity.x))
                m_rb.velocity = new Vector2(m_rb.velocity.x * reduceFactor, m_rb.velocity.y);

            if (Mathf.Sign(m_currentInput.y) != Mathf.Sign(m_rb.velocity.y))
                m_rb.velocity = new Vector2(m_rb.velocity.x, m_rb.velocity.y * reduceFactor);

            m_rb.AddForce(m_currentInput * m_acceleration, ForceMode2D.Force);

            m_rb.velocity = Vector2.ClampMagnitude(m_rb.velocity, m_maxSpeed);
        }
    }
    public void TakeDamage3(BorderDisc disc3)
    {
        KillChara();
    }

    public void TakeDamage2(EnemyMovement disc2)
    {
        KillChara();
    }

    public void TakeDamage(DiscController disc)
    {
        KillChara();
    }

    public void KillChara()
    {
        m_deathVFX.Play();
        m_isAlive = false;
        m_charaController.enabled = false;
        m_rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        m_rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        m_timer.timeIsRunning = false;
        StartCoroutine(LoadScene());

    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1.5f);
        button.SetActive(true);
        button1.SetActive(true);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Disc"))
        {
            DiscController disc = collision.GetComponentInParent<DiscController>();
            EnemyMovement disc2 = collision.GetComponentInParent<EnemyMovement>();
            BorderDisc disc3 = collision.GetComponentInParent<BorderDisc>();
            if (disc)
            {
                TakeDamage(disc);
            }

            if (disc2)
            {
                TakeDamage2(disc2);
            }

            if (disc3)
            {
                TakeDamage3(disc3);
            }
        }
    }

    private void StartMoving()
    {
        if (m_isMoving) return;

        m_isMoving = true;
    }

    private void StopMoving()
    {
        if (!m_isMoving) return;

        m_isMoving = false;
    }

    private void SetDrag()
    {
        if (m_isMoving || m_isDashing)
            m_rb.drag = m_movementDrag;
        else
            m_rb.drag = m_defaultDrag;
    }
}
