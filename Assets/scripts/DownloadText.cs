using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class DownloadText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private string _textURL;
    public APImot mot;

    [System.Serializable]

    public class APImot
    {
        public string status;
        public string motChoisi;
        public int nombreDeMots;
        public int emplacementDuMotDansLeDictionnaire;
        public string regles;
    }

    private void Start()
    {
        StartCoroutine(GetText());
    }

    public string motChoisi()
    {
        return mot.motChoisi;
    }

    private IEnumerator GetText()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(_textURL))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ProtocolError || request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                Debug.Log("Successfully downloaded text");

                var text = request.downloadHandler.text;
                mot = JsonUtility.FromJson<APImot>(text);
                _text.text = mot.motChoisi;
            }
        } 
    }
}