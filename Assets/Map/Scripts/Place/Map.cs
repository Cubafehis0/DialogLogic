using System.Collections.Generic;

public class Map
{
    public List<Place> placesDic = new List<Place>();
    public Map(MapInfo info)
    {
        placesDic = info.places.ConvertAll(x => new Place(x));
    }
}
