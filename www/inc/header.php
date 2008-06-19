<?php
    /* Deuteros.org website
     * Copyright (C) 2008 Achton Netherclift <achton@netherclift.net>
     *
     * This file is part of Deuteros.org website.

     * Deuteros.org website is free software: you can redistribute it and/or modify
     * it under the terms of the GNU General Public License as published by
     * the Free Software Foundation, either version 3 of the License, or
     * (at your option) any later version.

     * Deuteros.org website is distributed in the hope that it will be useful,
     * but WITHOUT ANY WARRANTY; without even the implied warranty of
     * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
     * GNU General Public License for more details.

     * You should have received a copy of the GNU General Public License
     * along with Deuteros.org website.  If not, see <http://www.gnu.org/licenses/>.

     */

    require_once($GLOBALS['DX_SITE_PATH'] . 'inc/functions.php');
    require_once($GLOBALS['DX_SITE_PATH'] . 'inc/functions_html.php');

    // don't output HTML head and body if we're inside the wiki or forum templates
    $path = explode('/', $_SERVER['REQUEST_URI']);
    if (strcasecmp($path[1], 'forum') != 0
        &&  strcasecmp($path[1], 'wiki') != 0
        &&  strcasecmp($path[1], 'mediawiki') != 0) {
        echo '<?xml version="1.0" encoding="UTF-8"?>' . PHP_EOL;
?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" dir="ltr" lang="en-gb" xml:lang="en-gb">
    <head>
        <title>Deuteros.org &bull; <?php echo ($path[1] ? $path[1] : 'Front page'); ?></title>

        <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
        <meta http-equiv="content-style-type" content="text/css" />
        <meta http-equiv="content-language" content="en-gb" />

        <link href="<?php echo $GLOBALS['DX_SITE_URL']; ?>inc/style.css" rel="stylesheet" type="text/css" />

        <script type="text/javascript" src="<?php echo $GLOBALS['DX_SITE_URL']; ?>inc/mootols/mootools.js"></script>
        <script type="text/javascript" src="<?php echo $GLOBALS['DX_SITE_URL']; ?>inc/functions.js"></script>
    </head>

    <body>
<?php
   } // END
?>
        <script type="text/javascript">
        <!--
        //If the browser is W3 DOM compliant, execute setImageSwaps function
        if (document.getElementsByTagName && document.getElementById) {
            if (window.addEventListener) window.addEventListener('load', setImageSwaps, false);
            else if (window.attachEvent) window.attachEvent('onload', setImageSwaps);
        }
        -->
        </script>

        <div id="dxpageouter">

        <div id="dxpage">
            <div id="dxheader">
                <div>
                    <a href="/">
                        <img src="<?php echo $GLOBALS['DX_SITE_URL']; ?>img/header_top.gif" alt="Logo" />
                    </a>
                </div>
                <div><?php echo getNavBarHTML(); ?></div>
            </div><!-- END #dxheader -->

            <div id="dxcontent">

