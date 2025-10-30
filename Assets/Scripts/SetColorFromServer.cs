using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Networking;
using static System.Net.WebRequestMethods;





#if UNITY_EDITOR

using UnityEditor;
#endif

public class ObjectColor
{
    public string title;
    public string color;
}

public class SetColorFromServer : MonoBehaviour
{
    public ServerLogin login;
    public string hostname = "http://127.0.0.1:8000/objects/";

    public float fetchFrequency = 0.25f;
    [HideInInspector] public Color color = Color.white;

    private Coroutine fetchCoR = null;

    void Start()
    {
#if UNITY_EDITOR
        if (login == null)
        {
            Debug.Log("Login info is required!");
            EditorApplication.ExitPlaymode();
        }
#endif
        fetchCoR = StartCoroutine(GetColor());

    }

    private IEnumerator GetColor()
    {
        float fetchStartTime = Time.realtimeSinceStartup;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(hostname))
        {
            yield return webRequest.SendWebRequest();

            SetColor(ParseColor(webRequest.downloadHandler.text));
        }
        float fetchDuration = Time.realtimeSinceStartup - fetchStartTime;
        float fetchDelayRemaining = fetchFrequency - fetchDuration;

        // Null out the fetchCoR so it gets a fresh color
        yield return new WaitForSeconds(fetchDelayRemaining > 0 ? fetchDelayRemaining : 0);
        fetchCoR = null;

    }

    private Color ParseColor(string text)
    {
        // Temp data digestion, only works for 1 entry lists like this
        string parsedEntries = text.Substring(text.IndexOf('[') + 1, text.LastIndexOf(']') - text.IndexOf('[') - 1);
        ObjectColor parsedObject = JsonUtility.FromJson<ObjectColor>(parsedEntries);

        Color returnColor;
        if (!ColorUtility.TryParseHtmlString($"#{parsedObject.color}", out returnColor))
        {
            Debug.LogError($"Could not parse {parsedObject.color} as a color");
        }

        return returnColor;
    }

    private void SetColor(Color color)
    {
        GetComponent<MeshRenderer>().material.SetColor("_BaseColor", color);
    }

    void Update()
    {
        // Make sure we have a pending fetch color coroutine
        if (fetchCoR == null)
        {
            fetchCoR = StartCoroutine(GetColor());
        }
    }
}
