//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using static Test;

//public class TestingEvent : MonoBehaviour
//{


//    private void Start()
//    {
//        Test test = GetComponent<Test>();
//        test.OnTestingFloat += Test_OnTestingFloat;
//    }

//    private void Test_OnTestingFloat(float f)
//    {
//        Debug.Log(f);
//    }

//    private void Test_OnSpacePressed(object sender, Test.aOnSpacePressedEventArgs e)
//    {
//        Debug.Log("cac " + e.spaceCount);
//    }


//    private void Test_OnSpacePressed2(object sender, Test.OnSpacePressedEventArgs e)
//    {
//        Debug.Log("cut " + e.spaceCount);
//    }



//}
