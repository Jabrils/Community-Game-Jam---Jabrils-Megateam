using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static class DialogueSystem
{
    public enum State { Finished, InUse, End };
    public static State state;
    public static int location = 0;
    public static GameObject chatbox;
    public static TextMeshProUGUI displayText;
    public static Image mugShot, caret;


    public static void Init(GameObject CHATBOX)
    {
        chatbox = CHATBOX;

        displayText = chatbox.GetComponentInChildren<TextMeshProUGUI>();
        Image[] allImgsInChatbox = chatbox.GetComponentsInChildren<Image>();

        mugShot = allImgsInChatbox[1];
        caret = allImgsInChatbox[2];
    }

    public static void Chat(Dialogue[] d, Action action)
    {
        // 
        if (state == State.Finished)
        {
            chatbox.SetActive(true);
            state = State.InUse;
            mugShot.sprite = d[location].mugshot;
            displayText.text = (d[location].dia);
        }
        else if (state == State.InUse)
        {
            location++;
            mugShot.sprite = d[location].mugshot;
            displayText.text = (d[location].dia);
        }
        else if (state == State.End)
        {
            state = State.Finished;
            chatbox.SetActive(false);

            // 
            if (action.quest != null)
            {
                action.quest.progress = action.progressTo;
            }

            return;
        }

        // 
        if (state != State.End && location == d.Length-1)
        {
            location = 0;
            state = State.End;
        }


        // 
        caret.color = (state == State.End) ? Color.red : Color.blue;
    }
}

[System.Serializable]
public class Dialogue
{
    public Sprite mugshot;
    public string dia;

    public Dialogue(Sprite spr, string dia)
    {
        this.mugshot = spr;
        this.dia = dia;
    }
}

[System.Serializable]
public class DialogueTimeline
{
    public int progress { get { return quest == null ? 0 : quest.progress; }}
    public bool willTalk { get { return convo.Length > 0; } }
    public Quest quest;
    public Conversation[] convo;
}

[CreateAssetMenu(fileName = "Conversation", menuName = "Story/Conversation", order = 1)]
public class Conversation : ScriptableObject
{
    public int match;
    public Dialogue[] lines;
    public Action action;
}