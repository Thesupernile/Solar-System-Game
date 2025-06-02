using UnityEngine;
using System;
using System.Collections.Generic;

public class SolarSystem : MonoBehaviour
{
    // Main Variables

    // Celestial Bodies
    public GameObject[] celestialBodyTemplates;
    public Vector3[] startingPositions;
    public Vector3[] startingSize;
    public float[] startingMasses;

    private List<GameObject> celestialBodies = new List<GameObject>();
    public List<Vector3> velocities = new List<Vector3>();

    // Gravity
    public float GravitationalConstant = 6.67e-11f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Create all the celestial bodies that we need to (each body needs a radius, a position, a mass and a velocity)
        // Values below are temporary and subject to change later :)
        // Creation of the sun
        for (int i = 0; i < celestialBodyTemplates.Length; i++)
        {
            GameObject body = Instantiate(celestialBodyTemplates[i], this.transform);
            body.SetActive(true);
            body.transform.position = startingPositions[i];
            body.transform.localScale = startingSize[i];
            celestialBodies.Add(body);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < celestialBodies.Count; i++)
        {
            GameObject targetCelestialBody = celestialBodies[i];
            // Using the universal law of gravitation, attract each of the celestial bodies to each of the other bodies
            foreach (GameObject secondaryCelestialBody in celestialBodies)
            {
                if (secondaryCelestialBody != targetCelestialBody) {
                    // Finds the magnitude of the gravitational force
                    Vector3 r = secondaryCelestialBody.transform.position - targetCelestialBody.transform.position;
                    float distanceSquared = (float)(Math.Pow(r.x, 2.0f) + Math.Pow(r.y, 2.0f) + Math.Pow(r.z, 2.0f));

                    float m1 = targetCelestialBody.GetComponent<Rigidbody>().mass;
                    float m2 = secondaryCelestialBody.GetComponent<Rigidbody>().mass;

                    float forceMagnitude = GravitationalConstant * (m1 * m2) / distanceSquared;

                    // Uses the unit vector of the distance to calculate the force vector
                    float distance = (float)(Math.Sqrt(distanceSquared));
                    Vector3 force = r * forceMagnitude / distance;

                    // Changes the first celestial body's velocity accordingly F=ma => F/m = a
                    velocities[i] += force / m1;

                    // Output a ton of debug stuff
                    /*Debug.Log($"Force mag. : {forceMagnitude}");
                    Debug.Log($"Centres distance: {distance}");
                    Debug.Log($"Mass 1: {m1} Mass 2: {m2}");
                    Debug.Log($"{force.x}, {force.y}, {force.z} \n");*/
                }
            }
            // Move the target body by it's curret velocity
            targetCelestialBody.transform.position += velocities[i];
        }
    }
}
