using System.Collections.Generic;

public class Map
{
    public List<Place> placesDic = new List<Place>();


    public Map(MapInfo info)
    {
        placesDic = info.places.ConvertAll(x => new Place(x));
    }

    public bool TryFindIncident(string name, out Incident res)
    {
        foreach (Place place in placesDic)
            foreach (Incident incident in place.incidents)
                if (incident.incidentName.Equals(name))
                {
                    res = incident;
                    return true;
                }
        res = null;
        return false;
    }
}
