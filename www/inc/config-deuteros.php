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

    // Absolute filesystem path to site root
    $GLOBALS['DX_SITE_PATH']        = '/var/www/sitepath/';
    // Absolute URL to site root
    $GLOBALS['DX_SITE_URL']         = 'http://deuterosx.org/';
    // Relative path/URL to forum
    $GLOBALS['DX_PHPBB3_URL']       = 'forum/';

    // cookie password for developer access
    $GLOBALS['DX_COOKIE_PWD']       = 'cookiepass';

    // PhpBB3 database config
    $GLOBALS['DX_PHPBB3_DBSERVER']  = 'localhost';
    $GLOBALS['DX_PHPBB3_DB']        = 'forumdbname';
    $GLOBALS['DX_PHPBB3_DBUSER']    = 'forumdbuser';
    $GLOBALS['DX_PHPBB3_DBPWD']     = 'forumdbpass';
    $GLOBALS['DX_PHPBB3_BOARD']     = '1'; // board for frontpage

    // MediaWiki database config
    $GLOBALS['DX_WIKI_DBSERVER']    = 'localhost';
    $GLOBALS['DX_WIKI_DB']          = 'wikidbname';
    $GLOBALS['DX_WIKI_DBUSER']      = 'wikidbuser';
    $GLOBALS['DX_WIKI_DBPWD']       = 'wikidbpass';

    // IRC config
    $GLOBALS['DX_IRC_SERVER']       = 'irc.domain.org';
    $GLOBALS['DX_IRC_SERVER_PORT']  = 6668;
    $GLOBALS['DX_IRC_SERVER_CHAN']  = '#channel';

?>
