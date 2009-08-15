using System;
using System.Drawing;

using MouseButtons = System.Windows.Forms.MouseButtons;

namespace Deuteros.Menu
{
	/// <summary>
	/// Summary description for MenuDataMainMenu.
	/// </summary>
	public class MenuDataMainMenu: MenuData
	{
		/// <summary>
		/// Creates new MenuDataMainMenu instance.
		/// </summary>
		public MenuDataMainMenu()
		{
			// menu buttons
			this.buttons = new MenuButton[2] 
				{ 
					new MenuButton(new Rectangle(128, 220 + 80, 256, 32), "Quit game", ButtonType.MainMenuButton, new ButtonClickEvent(doQuitGame)),
					new MenuButton(new Rectangle(128, 80, 256, 32), "Settings", ButtonType.MainMenuButton, new ButtonClickEvent(doOptions))
				};
		}

		private void doQuitGame(MouseButtons mouseButton)
		{
			GameEngine.Instance.hasQuit = true;
		}
		
		private void doOptions(MouseButtons mouseButton)
		{
			GameEngine.Instance.MessageBox("Feature not implemented. Yet.");
		}
	}
}
