using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neighborhood : MonoBehaviour
{
    [Header("Set Dynamically")]
    public List<Boid> neidhbors;
    private SphereCollider coll;
    void Start()
    {
        neidhbors = new List<Boid>();
        coll = GetComponent<SphereCollider>();
        coll.radius = Spawner.S.neighborDis / 2;
    }
    private void FixedUpdate()
    {
        if (coll.radius != Spawner.S.neighborDis / 2)
            coll.radius = Spawner.S.neighborDis / 2;
    }
    private void OnTriggerEnter(Collider other)
    {
        Boid b = other.GetComponent<Boid>();
        if (b != null)
            if (neidhbors.IndexOf(b) == -1)
                neidhbors.Add(b);
    }

    private void OnTriggerExit(Collider other)
    {
        Boid b = other.GetComponent<Boid>();
        if (b != null)
            if (neidhbors.IndexOf(b) != -1)
                neidhbors.Remove(b);
    }

    public Vector3 avgPos
    {
        get { Vector3 avg = Vector3.zero;
            if (neidhbors.Count == 0) return avg;

            for (int i = 0; i < neidhbors.Count; i++)
            {
                avg += neidhbors[i].pos;
            }
            avg /= neidhbors.Count;
            return avg;
        }
    }

    public Vector3 avgVel
    {
        get 
        {
            Vector3 avg = Vector3.zero;
            if (neidhbors.Count == 0)
                return avg;
            for (int i = 0; i < neidhbors.Count; i++)
            {
                avg += neidhbors[i].rigid.velocity;
            }
            avg /= neidhbors.Count;
            return avg;
        }
    }

    public Vector3 avgClosePos 
    {
        get 
        {
            Vector3 avg = Vector3.zero;
            Vector3 delta;
            int nearCount = 0;
            for (int i = 0; i < neidhbors.Count; i++)
            {
                delta = neidhbors[i].pos - transform.position;
                if (delta.magnitude <= Spawner.S.collDist) 
                {
                    avg += neidhbors[i].pos;
                    nearCount++;
                }
            }
            if (nearCount == 0)
                return avg;

            avg /= nearCount;
            return avg;
        
        }
    }
}
