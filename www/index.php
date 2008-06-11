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

    require_once('inc/config.php');

    // test whether special beta cookie is set, otherwise redirect to forum
    if ($_COOKIE['DeuterosX'] != md5($GLOBALS['DX_COOKIE_PWD'])) {
        header('Location: ' . $GLOBALS['DX_SITE_URL'] . 'forum/');
    }

    // set some phpBB3 variables and include the necessary files
    define('IN_PHPBB', true);
    $phpbb_root_path = $GLOBALS['DX_PHPBB3_URL'];
    $phpEx = 'php';
    require_once($GLOBALS['DX_SITE_PATH'] . $GLOBALS['DX_PHPBB3_URL'] . 'common.php');
    require_once($GLOBALS['DX_SITE_PATH'] . $GLOBALS['DX_PHPBB3_URL'] . 'includes/functions_display.php');

    // initiate phpBB3 session
    $user->session_begin();
    $auth->acl($user->data);
    $user->setup();

    // include the sitewide header
    require_once($GLOBALS['DX_SITE_PATH'] . 'inc/header.php');

    // get the raw forum posts
    $dbConn = getDBConnection($GLOBALS['DX_PHPBB3_DBSERVER'],
                              $GLOBALS['DX_PHPBB3_DB'],
                              $GLOBALS['DX_PHPBB3_DBUSER'],
                              $GLOBALS['DX_PHPBB3_DBPWD']);
    $postsArray = getPhpBB3Posts($GLOBALS['DX_PHPBB3_BOARD']);
    closeDBConnection($dbConn);

    if ($postsArray) {
        // get the HTML for the array of posts
        $postsHTML = getPhpBB3PostsHTML($postsArray);
    }
?>
                <table>
                <tr>
                <td id="dxmaincolumn">
                    <?php
                        echo $postsHTML;
                    ?>
                </td>
                <td id="dxrightcolumn">
                    SEARCH
                    <br />
                    POLLS
                    <br />
                    LOGIN
                    <br />
                    LATEST FORUM TOPICS
                    <br />
                </td>
                </tr>
                </table>


<?php
    // include the generic footer
    require_once($GLOBALS['DX_SITE_PATH'] . 'inc/footer.php');
?>
