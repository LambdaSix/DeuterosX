using System;
using System.Drawing;

namespace Deuteros.Menu
{
	/// <summary>
	/// Base class for defining menu data saving structures.
	/// </summary>
	public abstract class MenuData
	{
		/// <summary>
		/// Creates a new MenuData instance. Abstract.
		/// </summary>
		public MenuData()
		{

		}

		/// <summary>
		/// Menu buttons
		/// </summary>
		public MenuButton[] buttons = {};

		/// <summary>
		/// Notifies the data structure the data have to be updated.
		/// </summary>
		public virtual void Update()
		{

		}

		/// <summary>
		/// Returns the MenuData structure.
		/// </summary>
		/// <param name="menu">Menu type</param>
		/// <returns>Menu data</returns>
		public static MenuData CreateMenuData(MenuType menu)
		{
			switch ( menu )
			{
				case MenuType.MainMenu:
				{
					return new MenuDataMainMenu();

					//break;
				}
				default:
				{
					return null;

					//break;
				}
			}
		}
	}

	/// <summary>
	/// Ingame menu button
	/// </summary>
	public sealed class MenuButton
	{
		private Rectangle buttonRect = Rectangle.Empty;
		/// <summary>
		/// Button rectangle.
		/// </summary>
		public Rectangle ButtonRect { get { return buttonRect; } }

		private string text = "";
		/// <summary>
		/// Button caption.
		/// </summary>
		public string Text { get { return text; } }

		private ButtonType button = ButtonType.MainMenuButton;
		/// <summary>
		/// Button type.
		/// </summary>
		public ButtonType Button { get { return button; } }

		private event ButtonClickEvent onClick;

		/// <summary>
		/// Creates new menu button
		/// </summary>
		/// <param name="buttonRect">Button rectangle.</param>
		/// <param name="text">Button caption.</param>
		/// <param name="button">Button type</param>
		/// <param name="onClick">Button onclick event</param>
		public MenuButton(Rectangle buttonRect, string text, ButtonType button, ButtonClickEvent onClick)
		{
			this.buttonRect = buttonRect;
			this.text = text;
			this.button = button;
			this.onClick += onClick;
		}

		/// <summary>
		/// OnClick event
		/// </summary>
		/// <param name="mouseButton">Mouse button</param>
		public void OnClick(System.Windows.Forms.MouseButtons mouseButton)
		{
			if ( onClick.Target != null )
			{
				onClick.Method.Invoke( onClick.Target, new object[1] { mouseButton } );
			}
		}
	}

	/// <summary>
	/// Mouse button click event
	/// </summary>
	public delegate void ButtonClickEvent(System.Windows.Forms.MouseButtons mouseButton);

	/// <summary>
	/// Specifies the button type.
	/// </summary>
	public enum ButtonType
	{
		/// <summary>
		/// Main menu button
		/// </summary>
		MainMenuButton = 0,
		/// <summary>
		/// Buy/sell button
		/// </summary>
		BuySellButton = 1
	}
}
