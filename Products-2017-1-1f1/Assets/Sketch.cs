using UnityEngine;
using Pathfinding.Serialization.JsonFx; //make sure you include this using

public class Sketch : MonoBehaviour {
    public GameObject myPrefab;
    public string _WebsiteURL = "http://ChronoSpatial.azurewebsites.net/tables/Town?zumo-api-version=2.0.0";

    void Start () {
        //Reguest.GET can be called passing in your ODATA url as a string in the form:
        //http://{Your Site Name}.azurewebsites.net/tables/{Your Table Name}?zumo-api-version=2.0.0
        //The response produce is a JSON string
        string jsonResponse = Request.GET(_WebsiteURL);

        //Just in case something went wrong with the request we check the reponse and exit if there is no response.
        if (string.IsNullOrEmpty(jsonResponse))
        {
            return;
        }

        //We can now deserialize into an array of objects - in this case the class we created. The deserializer is smart enough to instantiate all the classes and populate the variables based on column name.
        Town[] towns = JsonReader.Deserialize<Town[]>(jsonResponse);

        //----------------------
        //YOU WILL NEED TO DECLARE SOME VARIABLES HERE SIMILAR TO THE CREATIVE CODING TUTORIAL

        int i = 0;
        int totalCubes = towns.Length;
        float totalDistance = 2.9f;
        //----------------------

        //We can now loop through the array of objects and access each object individually
        foreach (Town town in towns)
        {
            //Example of how to use the object
            Debug.Log("This towns name is: " + town.TownName);
            //----------------------
            //YOUR CODE TO INSTANTIATE NEW PREFABS GOES HERE
            float perc = i / (float)totalCubes;
            float sin = Mathf.Sin(perc * Mathf.PI / 2);

            float x = town.Latitude;
            float y = town.Altitude;
            float z = town.Longitude;

            var newCube = (GameObject)Instantiate(myPrefab, new Vector3(x, y, z), Quaternion.identity);

            newCube.GetComponent<CubeScript>().SetSize(.45f * (1.0f - perc));
            newCube.GetComponent<CubeScript>().rotateSpeed = .2f + perc * 4.0f;
            newCube.transform.Find("New Text").GetComponent<TextMesh>().text = town.TownName;//"Hullo Again";
            i++;

            //----------------------
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
