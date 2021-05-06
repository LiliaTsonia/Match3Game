using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tests
{
    public class Create
    {
        public static BoardManager Board()
        {
            return new GameObject().AddComponent<BoardManager>();
        }
    }
}

