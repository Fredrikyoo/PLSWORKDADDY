using System.Net;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupSimple : MonoBehaviour
{
    public GameObject Popup;
    public GameObject Tagname;
    public GameObject Toptekst;
    TextMeshProUGUI TagnameText;
    TextMeshProUGUI ToptekstText;

    string[] textPopup = {"ring ambulanse!","Ikke skyt","MAMMA!"};
    string[] topTextPopup = {"HJEELP","Vær så snill",""};

    void Start(){
        Popup.SetActive(false);
        TagnameText = Tagname.GetComponent<TextMeshProUGUI>();
        ToptekstText = Toptekst.GetComponent<TextMeshProUGUI>();
    }
    void OnTriggerEnter(Collider other){
        Popup.SetActive(true);
        if(gameObject.name == "ID1"){
            ToptekstText.text = topTextPopup[0];
            TagnameText.text = textPopup[0];
        }
        if(gameObject.name == "ID2"){
            ToptekstText.text = topTextPopup[1];
            TagnameText.text = textPopup[1];
        }
        if(gameObject.name == "ID3"){
            ToptekstText.text = topTextPopup[2];
            TagnameText.text = textPopup[2];
        }
        if(Input.GetKey("r")){
            TagnameText.color = new Color32(222, 41, 22, 255);
        }
    }
    void OnTriggerExit(Collider other){
        Popup.SetActive(false);
        TagnameText.color = new Color32(0, 0, 0, 255);
    }
}