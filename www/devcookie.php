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

    require_once('inc/config.php');

    setcookie('DeuterosX', md5($GLOBALS['DX_COOKIE_PWD']), time()+2592000);

    if ($_COOKIE['DeuterosX'] == md5($GLOBALS['DX_COOKIE_PWD'])) {
        echo 'Cookie was set properly.'
           . '<br><br>'
           . '<a href="' . $GLOBALS['DX_SITE_URL'] . '">Go to DX website</a>';
    } else {
        echo '<a href="devcookie.php">Check if cookie was set properly</a>';
    }
?>