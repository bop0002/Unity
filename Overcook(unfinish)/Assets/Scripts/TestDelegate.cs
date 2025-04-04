using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDelegate : MonoBehaviour
{

    public Action testAction;
    private float timer;

    public void SetTimer(float timer,Action testAction)
    {
        this.timer = timer;
        this.testAction = testAction;
    }

    private void CountDown()
    {
        timer -= Time.deltaTime;
    }

    private bool Check()
    {
        return timer <= 0f;
    }

    private void Update()
    {
        CountDown();
        if(Check())
        {
            testAction();
        }
    }


}
