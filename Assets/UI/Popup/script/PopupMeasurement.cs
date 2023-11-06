using System.Net;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupMeasurement : MonoBehaviour
{
    //[SerliazeField] private
    public GameObject Popup;                    //lager public varibler
    public GameObject Tagname;
    public GameObject TMP_Measurement;
    public GameObject Equipment;
    public string phpURL;
    private string url;                         //privat custom url string

    TextMeshProUGUI TagnameText;                //kobler til canvas
    TextMeshProUGUI MeasurementText;
    TextMeshProUGUI EquipmentText;

    string[] status = {"system ok", "high Alarm","high high Alarm","Fault"};    //Status array

    void Start(){
        Popup.SetActive(false);                                                 //fjener popup
        TagnameText = Tagname.GetComponent<TextMeshProUGUI>();                  //finner textobject
        MeasurementText = TMP_Measurement.GetComponent<TextMeshProUGUI>();
        EquipmentText = Equipment.GetComponent<TextMeshProUGUI>();
        url = phpURL + "?name=" + gameObject.name + "&amount=1";                //lager ulr
    }
    void OnTriggerEnter(Collider other){
        Popup.SetActive(true);                                                  //aktiverer popup
        TagnameText.text = transform.parent.name;                               //finner parent object navn
        string measurements = GetMeasurementFromDatabase();                     //henter målinger
        string value1 = GetMeasurementByIndex(measurements,1);                  // finner AH og AHH
        string value2 = GetMeasurementByIndex(measurements,2);
        int value = int.Parse(value1) + int.Parse(value2);                      //regner tot varsler
        if(int.Parse(value1) < int.Parse(value2)){                              //sjekker for error
            MeasurementText.text = status[3];                                   //skriver fault
            MeasurementText.color = new Color32(222, 41, 22, 255);              //tekst blir rød
        } else {
            MeasurementText.text = status[value];                               //velger status basert på tot varsler
        }
        EquipmentText.text = gameObject.name;                                   //utsyrnavn
    }
    private string GetMeasurementByIndex(string measurements,int index){        //func som henter fra databsen
        string[] parts = measurements.Split(',');                               //splitter data opp i deler i parts
        return parts[index];                                                    //gir tilbaake valgte måling
    }
    private string GetMeasurementFromDatabase(){                                //func som henter all måling
        string response;
        using (WebClient client = new WebClient()){
            response = client.DownloadString(url);                              //ved å bruke konstruert url
        }
        return response;
    }
    void OnTriggerExit(Collider other){                                         //går vekk fra component
        Popup.SetActive(false);                                                 //skru av popup
        MeasurementText.color = new Color32(0, 58, 255, 255);                   //tekstfarge tilbake til standar livefarge
    }
}
