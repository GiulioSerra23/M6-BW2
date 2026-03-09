using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Readme", menuName = "RunnerRogueLite/Readme")]
public class Readme : ScriptableObject
{
    public Texture2D icon;
    public string title;
    public Section[] sections = new Section[0];
    public bool loadedLayout;

    [Serializable]
    public class Section
    {
        public string heading, text, linkText, url;
    }
}


/*
icon: Un riferimento a un'immagine (es. il logo del vostro team).
title: Il titolo grande che apparirà in cima.
loadedLayout: Una variabile di controllo (booleana) che probabilmente lo script Editor usa per sapere se deve applicare una disposizione specifica alle finestre di Unity.
*/