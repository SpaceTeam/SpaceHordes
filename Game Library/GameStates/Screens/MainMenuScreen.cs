﻿using Microsoft.Xna.Framework;

using Game_Library.Input;

namespace Game_Library.GameStates.Screens
{
    /// <summary>
    /// The main menu screen is the first thing displayed when 
    /// </summary>
    public class MainMenuScreen : MenuScreen
    {
        #region Initialization

        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public MainMenuScreen(string text)
            : base(text)
        {
            //Create our menu entries.
#if WINDOWS
            MenuEntry playGameMenuEntry = new MenuEntry("Play Game");
#endif

#if XBOX
            MenuEntry playGameMenuEntry = new MenuEntry("Play Solo");
            MenuEntry playMultiplayerMenuEntry = new MenuEntry("Multiplayer");
#endif
            MenuEntry optionsMenuEntry = new MenuEntry("Options");
            MenuEntry exitMenuEntry = new MenuEntry("Exit");

            //Hook up menu event handlers.
            playGameMenuEntry.Selected += PlayGameMenuEntrySelected;
#if XBOX
            playMultiplayerMenuEntry.Selected += PlayMultiplayerMenuEntrySelected;
#endif
            optionsMenuEntry.Selected += OptionsMenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;

            //Add entries to the menu.
            MenuEntries.Add(playGameMenuEntry);
#if XBOX
            MenuEntries.Add(playMultiplayerMenuEntry);
#endif
            MenuEntries.Add(optionsMenuEntry);
            MenuEntries.Add(exitMenuEntry);
        }

        #endregion

        #region Events

        /// <summary>
        /// Event handler for when the Play Game menu entry is selected.
        /// </summary>
        void PlayGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, e.PlayerIndex, new GameplayScreen("Fonts/gamefont", false));
        }

        void PlayMultiplayerMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, e.PlayerIndex, new GameplayScreen("Fonts/gamefont", true));
        }

        /// <summary>
        /// Event handler for when the Options menu entry is selected.
        /// </summary>
        void OptionsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new OptionsMenuScreen(), e.PlayerIndex);
        }

        /// <summary>
        /// When the user cancels the main menu, ask if they want to exit the sample.
        /// </summary>
        protected override void OnCancel(PlayerIndex playerIndex)
        {
            ScreenManager.Game.Exit();
        }

        /// <summary>
        /// Event handler for when the user selects ok on the
        /// "are you sure you want to exit" message box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }

        #endregion
    }
}
