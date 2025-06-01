using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    public GameObject celestialBodyTemplate;
    GameObject sun;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Create all the celestial bodies that we need to (each body needs a radius, a position, a mass and a velocity)
        // Values below are temporary and subject to change later :)
        // Creation of the sun
        sun = Instantiate(celestialBodyTemplate);
        sun.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // Using the universal law of gravitation, attract each of the celestial bodies to each of the other bodies

    }
}
