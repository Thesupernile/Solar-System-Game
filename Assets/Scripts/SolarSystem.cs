using UnityEngine;
using System;
using System.Collections.Generic;

public class SolarSystem : MonoBehaviour
{
    // Main Variables

    // Celestial Bodies
    public List<GameObject> celestialBodies = new List<GameObject>();
    public List<Vector3> velocities = new List<Vector3>();

    // Gravity
    public float GravitationalConstant = 6.67e-11f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Apply a force to accelerate the celestial bodies up to speed
        for (int i = 0; i < celestialBodies.Count; i++)
        {
            Rigidbody rb = celestialBodies[i].GetComponent<Rigidbody>();
            rb.linearVelocity = velocities[i];
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 closestBodyDistance = new Vector3(10000, 10000, 10000);
        for (int i = 0; i < celestialBodies.Count; i++)
        {
            GameObject targetCelestialBody = celestialBodies[i];
            // Using the universal law of gravitation, attract each of the celestial bodies to each of the other bodies
            foreach (GameObject secondaryCelestialBody in celestialBodies)
            {
                if (secondaryCelestialBody != targetCelestialBody)
                {
                    // Finds the magnitude of the gravitational force
                    Vector3 r = secondaryCelestialBody.transform.position - targetCelestialBody.transform.position;
                    float distanceSquared = r.sqrMagnitude;

                    float m1 = targetCelestialBody.GetComponent<Rigidbody>().mass;
                    float m2 = secondaryCelestialBody.GetComponent<Rigidbody>().mass;

                    float forceMagnitude = GravitationalConstant * (m1 * m2 / distanceSquared);

                    // Uses the unit vector of the distance to calculate the force vector
                    Vector3 force = r.normalized * forceMagnitude;

                    // Add the force experienced to the target rigid body
                    targetCelestialBody.GetComponent<Rigidbody>().AddForce(force, ForceMode.Force);

                    // If the other body is the player, we compare the distance
                    if (targetCelestialBody.CompareTag("Player") && (closestBodyDistance != null || r.magnitude < closestBodyDistance.magnitude))
                    {
                        closestBodyDistance = r;
                        Debug.Log($"Player accelerated by: {force.x}, {force.y}, {force.z}");
                    }

                    // Debug

                    /*Debug.Log($"Force mag. : {forceMagnitude}");
                    Debug.Log($"Centres distance: {distance}");
                    Debug.Log($"Mass 1: {m1} Mass 2: {m2}");
                    Debug.Log($"Force: {force.x}, {force.y}, {force.z} \n");
                    Debug.Log($"Velocity: {velocities[i].x}, {velocities[i].y}, {velocities[i].z}");
                    */
                }
            }
        }

        Player.closestBodyDistance = closestBodyDistance;
    }
}


// Solar system concept
/*
    One sun in the centre, with a mass of: 2e12 and a radius of 1km
    A planet that is 2km from the centre and has a mass of: 6e6, radius of 250m and a starting velocity of 36.5m/s

    Issues: the planets don't seem to like staying in a stable orbit. I've tried using the equation ... (I need to use delta time don't I???)
    Fixed that issue, there's a max mass of 1e9
*/