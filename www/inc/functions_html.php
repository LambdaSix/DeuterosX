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

    /** This function generates the HTML for the horisontal graphical navigation in the header.
     *  It checks the current URL to find out whether to show the "activated" graphic or not for each link.
     */
    function getNavBarHTML() {
        // the global site URL
        $imgSrc = $GLOBALS['DX_SITE_URL'];
        // the array of navigation items, and the urls that correspond to each item
        $links = array('news' => array('news'),
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
            if (in_array(strtolower($path[1]), $urls)) {
                $suffix = 'hv';
            }
            // output the corresponding HTML
            $HTML .= '<a href="' . $imgSrc . $name . '/"><img src="' . $imgSrc . 'img/header_' . $name . '_' . $suffix . '.jpg" alt="Logo" /></a>';
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
        $sortedPostsArray = array();

        // sort the posts by topic
        foreach ($postsArray as $post) {
            $sortedPostsArray[$post['topic_id']][] = $post;
        }

        // treat each topic as 1 post and the rest as comments, and build the HTML
        foreach ($sortedPostsArray as $topicID => $posts) {
            // get the first post
            $firstPost = array_shift($posts);
            // format the post date
            $firstPostDate = date('jS M Y', $firstPost['post_time']);
            // the rest of the posts are comments
            $numComments = count($posts);
            // get some info on the last post, if present
            $lastPost = ($numComments > 0 ? array_pop($posts) : $firstPost);

            // setup the HTML for the current topic
            $HTML .= '<div class="dxnewspost">' . PHP_EOL
                   . '<div class="dxnewspostheader">'
                   . '<h4>by ' . $firstPost['username'] . ', ' . $firstPostDate . '</h4>'
                   . '<h1>'
                   . '<a href="' . $GLOBALS['DX_SITE_URL'] . $GLOBALS['DX_PHPBB3_URL'] . 'viewtopic.php?p=' . $firstPost['post_id'] . '#p' . $firstPost['post_id'] . '">'
                   . $firstPost['post_subject']
                   . '</a>'
                   . '</h1>' . PHP_EOL
                   . '</div>'
                   . '<p>';
            // call the special phpBB3 functions that must be setup in the calling php file (e.g. /index.php)
            $HTML .= generate_text_for_display(smiley_text($firstPost['post_text']), $firstPost['bbcode_uid'], $firstPost['bbcode_bitfield'], TRUE);
            $HTML .= '</p>' . PHP_EOL
                   . '<div class="dxnewspostfooter">'
                   . '<a href="' . $GLOBALS['DX_SITE_URL'] . $GLOBALS['DX_PHPBB3_URL'] . 'viewtopic.php?p=' . $lastPost['post_id'] . '#p' . $lastPost['post_id'] . '">'
                   . $numComments . ' comment' . ($numComments != 1 ? 's' : NULL)
                   . '</a>'
                   . '</div>' . PHP_EOL
                   . '</div>' . PHP_EOL;
        }

        // return all HTML for all topics from this array of posts
        return $HTML;

    }

?>