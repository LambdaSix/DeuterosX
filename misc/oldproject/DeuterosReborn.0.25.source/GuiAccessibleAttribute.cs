using System;
using System.Collections.Generic;
using System.Text;

namespace Deuteros
{
    public class GuiAccessibleAttribute: Attribute
    {
        private bool isAccessible = true;
        /// <summary>
        /// Gets whether this member is accessible from Gui "scripting"
        /// </summary>
        public bool IsAccessible
        {
            get { return this.isAccessible; }
        }

        public GuiAccessibleAttribute(bool isAccessible)
        {
            this.isAccessible = isAccessible;
        }
    }
}
