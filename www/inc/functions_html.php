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

    /** This function generates the HTML for the horisontal graphical navigation in the header.
     *  It checks the current URL to find out whether to show the "activated" graphic or not for each link.
     */
    function getNavBarHTML() {
        // the global site URL
        $imgSrc = $GLOBALS['DX_SITE_URL'];
        // the array of navigation items, and the relative url(s) that correspond to each item
        $links = array('home' => array('home'),
                       'about' => array('about'),
                       'forum' => array('forum'),
                       'wiki' => array('wiki', 'mediawiki'),
                       'developers' => array('developers'));
        // get the current URL
        $path = explode('/', $_SERVER['REQUEST_URI']);

        // output the leftmost part of the header
        $HTML = '<img src="' . $imgSrc . 'img/header_bottom_left.jpg" alt="Logo" />';

        // iterate over the array of navigation items
        foreach ($links as $name => $urls) {
            // the standard image suffix for non-active items (ie. we are not visiting that part of the site)
            $suffix = 'nm';
            // if the URL corresponds to this navigation item, change the image file suffix

            if (in_array(strtolower($path[1]), $urls) || ($path[1] == '' && $name == 'home')) {
                $suffix = 'hv';
            }

            if ($name == 'home') {
                $nameURL = '';
            } else {
                $nameURL = $name . '/';
            }

            // output the corresponding HTML
            $HTML .= '<a href="' . $imgSrc . $nameURL . '"><img src="' . $imgSrc . 'img/header_' . $name . '_' . $suffix . '.jpg" alt="Logo" /></a>';
        }
        // finish with the rightmost part of the header
        $HTML .= '<img src="' . $imgSrc . 'img/header_bottom_right.jpg" alt="Logo" />';

        return $HTML;
    }


    /** This function generates the HTML for a forum for e.g. the front page.
     *
     */
    function getPhpBB3PostsHTML($postsArray) {

        $HTML = NULL;

        foreach ($postsArray as $post) {
            // format the post date
            $postDate = date('jS M Y', $post['topic_time']);

            // setup the HTML for the current topic
            $HTML .= '<div class="dxnewspost">' . PHP_EOL
                   . '<div class="dxnewspostheader">'
                   . '<span class="meta">by ' . $post['username'] . ', ' . $postDate . '</span>'
                   . '<h1>'
                   . '<a href="' . $GLOBALS['DX_SITE_URL'] . $GLOBALS['DX_PHPBB3_URL'] . 'viewtopic.php?p=' . $post['post_id'] . '#p' . $post['post_id'] . '">'
                   . $post['topic_title']
                   . '</a>'
                   . '</h1>' . PHP_EOL
                   . '</div>'
                   . '<p>';
            // call the special phpBB3 functions that must be setup in the calling php file (e.g. /index.php)
            $HTML .= generate_text_for_display(smiley_text($post['post_text']), $post['bbcode_uid'], $post['bbcode_bitfield'], TRUE);
            $HTML .= '</p>' . PHP_EOL
                   . '<div class="dxnewspostfooter">'
                   . '<a href="' . $GLOBALS['DX_SITE_URL'] . $GLOBALS['DX_PHPBB3_URL'] . 'viewtopic.php?'
                   . 'f=' . $post['forum_id'] . '&amp;t=' . $post['topic_id'] . '&amp;p=' . $post['topic_last_post_id'] . '#p' . $post['topic_last_post_id'] . '">'
                   . $post['topic_replies'] . ' comment' . ($post['topic_replies'] != 1 ? 's' : NULL)
                   . '</a>'
                   . '</div>' . PHP_EOL
                   . '</div>' . PHP_EOL;
        }

        // return all HTML for all topics from this array of posts
        return $HTML;

    }


    /** This function generates the HTML for a list of recent posts.
     *
     */
    function getPhpBB3LatestPostsHTML($postsArray) {

        $maxChars = 16;
        $HTML = NULL;

        foreach ($postsArray as $post) {

            if (strlen($post['topic_title']) > $maxChars) {
                $topic_title = substr($post['topic_title'], 0, $maxChars) . '...';
            } else {
                $topic_title = $post['topic_title'];
            }

            $topic_url = $GLOBALS['DX_SITE_URL'] . $GLOBALS['DX_PHPBB3_URL'] . 'viewtopic.php?'
                         . 'f=' . $post['forum_id'] . '&amp;t=' . $post['topic_id'] . '&amp;p=' . $post['topic_last_post_id'] . '#p' . $post['topic_last_post_id'];

            $HTML .= '<a href="' . $topic_url . '">' . $topic_title . '</a> <small>by ' . $post['topic_last_poster_name'] . '</small><br />';
        }

        return $HTML;
    }

?>