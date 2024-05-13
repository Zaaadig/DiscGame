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

    // Start is called before the first frame update
    void Start()
    {
        Launch();
    }

    private void Launch()
    {
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
