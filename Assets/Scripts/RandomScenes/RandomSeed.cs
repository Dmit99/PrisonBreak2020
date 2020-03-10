using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomSeedCreation
{
    public class RandomSeed : MonoBehaviour
    {
        public void CreateRandomSeed(int seednumber)
        {
            for (int i = 0; i < 10; i++)
            {
                Random.InitState(seed: seednumber);
                float r = Random.Range(1f, 100f);
                Debug.Log(r);
            }
        }
    }
}
