﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerProject
{
  public class playerController
  {
    private playerView _view;
    private playerModel _model;
    private pokerController _controllerPoker;

    public playerController(pokerController controllerPoker, int numberOfCards)
    {
      _controllerPoker = controllerPoker;
      _model = new playerModel(numberOfCards);
      makeCardViews(_model.NumberOfCards);
      _view = new playerView(this);
    }

    public playerModel getModelPlayer()
    {
      return _model;
    }

    public playerView getViewPlayer()
    {
      return _view;
    }

    public pokerController getControllerPoker()
    {
      return _controllerPoker;
    }

    public List<cardView> getCardsView()
    {
      List<cardView> cardsView = new List<cardView>(); // Maak een lijst die de views zal opvangen
      foreach (cardController card in _model.Cards) // Loop over het huidig aantal kaarten uit het model
      {
        cardView cardView = card.getViewCard(); // Haal de view op voor iedere kaart
        cardsView.Add(cardView); // Voeg de kaart toe aan de lijst die de views opvangt
      }
      return cardsView; // Return de lijst met views van de kaarten
    }

    public void makeCardViews(int numberOfCards)
    {
      for (int cardNumber = 0; cardNumber < numberOfCards; cardNumber++)
      {
        cardController card = new cardController(this); //Enkele kaart aanmaken
        cardView cardView = card.getViewCard(); //Nieuwe view kaart
        _model.addCard(card); //kaart toevoegen aan lijst

        card.getModelCard().CardKind = _controllerPoker.getModelPoker().CardsStackKind[0]; //bovenste kaart soort ophalen van random lijst
        card.getModelCard().CardValue = _controllerPoker.getModelPoker().CardsStackValue[0]; //bovenste kaart waarde ophalen van random lijst

        _controllerPoker.getModelPoker().removeCardValue(0); //verwijder kaart van stack
        _controllerPoker.getModelPoker().removeCardKind(0);  //verwijder kaart van stack

        card.getViewCard().updateView(); //view update van de kaart
      }
    }
  }
}
