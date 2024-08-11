using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllCards : PersistentSingleton<AllCards>
{
  public List<CardDataBaseSO> CardPool;
}
