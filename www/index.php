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

    // initiate phpBB3 session
    define('IN_PHPBB', true);
    $phpbb_root_path = $GLOBALS['DX_PHPBB3_URL'];
    $phpEx = substr(strrchr(__FILE__, '.'), 1);
    require_once($GLOBALS['DX_SITE_PATH'] . $GLOBALS['DX_PHPBB3_URL'] . 'common.php');

    // start the phpBB3 session
    $user->session_begin();
    $auth->acl($user->data);
    $user->setup();

    // test whether user is logged in to the forum, otherwise redirect to forum
    if (!$user->data['is_registered'] || !$user->data['group_id'] == $GLOBALS['DX_PHPBB3_ADMGROUP']) {
        header('Location: ' . $GLOBALS['DX_SITE_URL'] . 'forum/');
    }

    // get readable forums
    $can_read_forum = $auth->acl_getf('f_read', TRUE);
    $forum_id_ary = array_keys($can_read_forum);
    unset($can_read_forum);

    // include the sitewide header
    require_once($GLOBALS['DX_SITE_PATH'] . 'inc/header.php');

    // prepare news posts
    $newsPostsArray = getPhpBB3Posts($db, 4, FALSE, $GLOBALS['DX_PHPBB3_BOARD']);
    if ($newsPostsArray) {
        // get the HTML for the array of posts
        $newsPostsHTML = getPhpBB3PostsHTML($newsPostsArray);
    }

    // prepare latest forum posts
    $latestPostsArray = getPhpBB3Posts($db, 4, $forum_id_ary);
    if ($latestPostsArray) {
        // get the HTML for the array of posts
        $latestPostsHTML = getPhpBB3LatestPostsHTML($latestPostsArray);
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

                    <h2>Search</h2>
                    <div class="dxrightbox">
                    <div id="customsearch" style="width: 100%;">Loading...</div>
                        <script src="http://www.google.com/jsapi" type="text/javascript"></script>
                        <script type="text/javascript">
                            google.load('search', '1');
                            google.setOnLoadCallback(function(){
                                    var searcher = new google.search.CustomSearchControl('005550185465920181014:5m3ax8_sf_g');
                                    searcher.setResultSetSize(google.search.Search.SMALL_RESULTSET);
                                    searcher.setLinkTarget(google.search.Search.LINK_TARGET_SELF);
                                    searcher.draw('customsearch');
                            }, true);
                        </script>
                    </div>

                    <h2>Login</h2>
                    <div class="dxrightbox">
                    <?php
                    if ($user->data['is_registered']) {
                        echo 'Welcome <span style="color:#' . $user->data['user_colour'] . ';">'.$user->data['username'].'</span>';
                        echo '<br />';
                        echo '<a title="Logout" href="'.$GLOBALS['DX_SITE_URL'].$GLOBALS['DX_PHPBB3_URL'].'ucp.php?mode=logout&amp;sid='.$user->data['session_id'].'">Logout</a>';
                    }
                    ?>

                    </div>

                    <h2>Forum</h2>
                    <div class="dxrightbox">
                    <?php echo $latestPostsHTML; ?>
                    </div>

                </td>
                </tr>
                </table>


<?php
    //closeDBConnection($dbConn);

    // include the generic footer
    require_once($GLOBALS['DX_SITE_PATH'] . 'inc/footer.php');
?>
