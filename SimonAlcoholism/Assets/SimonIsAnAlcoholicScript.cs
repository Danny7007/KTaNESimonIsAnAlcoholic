using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;

public class SimonIsAnAlcoholicScript : MonoBehaviour {

    public KMBombInfo Bomb;
    public KMAudio Audio;
    public Material[] offButtons;
    public Material[] onButtons;
    public KMSelectable[] buttons;
    public KMSelectable submit;
    public SpriteRenderer[] buttonSprites;
    public Sprite[] sprites;
    public TextMesh screen;
    public GameObject meter;
    public AudioClip[] drunk0, drunk1, drunk2, drunk3;
    private AudioClip[][] allBeeps;


    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    int[] colorNums = new int[] { 0, 1, 2, 3, 4, 5, 6, 7 };
    int[] allSprites = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
    int[] chosenSprites;
    bool[] solution = new bool[8];
    bool[] submission = new bool[8];

    void Awake ()
    {
        allBeeps = new AudioClip[][] { drunk0, drunk1, drunk2, drunk3 };
        moduleId = moduleIdCounter++;
        foreach (KMSelectable button in buttons)
        {
            button.OnInteract += delegate () { Press(button); return false; };
        }
        submit.OnInteract += delegate () { Submit(); return false; };

    }

    void Press(KMSelectable pressed)
    {

    }

    void Start()
    {

    }

    void Submit()
    {

    }

    #pragma warning disable 414
    private readonly string TwitchHelpMessage = @"Use !{0} to do something.";
    #pragma warning restore 414

    IEnumerator ProcessTwitchCommand (string Command) {
      yield return null;
    }

    IEnumerator TwitchHandleForcedSolve () {
      yield return null;
    }
}
