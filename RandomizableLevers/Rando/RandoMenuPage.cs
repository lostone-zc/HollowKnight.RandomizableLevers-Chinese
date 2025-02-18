﻿using MenuChanger;
using MenuChanger.MenuElements;
using MenuChanger.MenuPanels;
using MenuChanger.Extensions;
using RandomizerMod.Menu;
using static RandomizerMod.Localization;

namespace RandomizableLevers.Rando
{
    public class RandoMenuPage
    {
        internal MenuPage LeverRandoPage;
        internal MenuElementFactory<LeverRandomizationSettings> leverMEF;
        internal VerticalItemPanel leverVIP;
        
        internal SmallButton JumpToLeverRandoButton;

        private static RandoMenuPage _instance = null;
        internal static RandoMenuPage Instance => _instance ?? (_instance = new RandoMenuPage());

        public static void OnExitMenu()
        {
            _instance = null;
        }

        public static void Hook()
        {
            RandomizerMenuAPI.AddMenuPage(Instance.ConstructMenu, Instance.HandleButton);
            MenuChangerMod.OnExitMainMenu += OnExitMenu;
        }

        private bool HandleButton(MenuPage landingPage, out SmallButton button)
        {
            JumpToLeverRandoButton = new(landingPage, Localize("Levers"));
            JumpToLeverRandoButton.AddHideAndShowEvent(landingPage, LeverRandoPage);
            button = JumpToLeverRandoButton;
            return true;
        }

        private void ConstructMenu(MenuPage landingPage)
        {
            LeverRandoPage = new MenuPage(Localize("Levers"), landingPage);
            leverMEF = new(LeverRandoPage, RandoInterop.Settings);
            leverVIP = new(LeverRandoPage, new(0, 300), 75f, false, leverMEF.Elements);
            Localize(leverMEF);
        }
    }
}
