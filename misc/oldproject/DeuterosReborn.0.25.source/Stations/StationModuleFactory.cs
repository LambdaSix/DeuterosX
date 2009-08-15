/*
 * Created by SharpDevelop.
 * User: Luaan
 * Date: 4.6.2007
 * Time: 11:16
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

using Deuteros.Gui;
using Deuteros.Items;
using Deuteros.Teams;

namespace Deuteros.Stations
{
	/// <summary>
	/// Description of StationModuleFactory.
	/// </summary>
	public class StationModuleFactory: StationModuleBase
	{
		public StationModuleFactory()
		{
			BuildQueue = new List<ItemBuildInfo>();
			
			buttonInfo = new GuiButtonInfo("leftGuiBottom", new Rectangle(0, 0, 60, 40), "Production", "leftGui.Production", true);
			buttonInfo.Params.Add("NextScreen", new GuiButtonParamChangeScreen(GameScreen.StationModule));
			buttonInfo.Params.Add("NextStationModule", new GuiButtonParamChangeStationModule(this.GetType().Name));
			buttonIndex = 0;
		}
		
		public int productionPrepareTime = 1;
		
		public bool IsOrbital = false;
		public bool HasAOC = false;
		public WorkTeamProduction ProductionTeam = null;
		
		public int lastProductionMark = 0;
		public int BuildProgress = -1;
		public int BuildQueueIndex = -1;
		public List<ItemBuildInfo> BuildQueue = null;
		
		private bool resourcesReady = false;
		
		public override void OnRenderInterface(Microsoft.DirectX.Direct3D.Sprite Overlay)
		{
			GuiMenu mnu = GameEngine.Instance.GuiMenuModule["factoryMenu"];
			
			TextureManagerItem it;
			if ( BuildQueueIndex != -1 && BuildProgress > -1 )
			{
				StoreItem sit = GameEngine.Instance.ItemTemplates[BuildQueue[BuildQueueIndex].ItemId];
				GameEngine.Instance.GuiFonts["fntGui"].DXFont.DrawText(Overlay, sit.Title, new Rectangle(mnu.Location.X, mnu.Location.Y + 40, 200, 20), DrawTextFormat.Left | DrawTextFormat.VerticalCenter, Color.Green);
				
				if ( GameEngine.Instance.GuiTextures.ContainsKey("placeholder") )
				{
					it = GameEngine.Instance.mainTextures.GetTexture(GameEngine.Instance.GuiTextures["placeholder"].textureIdx);
					if ( it != null && it.Texture != null )
					{
						Overlay.Transform = Matrix.Identity;
						
						Overlay.Draw(it.Texture, new Rectangle(0, 0, 200, 125), new Vector3(0, 0, 0), new Vector3(mnu.Location.X, mnu.Location.Y + 69, 0), Color.FromArgb(255, 13, 13, 13).ToArgb());
					}
				}
				
				if ( GameEngine.Instance.GuiTextures.ContainsKey(sit.Picture) )
				{
					it = GameEngine.Instance.mainTextures.GetTexture(GameEngine.Instance.GuiTextures[sit.Picture].textureIdx);
					if ( it != null && it.Texture != null )
					{
						Overlay.Transform = Matrix.Identity;
						
						Overlay.Draw(it.Texture, new Rectangle(sit.TextureArea.Width * ( (BuildProgress * 3) / sit.BuildTime + 1), 0, sit.TextureArea.Width, sit.TextureArea.Height), new Vector3(sit.TextureArea.Width, 0, 0), new Vector3(mnu.Location.X + 200, mnu.Location.Y + 69, 0), Color.White.ToArgb());
					}
				}
			}
			
			if ( HasAOC )
			{
				if ( GameEngine.Instance.GuiTextures.ContainsKey("gameScreen.Production.AOC") )
				{
					it = GameEngine.Instance.mainTextures.GetTexture(GameEngine.Instance.GuiTextures["gameScreen.Production.AOC"].textureIdx);
					if ( it != null && it.Texture != null )
					{
						Overlay.Transform = Matrix.Identity;
						
						Overlay.Draw(it.Texture, new Rectangle(0, 0, 305, 58), new Vector3(0, 0, 0), new Vector3(mnu.Location.X + 366, mnu.Location.Y + 350, 0), Color.White.ToArgb());
					}
				}
				// TODO: Render the real AOC blink
				if ( GameEngine.Instance.GuiTextures.ContainsKey("gameScreen.Storage.ItemBlink") )
				{
					it = GameEngine.Instance.mainTextures.GetTexture(GameEngine.Instance.GuiTextures["gameScreen.Storage.ItemBlink"].textureIdx);
					if ( it != null && it.Texture != null )
					{
						Overlay.Transform = Matrix.Identity;
						
						Overlay.Draw(it.Texture, new Rectangle(8 + 42 * ((Environment.TickCount / 70) % 10), 8, 7, 4), new Vector3(0, 0, 0), new Vector3(mnu.Location.X + 366 + 58, mnu.Location.Y + 350 + 22, 0), Color.Red.ToArgb());
						Overlay.Draw(it.Texture, new Rectangle(8 + 42 * ((Environment.TickCount / 70) % 10), 8, 7, 4), new Vector3(0, 0, 0), new Vector3(mnu.Location.X + 366 + 58, mnu.Location.Y + 350 + 21, 0), Color.Red.ToArgb());
					}
				}
			}
			else
			{
				GameEngine.Instance.GuiFonts["fntGui"].DXFont.DrawText(Overlay, "LEADER", new Rectangle(mnu.Location.X + 364, mnu.Location.Y + 349, 90, 20), DrawTextFormat.Left | DrawTextFormat.VerticalCenter, Color.Red);
				GameEngine.Instance.GuiFonts["fntGui"].DXFont.DrawText(Overlay, "RANK", new Rectangle(mnu.Location.X + 364, mnu.Location.Y + 369, 90, 20), DrawTextFormat.Left | DrawTextFormat.VerticalCenter, Color.Red);
				GameEngine.Instance.GuiFonts["fntGui"].DXFont.DrawText(Overlay, "STAFF", new Rectangle(mnu.Location.X + 364, mnu.Location.Y + 389, 90, 20), DrawTextFormat.Left | DrawTextFormat.VerticalCenter, Color.Red);
				
				if ( ProductionTeam == null )
				{
					GameEngine.Instance.GuiFonts["fntGui"].DXFont.DrawText(Overlay, "None", new Rectangle(mnu.Location.X + 454, mnu.Location.Y + 349, 218, 20), DrawTextFormat.Left | DrawTextFormat.VerticalCenter, Color.White);
				}
				else
				{
					GameEngine.Instance.GuiFonts["fntGui"].DXFont.DrawText(Overlay, ProductionTeam.TeamLeader, new Rectangle(mnu.Location.X + 454, mnu.Location.Y + 349, 218, 20), DrawTextFormat.Left | DrawTextFormat.VerticalCenter, Color.White);
					GameEngine.Instance.GuiFonts["fntGui"].DXFont.DrawText(Overlay, ProductionTeam.GetTeamRank(), new Rectangle(mnu.Location.X + 454, mnu.Location.Y + 369, 218, 20), DrawTextFormat.Left | DrawTextFormat.VerticalCenter, Color.White);
					GameEngine.Instance.GuiFonts["fntGui"].DXFont.DrawText(Overlay, ProductionTeam.TeamSize.ToString(), new Rectangle(mnu.Location.X + 454, mnu.Location.Y + 389, 218, 20), DrawTextFormat.Left | DrawTextFormat.VerticalCenter, Color.White);
				}
			}
		}
		
		public override void FromXml(XmlNode node)
		{
			base.FromXml(node);
			
			XmlNode pt;
			
			this.HasAOC = bool.Parse(node["HasAOC"].InnerText);
			this.IsOrbital = bool.Parse(node["IsOrbital"].InnerText);
			
			pt = node["ProductionTeam"];
			if ( pt.ChildNodes.Count == 0 )
			{
				this.ProductionTeam = null;
			}
			else
			{
				if ( this.ProductionTeam == null )
				{
					this.ProductionTeam = new WorkTeamProduction();
					this.ProductionTeam.FromXml(pt);
				}
			}
			
			this.BuildProgress = int.Parse(node["BuildProgress"].InnerText);
			this.BuildQueueIndex = int.Parse(node["BuildQueueIndex"].InnerText);
			
			this.BuildQueue.Clear();
			foreach ( XmlNode bq in node["BuildQueue"].ChildNodes )
			{
				ItemBuildInfo ibi = new ItemBuildInfo(bq["ItemId"].InnerText, 
				                                      bool.Parse(bq["Repeat"].InnerText));
				this.BuildQueue.Add(ibi);
			}
		}
		
		public override void ToXml(System.Xml.XmlNode node)
		{
			base.ToXml(node);
			
			XmlDocument doc = node.OwnerDocument;
			
			XmlElement el = doc.CreateElement("HasAOC");
			el.InnerText = HasAOC.ToString();
			node.AppendChild(el);
			
			el = doc.CreateElement("IsOrbital");
			el.InnerText = HasAOC.ToString();
			node.AppendChild(el);
			
			el = doc.CreateElement("ProductionTeam");
			node.AppendChild(el);
			if ( ProductionTeam != null )
			{
				ProductionTeam.ToXml(el);
			}
			
			el = doc.CreateElement("BuildProgress");
			el.InnerText = BuildProgress.ToString();
			node.AppendChild(el);
			
			el = doc.CreateElement("BuildQueueIndex");
			el.InnerText = BuildQueueIndex.ToString();
			node.AppendChild(el);
			
			el = doc.CreateElement("BuildQueue");
			
			XmlElement el2, el3;
			for ( int i = 0; i < BuildQueue.Count; i ++ )
			{
				if ( BuildQueue[i] != null )
				{
					el2 = doc.CreateElement("QueueItem");
					el.AppendChild(el2);
					
					el3 = doc.CreateElement("Repeat");
					el3.InnerText = BuildQueue[i].Repeat.ToString();
					el2.AppendChild(el3);
					
					el3 = doc.CreateElement("ItemId");
					el3.InnerText = BuildQueue[i].ItemId;
					el2.AppendChild(el3);
				}
			}
			
			node.AppendChild(el);
		}
		
		private StationModuleMiningStore store = null;
		
		private StationModuleMiningStore Store
		{
			get
			{
				if ( this.store == null || this.store.Parent != this.Parent )
				{
					// Find a new store
					foreach (StationModuleBase mod in Parent.Modules)
					{
						if ( mod is StationModuleMiningStore )
						{
							this.store = mod as StationModuleMiningStore;
							
							return this.store;
						}
					}
					
					return null;
				}
				else
				{
					return this.store;
				}
			}
		}
		
		/// <summary>
		/// Evicts the current workteam to a lounge
		/// </summary>
		public void EvictTeam()
		{
			// TODO: Find a personel lounge (note: hangar module looks there) and move the production team there if possible
			
			#if GenLog
			Console.WriteLine("EvictTeam: Function not implemented yet. There are no lounges as of yet, ya' know?");
			#endif
		}
		
		/// <summary>
		/// Handles factorys work cycle
		/// </summary>
		public override void OnNextTurn()
		{
			if ( BuildQueueIndex == -1 && BuildQueue.Count > 0 )
			{
				BuildQueueIndex = 0;
				BuildProgress = 0;
				if ( HasAOC ) 
				{
					lastProductionMark = GameEngine.Instance.CurrentCycle + GameEngine.Instance.CurrentYear * 1000;
					
					BuildProgress = -1;
				}
			}

			#if DebugStations
			if ( BuildQueueIndex != -1 )
			{
				Console.WriteLine("Factory module on station '" + parent.Title + "' \r\n\thas a work cycle (Build progress " + BuildProgress.ToString() + " of " + GameEngine.Instance.ItemTemplates[BuildQueue[BuildQueueIndex].ItemId].BuildTime.ToString() + ").");
			}
			#endif
			
			if ( BuildQueueIndex != -1 && (GameEngine.Instance.CurrentCycle + GameEngine.Instance.CurrentYear * 1000 - lastProductionMark) >= productionPrepareTime )
			{
				if ( !resourcesReady && Store != null )
				{
					StoreItem it = GameEngine.Instance.ItemTemplates[BuildQueue[BuildQueueIndex].ItemId];
					
					bool isOk = true;
					foreach ( FactoryResource res in it.Resources )
					{
						isOk &= Store.CheckStorage( 
						  GameEngine.Instance.ItemTemplates[res.itemId].Type != ItemType.Mineral, 
						  res.itemId, res.amount );
					}
					
					if ( isOk )
					{
						foreach ( FactoryResource res in it.Resources )
						{
							isOk &= Store.RemoveFromStorage( 
							  GameEngine.Instance.ItemTemplates[res.itemId].Type != ItemType.Mineral, 
							  res.itemId, res.amount );
						}
						
						resourcesReady = true;
					}
				}
				
				if ( resourcesReady )
				{
					if ( ProductionTeam != null )
					{
						BuildProgress += ProductionTeam.GetProductionValue();
					}
					else
					{
						if ( HasAOC )
						{
							BuildProgress += 200;
						}
					}
					
					if ( BuildProgress >= GameEngine.Instance.ItemTemplates[BuildQueue[BuildQueueIndex].ItemId].BuildTime )
					{
						Store.AddToStorage(true, BuildQueue[BuildQueueIndex].ItemId, 1);
						
						#if DebugStations
						if ( BuildQueueIndex != -1 )
						{
							Console.WriteLine("Item " + GameEngine.Instance.ItemTemplates[BuildQueue[BuildQueueIndex].ItemId].Title + " built.");
						}
						#endif
	
						
						if ( !BuildQueue[BuildQueueIndex].Repeat )
						{
							GuiMenu mnu = null;
							GuiButtonInfo btn = null;
							
							if ( GameEngine.Instance.GuiMenuModule.ContainsKey("factoryMenuItems") )
							{
								mnu = GameEngine.Instance.GuiMenuModule["factoryMenuItems"];
								
								foreach ( KeyValuePair<string, GuiButtonInfo> cur in mnu.Buttons )
								{
									if ( ((GuiButtonParamExecuteModuleMethod)cur.Value.Params["CycleQueueItem"]).methodParams[0].Value == BuildQueue[BuildQueueIndex].ItemId )
									{
										btn = cur.Value;
										
										break;
									}
								}

                                btn.PictureColor = Color.Red;
							}
							
							BuildQueue.RemoveAt(BuildQueueIndex);
						}
						else
						{
							BuildQueueIndex ++;
						}
						
						resourcesReady = false;
						
						BuildProgress = 0;
						if ( HasAOC )
						{
							lastProductionMark = GameEngine.Instance.CurrentCycle + GameEngine.Instance.CurrentYear * 1000;
							
							BuildProgress = -1;
						}
					}
				}
			}
			
			if ( BuildQueueIndex >= BuildQueue.Count )
			{
				if ( BuildQueue.Count > 0 ) BuildQueueIndex = 0;
				else BuildQueueIndex = -1;
			}
		}
		
		/// <summary>
		/// Cycles item in the build queue - not building -> building 1 -> building inf.
		/// </summary>
		/// <param name="itemId">Item ID</param>
		public void CycleQueueItem(string itemId)
		{
			int idx = -1;

			for ( int i = 0; i < BuildQueue.Count; i ++ )
			{
				if ( BuildQueue[i].ItemId == itemId ) 
				{
					idx = i;
					
					break;
				}
			}
			
			GuiMenu mnu = null;
			GuiButtonInfo btn = null;
			
			if ( GameEngine.Instance.GuiMenuModule.ContainsKey("factoryMenuItems") )
			{
				mnu = GameEngine.Instance.GuiMenuModule["factoryMenuItems"];
				
				foreach ( KeyValuePair<string, GuiButtonInfo> cur in mnu.Buttons )
				{
					if ( ((GuiButtonParamExecuteModuleMethod)cur.Value.Params["CycleQueueItem"]).methodParams[0].Value == itemId )
					{
						btn = cur.Value;
						
						break;
					}
				}
			}
			
			if ( idx == -1 ) // not building -> build 1
			{
				AddQueueItem(itemId, false);
				
				if ( btn != null ) btn.PictureColor = Color.Yellow;
			}
			else
			{
				if ( BuildQueue[idx].Repeat )
				{
					if ( BuildQueueIndex == idx ) BuildProgress = 0;
					if ( BuildQueueIndex >= idx ) BuildQueueIndex --;
					
					BuildQueue.RemoveAt(idx);
					
					if ( btn != null ) btn.PictureColor = Color.Red;
				}
				else
				{
					BuildQueue[idx].Repeat = true;
					
					if ( btn != null ) btn.PictureColor = Color.Green;
				}
			}
		}
		
		/// <summary>
		/// Adds a new item to the build queue
		/// </summary>
		/// <param name="itemId">Id of item to build</param>
		/// <param name="repeat">False if item is to be built only once</param>
		public void AddQueueItem(string itemId, bool repeat)
		{
			BuildQueue.Add( new ItemBuildInfo(itemId, repeat) );
		}
		
		public void RemoveQueueItem(string itemId)
		{
			for ( int i = 0; i < BuildQueue.Count; i ++ )
			{
				if ( BuildQueue[i].ItemId == itemId )
				{
					BuildQueue.RemoveAt(i);
					
					break;
				}
			}
		}
		
		public override void OnInitMenus()
		{
			GuiMenu mnu = new GuiMenu("factoryMenu");
			mnu.Picture = "gameScreen.Production";
			mnu.Location = new Point(120, 40);
			mnu.TextureArea = new Rectangle(0, 0, 680, 420);
			GameEngine.Instance.GuiMenuModule.Add(mnu.Id, mnu);

			GuiButtonInfo btn = new GuiButtonInfo("factoryMenu", new Rectangle(298, 368, 60, 40), "Send Team To Bay", "", true);
			btn.Params.Add("EvictTeam", new GuiButtonParamExecuteModuleMethod("EvictTeam", GetType().ToString()));
			mnu.Buttons.Add(GetType().Name + ".EvictTeam", btn);
			
			mnu = new GuiMenu("factoryMenuItems");
			mnu.Picture = "";
			mnu.Location = new Point(560 + 120, 0 + 40);
			mnu.TextureArea = new Rectangle(0, 0, 101, 334);
			GameEngine.Instance.GuiMenuModule.Add(mnu.Id, mnu);
			
			Dictionary<string, bool> pomQueue = new Dictionary<string, bool>(BuildQueue.Count);
			int i;
			
			for ( i = 0; i < BuildQueue.Count; i ++ )
			{
				pomQueue.Add(BuildQueue[i].ItemId, BuildQueue[i].Repeat);
			}
			
			i = -1;
			
			foreach ( KeyValuePair<string, StoreItem> sit in GameEngine.Instance.ItemTemplates )
			{
				if ( sit.Value.Type != ItemType.Mineral )
				{
					i++;
					
					if ( sit.Value.Researched )
					{
						btn = new GuiButtonInfo("miningStoreItemMenu", new Rectangle( (i / 16) * 42 + 7, (i % 16 * 20) + 7, 42, 20), sit.Value.Title, "gameScreen.Storage.ItemBlink", true, 1, 100);
						if ( !pomQueue.ContainsKey(sit.Key) )
						{
							btn.PictureColor = Color.Red;
						}
						else
						{
							if ( pomQueue[sit.Key] )
							{
								btn.PictureColor = Color.Green;
							}
							else
							{
								btn.PictureColor = Color.Yellow;
							}
						}

						GuiButtonParamExecuteModuleMethod par = new GuiButtonParamExecuteModuleMethod("CycleQueueItem", GetType().ToString());
						par.methodParams.Add(new MethodParamInfo(typeof(string).ToString(), sit.Key));
						btn.Params.Add("CycleQueueItem", par);
						
						mnu.Buttons.Add(GetType().Name + ".CycleQueueItem." + sit.Key, btn);
					}
				}
			}

			//366,350
//			btn = new GuiButtonInfo("factoryMenu", new Rectangle(366+58-8, 350+31-8, 15, 12), "AOC status", "gameScreen.Storage.ItemBlink", true, 10, 100);
//			mnu.Buttons.Add(btn);
		}
	}
	
	public class ItemBuildInfo
	{
		public string ItemId = null;
		public bool Repeat = false;
		
		public ItemBuildInfo(string ItemId, bool repeat)
		{
			this.ItemId = ItemId;
			this.Repeat = repeat;
		}
	}
}
