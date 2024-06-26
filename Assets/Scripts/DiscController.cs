using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D m_rb;

    [Header("Value")]
    [SerializeField] private float m_speed = 10;
    
    [Header("Rotattion")]
    [SerializeField] private Transform m_rotationHandler;
    [SerializeField] private float m_rotationSpeed = 25;

    [Header("Dash")]
    [SerializeField] private bool m_isDashing = false;
    [SerializeField] private ParticleSystem m_trailVFX;
    [SerializeField] private CharaController m_charaController;
    [SerializeField] private CircleCollider2D m_hitBox;

    [Header("Color")]
    [SerializeField] private SpriteRenderer circle1;
    [SerializeField] private SpriteRenderer circle2;
    [SerializeField] private SpriteRenderer triangle1;
    [SerializeField] private SpriteRenderer triangle2;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Launch());
    }

    private IEnumerator Launch()
    {
        circle1.color = new Vector4(circle1.color.r, circle1.color.g, circle1.color.b, 0.4f);
        circle2.color = new Vector4(circle2.color.r, circle2.color.g, circle2.color.b, 0.4f);
        triangle1.color = new Vector4(triangle1.color.r, triangle1.color.g, triangle1.color.b, 0.4f);
        triangle2.color = new Vector4(triangle2.color.r, triangle2.color.g, triangle2.color.b, 0.4f);
        m_hitBox.enabled = false;

        m_rb.simulated = false;
        yield return new WaitForSeconds(3f);

        circle1.color = new Vector4(circle1.color.r, circle1.color.g, circle1.color.b, 1f);
        circle2.color = new Vector4(circle2.color.r, circle2.color.g, circle2.color.b, 1f);
        triangle1.color = new Vector4(triangle1.color.r, triangle1.color.g, triangle1.color.b, 1f);
        triangle2.color = new Vector4(triangle2.color.r, triangle2.color.g, triangle2.color.b, 1f);
        m_rb.simulated = true;
        m_hitBox.enabled = true;
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        m_rb.AddForce(randomDir * m_speed, ForceMode2D.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
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
}
