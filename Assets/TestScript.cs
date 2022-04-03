using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestScript : MonoBehaviour
{
    [SerializeField]
    private Button button;
    [SerializeField]
    private GameObject gameObject1;
    [SerializeField]
    private GameObject card1; 
    private void Start()
    {
        TestIncident();
    }
    public void TestIncident()
    {
        List<Incident> incidents = new List<Incident>();
        Incident incident1 = new Incident("1", IncidentType.Main, 1, "1", "1", "1", "1", 1, 1, " (1,10) ");
        Incident incident2 = new Incident("2", IncidentType.Daily , 2, "1", "1", "1", "1", 1, 1, " (1,10) ");
        Incident incident3 = new Incident("3", IncidentType.Main, 2, "2", "1", "1", "1", 1, 1, " (1,10) ");
        incidents.Add(incident1);
        incidents.Add(incident2);
        incidents.Add(incident3);
        IncidentSystem.Instance.AddIncident(incidents);
        IncidentSystem.Instance.CheckIncident();

    }
}
