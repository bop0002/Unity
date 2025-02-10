using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScoringZone : MonoBehaviour
{

    public EventTrigger.TriggerEvent scoreEvent;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if(ball != null )
        {
            BaseEventData eventdata = new BaseEventData(EventSystem.current);
            this.scoreEvent.Invoke(eventdata);
        }
    }
}
