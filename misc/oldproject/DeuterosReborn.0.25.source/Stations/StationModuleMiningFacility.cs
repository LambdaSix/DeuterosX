using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using Deuteros.Gui;
using Deuteros.Items;
using Deuteros.Universe;

using Microsoft.DirectX.Direct3D;

namespace Deuteros.Stations
{
    public class StationModuleMiningFacility: StationModuleBase
    {
        public StationModuleMiningFacility()
        {
            mines = new Dictionary<string, MiningOperationInfo>();

            foreach (KeyValuePair<string, StoreItem> min in GameEngine.Instance.ItemTemplates)
            {
                if (min.Value.Type == ItemType.Mineral)
                {
                    mines.Add(min.Key, new MiningOperationInfo());
                }
            }

            buttonInfo = new GuiButtonInfo("leftGuiBottom", new Rectangle(60, 160, 60, 40), "Mining Facility", "leftGui.Resources", true);
            buttonInfo.Params.Add("NextScreen", new GuiButtonParamChangeScreen(GameScreen.StationModule));
            buttonInfo.Params.Add("NextStationModule", new GuiButtonParamChangeStationModule(this.GetType().Name));
            buttonIndex = 0;
        }

        private int derricks = 1;
        /// <summary>
        /// Gets or sets the current number of derricks on this module
        /// </summary>
        public int Derricks
        {
            get { return derricks; }
            set { derricks = value; }
        }

        private Dictionary<string, MiningOperationInfo> mines = null;
        /// <summary>
        /// Gets current mines under this module.
        /// </summary>
        public Dictionary<string, MiningOperationInfo> Mines
        {
            get { return mines; }
        }

        private SpaceItem location = null;
        /// <summary>
        /// Gets the body this module binds to.
        /// </summary>
        public SpaceItem Location
        {
            get
            {
                if (location == null)
                {
                    Galaxy gal;
                    SolarSystem sys;
                    SpaceItem body;

                    string[] locstr = parent.Location.Split('.');

                    if (GameEngine.Instance.Universe.ContainsKey(locstr[0])) gal = GameEngine.Instance.Universe[locstr[0]];
                    else return null;

                    if (gal.Systems.ContainsKey(locstr[1])) sys = gal.Systems[locstr[1]];
                    else return null;

                    if (sys.Bodies.ContainsKey(locstr[2])) body = sys.Bodies[locstr[2]];
                    else return null;

                    return body;
                }
                else return location;
            }
        }

        private StationModuleMiningStore store = null;
        /// <summary>
        /// Gets the mining store this module uses to store extracted materials.
        /// </summary>
        public StationModuleMiningStore Store
        {
            get
            {
                if (this.store == null || this.store.Parent != this.Parent)
                {
                    // Find a new store
                    foreach (StationModuleBase mod in Parent.Modules)
                    {
                        if (mod is StationModuleMiningStore)
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
        /// Adds another derrick to the mining facility
        /// </summary>
        public void AddDerrick()
        {
            if (Store.CheckStorage(true, "Derrick", 1))
            {
                Store.RemoveFromStorage(true, "Derrick", 1);

                derricks++;
            }
        }

        public override void OnNextTurn()
        {
            base.OnNextTurn();

            foreach (KeyValuePair<string, MiningOperationInfo> moi in Mines)
            {
                if (moi.Value.SurveyProgress > 0) moi.Value.SurveyProgress--;
                else if (moi.Value.SurveyProgress == 0)
                {
                    // Generate new "vein"
                    moi.Value.CurrentMiningLimit = Location.Minerals[moi.Key].NewSurvey();

                    moi.Value.SurveyProgress = -1;
                }
                else if (moi.Value.SurveyProgress == -1)
                {
                    int amnt = -GameEngine.Instance.ItemTemplates[moi.Key].BuildTime * derricks;

                    if (moi.Value.CurrentMiningLimit < amnt) amnt = moi.Value.CurrentMiningLimit;

                    moi.Value.CurrentMiningLimit -= amnt;

                    Store.AddToStorage(false, moi.Key, amnt);

                    if (moi.Value.CurrentMiningLimit == 0)
                    {
                        moi.Value.SurveyProgress = 10;
                    }
                }
            }
        }

        public override void OnRenderInterface(Microsoft.DirectX.Direct3D.Sprite overlay)
        {
            base.OnRenderInterface(overlay);

            int i = 0;
            foreach (KeyValuePair<string, MiningOperationInfo> moi in Mines)
            {
                if ( moi.Value.CurrentMiningLimit != 0 ) GameEngine.Instance.GuiFonts["fntGui"].DXFont.DrawText(overlay, moi.Value.CurrentMiningLimit.ToString(), new Rectangle(130, 40 + i * 20, 70, 20), DrawTextFormat.Right | DrawTextFormat.VerticalCenter, Color.White);
                else GameEngine.Instance.GuiFonts["fntGui"].DXFont.DrawText(overlay, "survey", new Rectangle(130, 40 + i * 20, 70, 20), DrawTextFormat.Right | DrawTextFormat.VerticalCenter, Color.Red);
                GameEngine.Instance.GuiFonts["fntGui"].DXFont.DrawText(overlay, GameEngine.Instance.ItemTemplates[moi.Key].Title, 220, 40 + i * 20, Color.White);

                i++;
            }
        }
    }

    /// <summary>
    /// Mining operation information.
    /// </summary>
    public class MiningOperationInfo
    {
        private int currentMiningLimit = 0;
        /// <summary>
        /// Resources in the currently running mine
        /// </summary>
        public int CurrentMiningLimit
        {
            get { return currentMiningLimit; }
            set { currentMiningLimit = value; }
        }

        private int surveyProgress = 10;
        /// <summary>
        /// The survey progress for this mine - when zero, mine is ready to be mined.
        /// </summary>
        public int SurveyProgress
        {
            get { return surveyProgress; }
            set { surveyProgress = value; }
        }
    }
}
