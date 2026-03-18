using UnityEngine;
using XLua;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Threading.Tasks;

namespace XLuaTest
{
    public class Coroutine_Runner : MonoBehaviour
    {
        void Start()
        {
            StartCoroutine(test());
            // ExampleAsync();
        }
        IEnumerator test(){
            yield return new WaitForSeconds(1);
        }
        // async Task<int> ExampleAsync() {
        //     Console.WriteLine("开始");
        //     await Task.Delay(1000);
        //     Console.WriteLine("结束");
        //     return 42;
        // }
    }


    public static class CoroutineConfig
    {
        [LuaCallCSharp]
        public static List<Type> LuaCallCSharp
        {
            get
            {
                return new List<Type>()
            {
                typeof(WaitForSeconds),
                typeof(WWW)
            };
            }
        }
    }
}
