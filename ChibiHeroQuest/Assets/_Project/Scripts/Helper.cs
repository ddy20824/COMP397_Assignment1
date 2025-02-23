using System;
using System.Collections;
using UnityEngine;

namespace Platformer397
{
    public class Helper
    {
        public static IEnumerator Delay(Action methodName, float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            methodName();
        }
    }
}
