using System.Collections;
using System.Collections.Generic;
using PeteUnityUtils;
using UnityEngine;

public class AllCards : PersistentSingleton<AllCards>
{
  public List<CardDataBaseSO> CardPool;


  public void ShuffleCardPool()
  {
    CardPool.Shuffle();
  }
}
