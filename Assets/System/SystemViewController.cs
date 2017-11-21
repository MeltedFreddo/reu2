using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SystemViewController : MonoBehaviour
{

    public GameObject theStar;
    public ICollection<GameObject> planets;

    public GameObject StarPrefab;
    public GameObject PlanetPrefab;

    // Use this for initialization
    void Start()
    {
        //TODO load star and planets from game state
        theStar = Instantiate(StarPrefab);

        planets = new List<GameObject>();
        for (var i = 0; i < 5; i++)
        {
            float radius = i + 3f;
            float angle = i * Mathf.PI * 2 / 5;
            Vector3 pos = new Vector3(Mathf.Cos(angle), 0, 0) * radius;
            planets.Add((GameObject)Instantiate(PlanetPrefab, pos, PlanetPrefab.transform.rotation));
            
            //planets.Add(Instantiate(planet, new Vector3(i + 5, i, 0), Quaternion.identity));
        }
    }

    // Update is called once per frame
    void Update()
    {                
        //orbit planets around star
        foreach (var planet in planets)
        {
            var planetPosition = planet.transform.position;
            var distanceFromSun = (theStar.transform.position - planetPosition).magnitude;            
            var orbitSpeed = 50 / distanceFromSun;
            planet.transform.RotateAround(theStar.transform.position, new Vector3(0, 1, 0), orbitSpeed * Time.deltaTime); // (1 is left) (-1 is right)
            planet.transform.rotation = PlanetPrefab.transform.rotation;

            //update scale
            var distanceFromZPlane = (new Vector3(planetPosition.x, planetPosition.y, 0) - planetPosition).z;           
            var prefabScale = PlanetPrefab.transform.localScale.x; //0.1
            var newScale = 0f;
            if (distanceFromZPlane != 0)
            {
                newScale = prefabScale + ConvertDistanceToScale(5, distanceFromZPlane);
            }
            planet.transform.localScale = new Vector3(newScale, newScale, newScale);

        }   
    }

    private float ConvertDistanceToScale(float maxDistance, float actualDistance)
    {
        return (((actualDistance - maxDistance * -1f) * (0.05f - -0.05f)) / (maxDistance - maxDistance * -1f)) + -0.05f;
    }

}
