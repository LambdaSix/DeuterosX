/*
 * Created by SharpDevelop.
 * User: Luaan
 * Date: 12.7.2007
 * Time: 23:20
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;

using Deuteros.Items;

namespace Deuteros.Universe
{
	/// <summary>
	/// Defines a celestial body under solar system.
	/// </summary>
	public class SpaceItem
	{
		public SpaceItem()
		{
            minerals = new Dictionary<string, SurveyInfo>();

            foreach (KeyValuePair<string, StoreItem> min in GameEngine.Instance.ItemTemplates)
            {
                if (min.Value.Type == ItemType.Mineral)
                {
                    minerals.Add(min.Key, new SurveyInfo());
                }
            }
		}

        private string name = "Earth";
        /// <summary>
        /// Gets or sets the body name
        /// </summary>
        [Description("Name of this body")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string orbit = "Sol";
        /// <summary>
        /// Gets or sets the body this body is orbiting
        /// </summary>
        [Description("Parent body")]
        public string Orbit
        {
            get { return orbit; }
            set { orbit = value; }
        }

        private Dictionary<string, SurveyInfo> minerals = null;
        /// <summary>
        /// Gets the survey information
        /// </summary>
        [Browsable(false)]
        public Dictionary<string, SurveyInfo> Minerals
        {
            get { return minerals; }
        }

        public static SpaceItem InvokeCreate(string typ)
        {
            SpaceItem si = StructXml.CreateInstanceFromType(typ) as SpaceItem;

            if (si == null) return new SpaceItem();
            else return si;
        }

        public void FromXml(XmlNode node)
        {
            this.name = node["Name"].InnerText;
            this.orbit = node["Orbit"].InnerText;

            SurveyInfo mineral;
            foreach ( XmlNode xMineral in node["Surveys"].ChildNodes )
            {
                mineral = SurveyInfo.InvokeCreate();
                mineral.FromXml(xMineral);
                this.minerals[xMineral.Name] = mineral;
            }
        }

        public void ToXml(XmlNode node)
        {
            XmlDocument doc = node.OwnerDocument;

            XmlElement el = doc.CreateElement("Name");
            el.InnerText = this.name;
            node.AppendChild(el);

            el = doc.CreateElement("Orbit");
            el.InnerText = this.orbit;
            node.AppendChild(el);

            XmlNode surveys = doc.CreateElement("Surveys");
            node.AppendChild(surveys);

            XmlNode xMineral;
            foreach (KeyValuePair<string, SurveyInfo> mineral in this.minerals)
            {
                xMineral = doc.CreateElement(mineral.Key);
                surveys.AppendChild(xMineral);
                mineral.Value.ToXml(xMineral);
            }
        }
	}

    /// <summary>
    /// Class for holding information about survey of body
    /// </summary>
    public class SurveyInfo
    {
        //private int surveyProgress = 0;
        ///// <summary>
        ///// Gets or sets the current survey progress
        ///// </summary>
        //public int SurveyProgress
        //{
        //    get { return surveyProgress; }
        //    set { surveyProgress = value; }
        //}

        private bool mineralMineable = false;
        /// <summary>
        /// Gets or sets whether this mineral is mineable on this body
        /// </summary>
        public bool MineralMineable
        {
            get { return mineralMineable; }
            set { mineralMineable = value; }
        }

        //private int currentMineLimit = 0;
        ///// <summary>
        ///// Gets or sets current mine resource capacity
        ///// </summary>
        //public int CurrentMineLimit
        //{
        //    get { return currentMineLimit; }
        //    set { currentMineLimit = value; }
        //}

        private int minMineLimit = 1000;
        /// <summary>
        /// Gets or sets the minimal mine resource capacity on a new survey
        /// </summary>
        public int MinMineLimit
        {
            get { return minMineLimit; }
            set { minMineLimit = value; }
        }

        private int maxMineLimit = 150000;
        /// <summary>
        /// Gets or sets the maximal mine resource capacity on a new survey
        /// </summary>
        public int MaxMineLimit
        {
            get { return maxMineLimit; }
            set { maxMineLimit = value; }
        }

        //private string mineral = "Iron";
        ///// <summary>
        ///// Gets or sets the mineral this survey is about
        ///// </summary>
        //public string Mineral
        //{
        //    get { return mineral; }
        //    set { mineral = value; }
        //}

        public SurveyInfo()
        {
        }

        public int NewSurvey()
        {
            return new Random(GameEngine.Instance.RandomSeed++).Next(this.minMineLimit, this.maxMineLimit);
        }

        public static SurveyInfo InvokeCreate()
        {
            return new SurveyInfo();
        }

        public void FromXml(XmlNode node)
        {
            //this.currentMineLimit = node.Attributes["CurrentMineLimit"].Value;
            this.maxMineLimit = int.Parse(node.Attributes["MaxMineLimit"].Value);
            this.mineralMineable = bool.Parse(node.Attributes["MineralMineable"].Value);
            this.minMineLimit = int.Parse(node.Attributes["MinMineLimit"].Value);
            //this.surveyProgress = node.Attributes["SurveyProgress"].Value;
        }

        public void ToXml(XmlNode node)
        {
            XmlDocument doc = node.OwnerDocument;
            XmlAttribute at;

            at = doc.CreateAttribute("MaxMineLimit");
            at.Value = this.maxMineLimit.ToString();
            node.Attributes.Append(at);

            at = doc.CreateAttribute("MineralMineable");
            at.Value = this.mineralMineable.ToString();
            node.Attributes.Append(at);

            at = doc.CreateAttribute("MinMineLimit");
            at.Value = this.minMineLimit.ToString();
            node.Attributes.Append(at);
        }
    }
}
