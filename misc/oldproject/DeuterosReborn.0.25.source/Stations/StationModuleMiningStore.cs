/*
 * Created by SharpDevelop.
 * User: Luaan
 * Date: 8.6.2007
 * Time: 23:47
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

using Deuteros;
using Deuteros.Gui;
using Deuteros.Items;

namespace Deuteros.Stations
{
	/// <summary>
	/// Description of StationModuleMiningStore.
	/// </summary>
	public class StationModuleMiningStore: StationModuleBase
	{
		public Dictionary<string, int> StoreMaterials = null;
		public Dictionary<string, int> StoreItems = null;
		
		public StationModuleMiningStore()
		{
			this.StoreItems = new Dictionary<string, int>();
			this.StoreMaterials = new Dictionary<string, int>();
			
			foreach ( KeyValuePair<string, StoreItem> it in GameEngine.Instance.ItemTemplates )
			{
				if ( it.Value != null )
				{
					if ( it.Value.Type == ItemType.Mineral )
					{
						this.StoreMaterials.Add(it.Key, 0);
					}
					else
					{
						this.StoreItems.Add(it.Key, 0);
					}
				}
			}
			
			buttonInfo = new GuiButtonInfo("leftGuiBottom", new Rectangle(60, 200, 60, 40), "Mining Store", "leftGui.MiningStores", true);
			buttonInfo.Params.Add("NextScreen", new GuiButtonParamChangeScreen(GameScreen.StationModule));
			buttonInfo.Params.Add("NextStationModule", new GuiButtonParamChangeStationModule(this.GetType().Name));
			buttonIndex = 0;
		}
		
		public bool mineralList = true;
		
		public int AddToStorage(bool isItem, string id, int amount)
		{
			if ( isItem )
			{
				int toadd = amount;
				if ( StoreItems[id] + amount > 150 ) toadd = 150 - StoreItems[id];
				
				StoreItems[id] += toadd;

				return toadd;
			}
			else
			{
				int toadd = amount;
				if ( StoreMaterials[id] + amount > 50000 ) toadd = 50000 - StoreMaterials[id];
				
				StoreMaterials[id] += toadd;

				return toadd;
			}
		}
		
		public bool CheckStorage(bool isItem, string id, int amount)
		{
			if ( isItem )
			{
				if ( StoreItems[id] - amount < 0 )
				{
					return false;
				}
				else
				{
					return true;
				}
			}
			else
			{
				if ( StoreMaterials[id] - amount < 0 )
				{
					return false;
				}
				else
				{
					return true;
				}
			}
		}
		
		public bool RemoveFromStorage(bool isItem, string id, int amount)
		{
			if ( isItem )
			{
				if ( StoreItems[id] - amount < 0 )
				{
					return false;
				}
				else
				{
					StoreItems[id] -= amount;
					
					return true;
				}
			}
			else
			{
				if ( StoreMaterials[id] - amount < 0 )
				{
					return false;
				}
				else
				{
					StoreMaterials[id] -= amount;
					
					return true;
				}
			}
		}
		
		public override void FromXml(XmlNode node)
		{
			base.FromXml(node);
			
			XmlDocument doc = node.OwnerDocument;
			
			XmlElement el;

			StoreMaterials.Clear();
			StoreItems.Clear();
			
			foreach ( KeyValuePair<string, StoreItem> it in GameEngine.Instance.ItemTemplates )
			{
				if ( it.Value != null )
				{
					if ( it.Value.Type == ItemType.Mineral )
					{
						this.StoreMaterials.Add(it.Key, 0);
					}
					else
					{
						this.StoreItems.Add(it.Key, 0);
					}
				}
			}
			
			el = node["StoreMaterials"];
			foreach ( XmlNode xMat in el.ChildNodes )
			{
				StoreMaterials[xMat.Name] = int.Parse(xMat.InnerText);
			}
			
			el = node["StoreItems"];
			foreach ( XmlNode xIt in el.ChildNodes )
			{
				StoreItems[xIt.Name] = int.Parse(xIt.InnerText);
			}
		}
		
		public override void ToXml(XmlNode node)
		{
			base.ToXml(node);
			
			XmlDocument doc = node.OwnerDocument;
			
			XmlElement el = doc.CreateElement("StoreMaterials");
			node.AppendChild(el);
			
			XmlElement el2;
			
			foreach ( KeyValuePair<string, int> it in this.StoreMaterials )
			{
				el2 = doc.CreateElement(it.Key);
				el2.InnerText = it.Value.ToString();
				el.AppendChild(el2);
			}
			
			el = doc.CreateElement("StoreItems");
			node.AppendChild(el);
			
			foreach ( KeyValuePair<string, int> it in this.StoreItems )
			{
				el2 = doc.CreateElement(it.Key.Replace(" ", "_"));
				el2.InnerText = it.Value.ToString();
				el.AppendChild(el2);
			}
		}

		public void SwitchStore()
		{
			if ( GameEngine.Instance.GuiMenuModule.ContainsKey("miningStoreItemMenu") )
			{
				GameEngine.Instance.GuiMenuModule["miningStoreItemMenu"].Buttons.Clear();
			}
			
			mineralList = !mineralList;
			
			int prodRank = 1;
			bool orbital = false;
			
			// TODO: Get prodRank and orbital
			
			if ( GameEngine.Instance.GuiMenuModule.ContainsKey("miningStoreItemMenu") )
			{
				if ( mineralList )
				{
					GuiButtonInfo btn;
					int i = -1;
					foreach ( KeyValuePair<string, StoreItem> sit in GameEngine.Instance.ItemTemplates )
					{
						if ( sit.Value.Type != ItemType.Mineral )
						{
							i++;
							
							if ( sit.Value.Researched )
							{
								btn = new GuiButtonInfo("miningStoreItemMenu", new Rectangle( (i / 16) * 42 + 7, (i % 16 * 20) + 7, 42, 20), sit.Value.Title, "gameScreen.Storage.ItemBlink", true, 1, 100);
								if ( sit.Value.OrbitalOnly && !orbital )
								{
									btn.PictureColor = Color.Yellow;
								}
								else
								{
									if ( sit.Value.BuildRank > prodRank )
									{
										btn.PictureColor = Color.Red;
									}
									else
									{
										btn.PictureColor = Color.Green;
									}
								}

								GuiButtonParamExecuteModuleMethod par = new GuiButtonParamExecuteModuleMethod("ShowItemPrice", GetType().ToString());
								par.methodParams.Add(new MethodParamInfo(typeof(string).ToString(), sit.Key));
								btn.Params.Add("Show" + sit.Key + "Price", par);
								
								par = new GuiButtonParamExecuteModuleMethod("ResetAnimation", GetType().ToString());
								par.methodParams.Add(new MethodParamInfo(typeof(string).ToString(), GameEngine.Instance.GuiMenuModule["miningStoreItemMenu"].Buttons.Count.ToString()));
								btn.Params.Add("ResetAnimation", par);
								
								GameEngine.Instance.GuiMenuModule["miningStoreItemMenu"].Buttons.Add(GetType().Name + ".ShowItemPrice." + sit.Key, btn);
							}
						}
					}
				}
			}
		}
		
		public void ResetAnimation(string btnIdx)
		{
			int idx = int.Parse(btnIdx);
			
			int i = 0;
			
			foreach ( KeyValuePair<string, GuiButtonInfo> btn in GameEngine.Instance.GuiMenuModule["miningStoreItemMenu"].Buttons )
			{
				btn.Value.AnimationFrames = 1;
				if ( idx == i ) btn.Value.AnimationFrames = 10;
				
				i++;
			}
		}
		
		public string curItemId = "";
		
		public void ShowItemPrice(string id)
		{
			this.curItemId = id;
		}
		
		public bool isDescriptionShown = false;
		
		public override void OnInitMenus()
		{
			GuiMenu mnu = new GuiMenu("miningStoreMenu");
			mnu.Picture = "gameScreen.Storage";
			mnu.Location = new Point(120, 40);
			mnu.TextureArea = new Rectangle(0, 0, 680, 420);
			GameEngine.Instance.GuiMenuModule.Add(mnu.Id, mnu);

			GuiButtonInfo btn = new GuiButtonInfo("miningStoreMenu", new Rectangle(80, 378, 61, 40), "Switch store", "", true);
			
			btn.Params.Add("SwitchStore", new GuiButtonParamExecuteModuleMethod("SwitchStore", GetType().ToString()));
			mnu.Buttons.Add(GetType().Name + ".SwitchStore", btn);
			
			mnu = new GuiMenu("miningStoreItemMenu");
			mnu.Picture = "";
			mnu.Location = new Point(560 + 120, 0 + 40);
			mnu.TextureArea = new Rectangle(0, 0, 101, 334);
			GameEngine.Instance.GuiMenuModule.Add(mnu.Id, mnu);
			
			SwitchStore();
			SwitchStore();
		}
		
		public override void OnRenderInterface(Microsoft.DirectX.Direct3D.Sprite overlay)
		{
			if ( mineralList )
			{
				isDescriptionShown = false;
			}
			else
			{
				if ( GameEngine.Instance.IsMouseDown )
				{
					isDescriptionShown = false;
					curItemId = "";					
				}
			}
			
			int i = 0;
			
			Dictionary<string, int> pomst = null;
			if ( this.mineralList ) pomst = this.StoreMaterials;
			else pomst = this.StoreItems;
		
			Dictionary<string, int> required = new Dictionary<string, int>();
			if ( curItemId != "" && mineralList )
			{
				for ( i = 0; i < GameEngine.Instance.ItemTemplates[curItemId].Resources.Count; i ++ )
				{
					required.Add( GameEngine.Instance.ItemTemplates[curItemId].Resources[i].itemId, GameEngine.Instance.ItemTemplates[curItemId].Resources[i].amount );
				}
			}
			
			int maxCanDo = 200;
			i = 0;
			foreach ( KeyValuePair<string, int> si in pomst )
			{
				if ( GameEngine.Instance.ItemTemplates[si.Key].Researched )
				{
					Color col = Color.FromArgb(255, 110, 110, 45);
					if ( required.ContainsKey(si.Key) )
					{
						if ( required[si.Key] <= si.Value )
						{
							col = Color.Green;
						}
						else
						{
							col = Color.Red;
						}
						
						int pom = si.Value / required[si.Key];
						if ( maxCanDo > pom ) maxCanDo = pom;
					
					}

					GameEngine.Instance.GuiFonts["fntGui"].DXFont.DrawText(overlay, si.Value.ToString(), new Rectangle(130, 40 + i * 20, 70, 20), DrawTextFormat.Right | DrawTextFormat.VerticalCenter, col);
					GameEngine.Instance.GuiFonts["fntGui"].DXFont.DrawText(overlay, GameEngine.Instance.ItemTemplates[si.Key].Title, 220, 40 + i * 20, Color.White);
					Rectangle rct;
					rct = GameEngine.Instance.GuiFonts["fntGui"].DXFont.MeasureString(overlay, GameEngine.Instance.ItemTemplates[si.Key].Title, DrawTextFormat.None, Color.White);
					rct.Offset(220, 40 + i * 20);
					
					if ( rct.Contains(GameEngine.Instance.MouseCoords) )
					{
						GameEngine.Instance.CurrentTooltip = GameEngine.Instance.ItemTemplates[si.Key].Title;
						
						if ( !mineralList && GameEngine.Instance.IsMouseDown )
						{
							isDescriptionShown = true;
							curItemId = si.Key;
						}
					}
				
					i++;
				}
			}
			
			GuiMenu mnu = GameEngine.Instance.GuiMenuModule["miningStoreMenu"];
			
			Point center = new Point(mnu.Location.X + mnu.TextureArea.Width / 2, mnu.Location.Y + mnu.TextureArea.Height / 2);
			
			if ( mineralList && curItemId != "" )
			{
				GameEngine.Instance.GuiFonts["fntGui"].DXFont.DrawText(overlay, "Enough Supplies For\r\n" + maxCanDo.ToString() + "x " + GameEngine.Instance.ItemTemplates[curItemId].Title, new Rectangle(mnu.Location.X, mnu.Location.Y, mnu.TextureArea.Width - 140, mnu.TextureArea.Height), DrawTextFormat.Right | DrawTextFormat.Top, Color.Yellow);
			}
			
			if ( isDescriptionShown )
			{
				StoreItem si = GameEngine.Instance.ItemTemplates[curItemId];
				
				TextureManagerItem it;
				if ( GameEngine.Instance.GuiTextures.ContainsKey("gameScreen.Storage.ItemDescription") )
				{
					it = GameEngine.Instance.mainTextures.GetTexture(GameEngine.Instance.GuiTextures["gameScreen.Storage.ItemDescription"].textureIdx);
					if ( it != null && it.Texture != null )
					{
						overlay.Transform = Matrix.Identity;
						
						overlay.Draw(it.Texture, new Rectangle(0, 0, 600, 300), new Vector3(300, 150, 0), new Vector3(center.X, center.Y, 0), Color.White.ToArgb());
					}
				}
				
				GameEngine.Instance.GuiFonts["fntGui"].DXFont.DrawText(overlay, si.Title, new Rectangle(center.X - 270, center.Y - 130, 540, 20), DrawTextFormat.Left | DrawTextFormat.VerticalCenter, Color.White);
				GameEngine.Instance.GuiFonts["fntGui"].DXFont.DrawText(overlay, si.Description, new Rectangle(center.X - 270, center.Y - 110, 540, 260), DrawTextFormat.Left | DrawTextFormat.Top, Color.Yellow);
				
				if ( GameEngine.Instance.GuiTextures.ContainsKey(si.Picture) )
				{
					it = GameEngine.Instance.mainTextures.GetTexture(GameEngine.Instance.GuiTextures[si.Picture].textureIdx);
					if ( it != null && it.Texture != null )
					{
						overlay.Transform = Matrix.Identity;
						
						overlay.Draw(it.Texture, new Rectangle(0, 0, si.TextureArea.Width, si.TextureArea.Height), new Vector3(si.TextureArea.Width, si.TextureArea.Height / 2, 0), new Vector3(center.X + 270, center.Y, 0), Color.White.ToArgb());
					}
				}
			}
		}
	}
}
