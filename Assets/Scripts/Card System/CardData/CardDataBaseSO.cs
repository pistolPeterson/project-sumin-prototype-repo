
using UnityEngine;

public abstract class CardDataBaseSO : ScriptableObject //TODO: will probably be renamed to something about cards for ingame bless/curses
{
  public string cardName = "New Card Name";
  [TextArea] public string cardDescription = "New Card Description, hehehe";
  public CardType cardType = CardType.Neutral; //might not be needed, we can do a naming convention so card names has bless or curse
  public abstract void CardEffect(GameManager gameManager); // will be called sometime in awake/start? 
}

public enum CardType // might be renamced as "card tyoes" can mean if its for in game or outside the game
{
  Neutral,
  Bless,
  Curse
}

//Todo: place in a specific script 
[CreateAssetMenu(menuName = "New Card/MaxHealthBless")]
public class MaxHealthBless : CardDataBaseSO
{
  public override void CardEffect(GameManager gameManager)
  {
    gameManager.playerObject.GetComponent<PlayerHeatlh>().SetMaxHealth(2);
  }
}
