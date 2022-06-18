using System.Collections.Generic;

public class Place
{
    public string name;
    public List<Incident> incidents = new List<Incident>();

    public Place() { }

    public Place(PlaceInfo info)
    {
        name = info.name;
        incidents = info.incidents.ConvertAll(x => new Incident(x));
    }
    public Place(Place origin)
    {
        name = origin.name;
        incidents = origin.incidents.ConvertAll(x => new Incident(x));
    }
}


