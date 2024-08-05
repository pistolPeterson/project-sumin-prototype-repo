
using UnityEngine;

public abstract class CardDataBaseSO : ScriptableObject //TODO: will probably be renamed to something about cards for ingame bless/curses
{
  public string cardName = "New Card Name";
  public Sprite cardSprite;
  [TextArea] public string cardDescription = "New Card Description, hehehe";

  public virtual void CardEffect(GameManager gameManager)// will be called sometime in awake/start? 
  {
    
  }
}

//These scripts are the ones you inherit from 2
public class BlessCardBase : CardDataBaseSO { }
public class CurseCardBase : CardDataBaseSO { }





