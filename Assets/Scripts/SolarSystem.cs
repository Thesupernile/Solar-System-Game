using UnityEngine;
using System;

public class SolarSystem : MonoBehaviour
{
    // Temp Variable for testing
    public GameObject celestialBodyTemplate;
    // Main Variables
    public SolarSystemResources.CelestialBody[] celestialBodies;
    public const float GravitationalConstant = (6.67 * Math.Pow(10, -11));
    GameObject sun;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Create all the celestial bodies that we need to (each body needs a radius, a position, a mass and a velocity)
        // Values below are temporary and subject to change later :)
        // Creation of the sun
        sun = Instantiate(celestialBodyTemplate);
        sun.SetActive(true);
        sun.transform.localScale = new Vector3(100, 100, 100);
        sun.transform.position = new Vector3(0, 0, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        // Using the universal law of gravitation, attract each of the celestial bodies to each of the other bodies
        foreach (SolarSystemResources.CelestialBody targetCelestialBody in celestialBodies) {
            foreach (SolarSystemResources.CelestialBody secondaryCelestialBody in celestialBodies) {
                
            }
        }
        float mass = sun.GetComponent<Rigidbody>().mass;

    }
}
