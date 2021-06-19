using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;

enum SimonColors
{
    Red,
    Orange,
    Yellow,
    Green,
    Blue,
    Cyan,
    Purple,
    Magenta
}
enum Drinks
{
    Club_Soda,
    Cognac, 
    Cola,
    Fruit_Juice,
    Gin,
    Ginger_Ale,
    Lemons, //no this is not a drink. fuck you
    Rum,
    Tequila,
    Tonic,
    Vodka, 
    Whiskey
}

public class SimonIsAnAlcoholicScript : MonoBehaviour {

    public KMBombInfo Bomb;
    public KMAudio Audio;
    public Material[] offColors;
    public Material[] onColors;
    public KMSelectable[] buttons;
    public KMSelectable submit;
    public SpriteRenderer[] buttonSprites;
    public Sprite[] sprites;
    public TextMesh screen;
    public GameObject meter;
    public AudioClip[] allBeeps;


    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    SimonColors[] colors = Enumerable.Range(0, 8).Select(x => (SimonColors)x).ToArray();
    Drinks[] drinks = Enumerable.Range(0, 12).Select(x => (Drinks)x).ToArray();

    Drinks[] chosenSprites = new Drinks[8];
    SimonColors[] chosenColors = new SimonColors[8];

    bool[] solution = new bool[8];
    bool[] submission = new bool[8];
    Coroutine[] flashCoroutines = new Coroutine[8];

    int BAC = 0;

    void Awake ()
    {
        moduleId = moduleIdCounter++;
        foreach (KMSelectable button in buttons)
        {
            button.OnInteract += delegate () { Press(Array.IndexOf(buttons, button)); return false; };
        }
        submit.OnInteract += delegate () { Submit(); return false; };

    }

    void Press(int pos)
    {
        for (int i = 0; i < 8; i++)
            if (flashCoroutines[i] != null)
                StopCoroutine(flashCoroutines[i]);
        StartCoroutine(ButtonFlash(pos, true, true));
    }

    void Start()
    {
        SetColors();
        SetDrinks();
    }

    void SetColors()
    {
        chosenColors = colors.Shuffle();
        for (int i = 0; i < 8; i++)
        {
            buttons[i].GetComponent<MeshRenderer>().material = offColors[(int)chosenColors[i]];
        }
        Debug.LogFormat("[Simon is an Alcoholic #{0}] The button colors are {1}.", moduleId, chosenColors.Select(x => x.ToString()).Join(", "));
    }
    void SetDrinks()
    {
        chosenSprites = drinks.Shuffle();
        for (int i = 0; i < 8; i++)
        {
            buttonSprites[i].sprite = sprites[(int)chosenSprites[i]];
        }
        Debug.LogFormat("[Simon is an Alcoholic #{0}] The displayed items are {1}.", moduleId, chosenSprites.Select(x => x.ToString().Replace('_', ' ')).Join(", "));
    }

    void Submit()
    {

    }

    IEnumerator ButtonFlash(int pos, bool sound, bool stay)
    {
        if (sound && !submission[pos])
        {
            int drunkLevel = BAC - 7 < 0 ? 0 : (BAC - 7) / 8 + 1;
            Audio.PlaySoundAtTransform(allBeeps[8*drunkLevel + (int)chosenColors[pos]].name, buttons[pos].transform);
        }
        submission[pos] = !submission[pos];
        Material[] usingCols = submission[pos] ? onColors : offColors;
        buttons[pos].GetComponent<MeshRenderer>().material = usingCols[(int)chosenColors[pos]];
        yield return null;
    }

    #pragma warning disable 414
    private readonly string TwitchHelpMessage = @"Use !{0} to do something.";
    #pragma warning restore 414

    IEnumerator ProcessTwitchCommand (string input)
    {
      yield return null;
    }

    IEnumerator TwitchHandleForcedSolve ()
    {
      yield return null;
    }
}
