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

    // test whether special beta cookie is set, otherwise redirect to forum
    if ($_COOKIE['DeuterosX'] != md5($GLOBALS['DX_COOKIE_PWD'])) {
        header('Location: ' . $GLOBALS['DX_SITE_URL'] . 'forum/');
    }

    // initiate phpBB3 session
    define('IN_PHPBB', true);
    $phpbb_root_path = $GLOBALS['DX_PHPBB3_URL'];
    $phpEx = substr(strrchr(__FILE__, '.'), 1);
    require_once($GLOBALS['DX_SITE_PATH'] . $GLOBALS['DX_PHPBB3_URL'] . 'common.php');

    // start the phpBB3 session
    $user->session_begin();
    $auth->acl($user->data);
    $user->setup();

    // get readable forums
    $can_read_forum = $auth->acl_getf('f_read', TRUE);
    $forum_id_ary = array_keys($can_read_forum);
    unset($can_read_forum);

    // include the sitewide header
    require_once($GLOBALS['DX_SITE_PATH'] . 'inc/header.php');

    // prepare news posts
    $newsPostsArray = getPhpBB3Posts($db, $forum_id_ary, 4, $GLOBALS['DX_PHPBB3_BOARD']);
    if ($newsPostsArray) {
        // get the HTML for the array of posts
        $newsPostsHTML = getPhpBB3PostsHTML($newsPostsArray);
    }

    // prepare latest forum posts
    $latestPostsArray = getPhpBB3Posts($db, $forum_id_ary, 8);
    if ($latestPostsArray) {
        // get the HTML for the array of posts
        $latestPostsHTML = getPhpBB3LatestPostsHTML($latestPostsArray);
    }

     if ($user->data['is_registered']) {
        echo 'Welcome ' . $user->data['username']; //User is already logged in
    }

?>
                <table>
                <tr>
                <td id="dxmaincolumn">
                    <?php
                        echo $newsPostsHTML;
                    ?>
                </td>

                <td id="dxrightcolumn">

                    <h2 id="slide_search">Search</h2>
                    <div class="dxrightbox" id="slidebox_search">
                        TEMP
                    </div>

                    <h2 id="slide_login">Login</h2>
                    <div class="dxrightbox" id="slidebox_login">
                        TEMP
                    </div>

                    <h2 id="slide_forum">Forum</h2>
                    <div class="dxrightbox" id="slidebox_forum">
<?php
        echo $latestPostsHTML;

?>
                    </div>

                    <h2 id="slide_polls">Polls</h2>
                    <div class="dxrightbox" id="slidebox_polls">
                        TEMP
                    </div>

                </td>
                </tr>
                </table>


<?php
    //closeDBConnection($dbConn);

    // include the generic footer
    require_once($GLOBALS['DX_SITE_PATH'] . 'inc/footer.php');
?>
