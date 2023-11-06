using System.Net;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlarmOverView : MonoBehaviour
{
    public GameObject StableT;
    public GameObject AHT;
    public GameObject AHHT;
    public GameObject FaultT;
    public GameObject Component1;
    public GameObject Component2;
    public GameObject Component3;
    public GameObject Component4;
    public GameObject Component5;
    public GameObject Component1Parent;
    public GameObject Component2Parent;
    public GameObject Component3Parent;
    public GameObject Component4Parent;
    public GameObject Component5Parent;
    

    public string phpURL;
    private string url;                         //privat custom url string */
    
    TextMeshProUGUI StableText;
    TextMeshProUGUI AHText;    
    TextMeshProUGUI AHHText;    
    TextMeshProUGUI FaultText;        
    private string activated;            
    
    void Start()
    {
        StableText = StableT.GetComponent<TextMeshProUGUI>();
        AHText = AHT.GetComponent<TextMeshProUGUI>();
        AHHText = AHHT.GetComponent<TextMeshProUGUI>();
        FaultText = FaultT.GetComponent<TextMeshProUGUI>();

        gameObject.GetComponent<Renderer>().material.color = new Color32(10, 10, 10, 255);
        StableText.text = "";
        AHText.text = "";
        AHHText.text = "";
        FaultText.text = "";

        activated = "false";
    }

    
    void OnTriggerEnter(Collider other)
    {
        string val1 = GetValues(Component1);
        string val2 = GetValues(Component2);
        string val3 = GetValues(Component3);
        string val4 = GetValues(Component4);
        string val5 = GetValues(Component5);

        string[] Components = {Component1.name,Component2.name,Component3.name,Component4.name,Component5.name};
        string[] ComponentParents = {Component1Parent.name,Component2Parent.name,Component3Parent.name,Component4Parent.name,Component5Parent.name};
        string[] measuredValues = {val1, val2, val3, val4, val5};
        TextMeshProUGUI[] CanvasTexts = {StableText,AHText,AHHText,FaultText};

        if(activated == "false"){
            int stableValues = 0;
            int highValues = 0;
            int veryHighValues = 0;
            int faultValues = 0;

            for(int i = 0; i < 5; i++){
                if (measuredValues[i] == "0"){
                    stableValues += 1;
                } if (measuredValues[i] == "1"){
                    highValues += 1;
                    AHText.color = new Color32(220, 220, 35, 255);
                } if (measuredValues[i] == "2"){
                    veryHighValues += 1;
                    AHHText.color = new Color32(220, 220, 35, 255);
                } if (measuredValues[i] == "3"){
                    faultValues += 1;
                    FaultText.color = new Color32(220, 10, 10, 255);
                }
            }

            activated = "p1";
            gameObject.GetComponent<Renderer>().material.color = new Color32(250, 250, 250, 255);
            StableText.text = "System Stable: " + stableValues.ToString();
            AHText.text = "System AH: " + highValues.ToString();
            AHHText.text = "System AHH: " + veryHighValues.ToString();
            FaultText.text = "System Fault: " + faultValues.ToString();
        }
        else if(activated == "p1"){
            bool loop = true;
            int CountFaultAlamrms = 0;
            int CountHighHighAlamrms = 0;
            int CountHighAlamrms = 0;
            int CountTot = 0;
            activated = "p2";
            for(int j = 0; j < 5; j++){
                if(CountTot > 4){
                    Debug.Log("there are more alarms not displayed");
                }
                else if(measuredValues[j] == "3"){
                    CanvasTexts[CountTot].text = ComponentParents[j] + " Fault";
                    CountFaultAlamrms += 1;
                    CountTot += 1;
                }
                else if(measuredValues[j] == "2"){
                    CanvasTexts[CountTot].text = ComponentParents[j] + " HighHigh";
                    CountHighHighAlamrms += 1;
                    CountTot += 1;
                }
                else if(measuredValues[j] == "1"){
                    CanvasTexts[CountTot].text = ComponentParents[j] + " High";
                    CountHighAlamrms += 1;
                    CountTot += 1;
                }
                CanvasTexts[CountTot].color = new Color32(250, 250, 250, 255); 
            }
            while (loop == true){
                if(CountTot < 4){
                    CanvasTexts[CountTot].text = "";
                    CountTot += 1;
                } else {
                    break;
                }
            }
        }
        else {
            activated = "false";
            gameObject.GetComponent<Renderer>().material.color = new Color32(10, 10, 10, 255);
            StableText.text = "";
            AHText.text = "";
            AHHText.text = "";
            FaultText.text = "";
        }
    }
    private string GetValues(GameObject Component){
        string measurements = GetMeasurementFromDatabase(Component);            //henter målinger
        string value1 = GetMeasurementByIndex(measurements,1);                  // finner AH og AHH
        string value2 = GetMeasurementByIndex(measurements,2);
        int value = int.Parse(value1) + int.Parse(value2);                      //regner tot varsler
        if(int.Parse(value1) < int.Parse(value2)){                              //sjekker for error
            return "3";
        } else {
            return value.ToString();
        }
    }
    private string GetMeasurementByIndex(string measurements,int index){        //func som henter fra databsen
        string[] parts = measurements.Split(',');                               //splitter data opp i deler i parts
        return parts[index];                                                    //gir tilbaake valgte måling
    }
    private string GetMeasurementFromDatabase(GameObject Component){                                //func som henter all måling
        url = phpURL + "?name=" + Component.name + "&amount=1";                //lager ulr
        string response;
        using (WebClient client = new WebClient()){
            response = client.DownloadString(url);                              //ved å bruke konstruert url
        }
        return response;
    }
}
