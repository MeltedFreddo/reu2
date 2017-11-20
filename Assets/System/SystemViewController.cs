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
            float radius = i / 2f;
            float angle = i * Mathf.PI * 2 / 5;
            Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            planets.Add((GameObject)Instantiate(PlanetPrefab, pos, PlanetPrefab.transform.rotation));

            //planets.Add(Instantiate(planet, new Vector3(i + 5, i, 0), Quaternion.identity));
        }
    }

    // Update is called once per frame
    void Update()
    {
        //TODO orbit planets around star
        foreach (var planet in planets)
        {
            planet.transform.RotateAround(theStar.transform.position, new Vector3(0, 0, -1), (float)((Random.value - 0.5) * 30 + 60) * Time.deltaTime); // (1 is left) (-1 is right)
            planet.transform.rotation = PlanetPrefab.transform.rotation;
        }
    }

}
