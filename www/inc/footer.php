<?php
    /* Deuteros X website
     * Copyright (C) 2008 Geddeth aka Achton Smidt Netherclift <achton@netherclift.net>
     *
     * This file is part of the Deuteros X website.

     * Deuteros X website is free software: you can redistribute it and/or modify
     * it under the terms of the GNU General Public License as published by
     * the Free Software Foundation, either version 3 of the License, or
     * (at your option) any later version.

     * Deuteros X website is distributed in the hope that it will be useful,
     * but WITHOUT ANY WARRANTY; without even the implied warranty of
     * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
     * GNU General Public License for more details.

     * You should have received a copy of the GNU General Public License
     * along with Deuteros X website.  If not, see <http://www.gnu.org/licenses/>.

     */
?>
            </div><!-- END #dxcontent -->

            <div id="dxfooter">
                The "Deuteros" name and Deuteros Logo are Copyright (c) 1991 Activision
            </div>

        </div><!-- END #dxpage -->

        </div><!-- END #dxpageouter -->

        <script type="text/javascript">
            var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
            document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
        </script>
        <script type="text/javascript">
            var pageTracker = _gat._getTracker("UA-318471-12");
            pageTracker._initData();
            pageTracker._trackPageview();
        </script>
<?php

    // don't output HTML head if we're inside the wiki or forum templates
    $path = explode('/', $_SERVER['REQUEST_URI']);
    if (strcasecmp($path[1], 'forum') != 0
        &&  strcasecmp($path[1], 'wiki') != 0
        &&  strcasecmp($path[1], 'mediawiki') != 0) {
?>
    </body>
</html>
<?php
    }
?>