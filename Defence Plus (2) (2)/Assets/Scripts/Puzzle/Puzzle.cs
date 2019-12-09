using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Puzzle : MonoBehaviour
{
    const int POS_INTERVAL = 170;
    const int POS_X = -515; 
    const int POS_Y = 505; 

    public int x;
    public int y;
    public int number;
    public Sprite puzzleImage;
    public List<Material> materials = new List<Material>();

    //public bool isPressed;

    public PuzzleState _state = PuzzleState.Normal;

    public Text text;
    private Image image;

	void Start ()
	{
	    text = GetComponentInChildren<Text>();
	    image = GetComponent<Image>();
	    InitPuzzle();
	}

    public void InitPuzzle()
    {
        SetPositionByPos();
        text.text = number.ToString();
        GetComponent<Image>().sprite = puzzleImage;

        InitTriggerEvent();
    }

    public void SetPositionByPos()
    {
        float pos_x = POS_X + (POS_INTERVAL * x);
        float pos_y = POS_Y - (POS_INTERVAL * y);
        transform.localPosition = new Vector3(pos_x, pos_y, 0);
    }

    #region TriggerEvent
    private void InitTriggerEvent()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();

        AddTriggerEvent(trigger, EventTriggerType.PointerEnter, PointerEnterEvent);
        AddTriggerEvent(trigger, EventTriggerType.PointerDown, PointerDownEvent);
        AddTriggerEvent(trigger, EventTriggerType.PointerUp, PointerUpEvent);
        //AddTriggerEvent(trigger, EventTriggerType.PointerExit, PointerExitEvent);
    }

    private void AddTriggerEvent(EventTrigger trigger, EventTriggerType eventType, UnityAction<BaseEventData> action)
    {
        EventTrigger.Entry e1 = new EventTrigger.Entry();
        e1.eventID = eventType;
        e1.callback.AddListener(action);
        trigger.triggers.Add(e1);
    }

    private void PointerUpEvent(BaseEventData arg0)
    {
        if (PuzzleManager.Instance._state != PuzzleManagerState.BoardIsChanging)
        {
            PuzzleManager.Instance.SetState(PuzzleManagerState.Waiting);
            PuzzleManager.Instance.CancleAnswer();
        }
    }

    private void PointerDownEvent(BaseEventData arg0)
    {
        if (PuzzleManager.Instance._state == PuzzleManagerState.Waiting)
        {
            SetPuzzle(PuzzleState.Pressed);
            PuzzleManager.Instance.SetState(PuzzleManagerState.Answering);
            PuzzleManager.Instance.AddToAnswer(this); 
        }
    }

    private void PointerEnterEvent(BaseEventData arg0)
    {
        if (_state == PuzzleState.Pressed)
        {
            PuzzleManager.Instance.CancleAnswer();
            return;
        }

        if(PuzzleManager.Instance._state == PuzzleManagerState.Answering)
        {
            SetPuzzle(PuzzleState.Pressed);
            PuzzleManager.Instance.AddToAnswer(this);
        }
    }

    //private void PointerExitEvent(BaseEventData arg0)
    //{
    //    Vector3 mousePos = Input.mousePosition;
    //    if (mousePos.x < 76 || mousePos.x > 639 || mousePos.y < 166 || mousePos.y > 725)
    //        PuzzleManager.Instance.CancleAnswer();
    //}
    #endregion

    public void SetPuzzle(PuzzleState state)
    {
        _state = state;
        image.material = materials[(int)state];
    }
}
