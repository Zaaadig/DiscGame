using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderDisc : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D m_rbed;

    [Header("Value")]
    [SerializeField] private float m_speeded = 10;

    [Header("Rotattion")]
    [SerializeField] private Transform m_rotationHandlered;
    [SerializeField] private float m_rotationSpeeded = 25;

    [Header("Dash")]
    [SerializeField] private bool m_isDashinged = false;
    [SerializeField] private ParticleSystem m_trailVFX;
    [SerializeField] private CharaController m_charaController;
    [SerializeField] private CircleCollider2D m_hitBoxed;

    // Start is called before the first frame update
    void Start()
    {
        Launched();
    }

    private void Launched()
    {

        Vector2 randomDir = Random.insideUnitCircle.normalized;
        m_rbed.AddForce(randomDir * m_speeded, ForceMode2D.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        m_rotationHandlered.Rotate(Vector3.forward * m_rotationSpeeded * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && !m_isDashinged)
        {
            StartCoroutine(Invincibilityed());
        }
    }

    private IEnumerator Invincibilityed()
    {
        m_hitBoxed.enabled = false;

        yield return new WaitForSeconds(0.4f);
        m_hitBoxed.enabled = true;
    }
}
