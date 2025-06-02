using UnityEngine;
using System;

public class SolarSystem : MonoBehaviour
{
    // Main Variables

    // Celestial Bodies
    public GameObject[] celestialBodyTemplates;
    public Vector3[] startingVelocities;
    public Vector3[] startingPositions;
    public Vector3[] startingSize;
    public int[] startingMasses;

    private GameObject[] celestialBodies;

    // Gravity
    public const float GravitationalConstant = 6.67e-11f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Create all the celestial bodies that we need to (each body needs a radius, a position, a mass and a velocity)
        // Values below are temporary and subject to change later :)
        // Creation of the sun
        for (int i = 0; i < celestialBodies.Length; i++)
        {
            GameObject body = Instantiate(celestialBodyTemplates[i]);
            body.transform.position = startingPositions[i];
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // Using the universal law of gravitation, attract each of the celestial bodies to each of the other bodies
        foreach (GameObject targetCelestialBody in celestialBodies) {
            foreach (GameObject secondaryCelestialBody in celestialBodies)
            {
                // Finds the magnitude of the gravitational force
                Vector3 r = secondaryCelestialBody.transform.position - targetCelestialBody.transform.position;
                float distanceSquared = (float)(Math.Pow(r.x, 2.0f) + Math.Pow(r.y, 2.0f) + Math.Pow(r.z, 2.0f));

                float m1 = targetCelestialBody.GetComponent<Rigidbody>().mass;
                float m2 = secondaryCelestialBody.GetComponent<Rigidbody>().mass;

                float forceMagnitude = GravitationalConstant * (m1 * m2) / distanceSquared;

                // Uses the unit vector of the distance to calculate the force vector
                float distance = (float)(Math.Sqrt(distanceSquared));
                Vector3 force = new Vector3((r.x * forceMagnitude / distance), (r.y * forceMagnitude / distance), (r.z * forceMagnitude / distance));

                // Moves the first celestial body accordingly
                targetCelestialBody.transform.position += force;
            }
        }
    }
}
