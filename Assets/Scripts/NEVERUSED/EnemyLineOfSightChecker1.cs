using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class EnemyLineOfSightChecker1 : MonoBehaviour
{
    public SphereCollider Collider;
    public float FieldOfView = 90f;
    public LayerMask LineOfSightLayers;

    public delegate void GainSightEvent(Transform Target);
    public GainSightEvent OnGainSight;
    
    public delegate void LooseSightEvent(Transform Target);
    public LooseSightEvent OnLooseSight;

    private Coroutine CheckForLineOfSightCourountine;


    private void Awake()
    {
        Collider = GetComponent<SphereCollider>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!CheckLineOfSight(other.transform))
        {
            CheckForLineOfSightCourountine = StartCoroutine(CheckForLineOfSight(other.transform));     
        }
    }

    private void OnTriggerExit(Collider other)
    {
        OnLooseSight?.Invoke(other.transform);
        if (CheckForLineOfSightCourountine != null)
        {
            StopCoroutine(CheckForLineOfSightCourountine);
        }
    }

    private bool CheckLineOfSight (Transform target)
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        float dotProduct = Vector3.Dot(transform.forward, direction);

        if (dotProduct >= Mathf.Cos(FieldOfView))
        {
            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, Collider.radius, LineOfSightLayers))
            {
                OnGainSight?.Invoke(target);
                return true;
            }
        }

        return false;
    }

    private IEnumerator CheckForLineOfSight(Transform target)
    {
        WaitForSeconds Wait = new WaitForSeconds(0.5f);

        while (!CheckLineOfSight(target))
        {
            yield return Wait;
        }
    }
}
