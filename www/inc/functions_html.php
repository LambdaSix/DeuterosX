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
        $links = array('home' => array('home/'),
                       'about' => array('wiki/DeuterosX:About'),
                       'forum' => array('forum/'),
                       'wiki' => array('wiki/', 'mediawiki/'),
                       'developers' => array('developers/'));
        // get the current URL as an array
        $path = explode('/', $_SERVER['REQUEST_URI']);
        // also get it as a string
        $pathFull = $_SERVER['REQUEST_URI'];
        // trim forward slash from front of URL
        if ($pathFull[0] == '/') {
            $pathFull = substr($pathFull, 1);
        }

        // output the leftmost part of the header
        $HTML = '<img src="' . $imgSrc . 'img/header_bottom_left.jpg" alt="Logo" />';

        // intiate variable
        $hoverFound = FALSE;
        // iterate over the array of navigation items
        foreach ($links as $name => $urls) {
            // the standard image suffix for non-active items (ie. we are not visiting that part of the site)
            $suffix = 'nm';
            // if the URL corresponds to this navigation item, change the image file suffix
            if ($hoverFound == FALSE && (in_array($pathFull, $urls) || in_array($path[1], $urls) || ($path[1] == '' && $name == 'home'))) {
                $suffix = 'hv';
                // set variable to indicate we found a match in the $links array
                $hoverFound = TRUE;
            }

            $firstURL = array_pop($urls);
            if ($name == 'home') {
                $firstURL = '';
            }

            // output the corresponding HTML
            $HTML .= '<a href="' . $imgSrc . $firstURL . '"><img src="' . $imgSrc . 'img/header_' . $name . '_' . $suffix . '.jpg" alt="Logo" /></a>';
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
            $postDate = date('M jS Y', $post['topic_time']);

            // setup the HTML for the current topic
            $HTML .= <<<EOP
            <div class="dxnewspost">
                <h1>
                    <a href="{$GLOBALS['DX_SITE_URL']}{$GLOBALS['DX_PHPBB3_URL']}viewtopic.php?p={$post['post_id']}#p{$post['post_id']}">
                    {$post['topic_title']}
                    </a>
                </h1>
                <p>
EOP;
            // call the special phpBB3 functions that must be setup in the calling php file (e.g. /index.php)
            $HTML .= generate_text_for_display($post['post_text'], $post['bbcode_uid'], $post['bbcode_bitfield'], 7);
            $plural_s = ($post['topic_replies'] != 1 ? 's' : NULL);
            $HTML .= <<<EOP
                </p>
                <div class="dxnewspostfooter">
                    {$postDate} // 
                    <a href="{$GLOBALS['DX_SITE_URL']}{$GLOBALS['DX_PHPBB3_URL']}viewtopic.php?f={$post['forum_id']}&amp;t={$post['topic_id']}&amp;p={$post['topic_last_post_id']}#p{$post['topic_last_post_id']}">
                    {$post['topic_replies']} comment{$plural_s} 
                    </a>
                </div>
            </div>
EOP;
        }

        // return all HTML for all topics from this array of posts
        return $HTML;

    }


    /** This function generates the HTML for a list of recent posts.
     *
     */
    function getPhpBB3LatestPostsHTML($postsArray) {

        $maxTopicChars = 32;
        $maxPostChars = 128;
        $HTML = NULL;

        foreach ($postsArray as $post) {

            if (strlen($post['topic_title']) > $maxTopicChars) {
                $topic_title = substr($post['topic_title'], 0, $maxTopicChars) . '...';
            } else {
                $topic_title = $post['topic_title'];
            }
            
            $post_time = strftime('%e %b %Y', $post['topic_last_post_time']);
            
            $post_text = $post['post_text'];

            // attempt to ditch quoted replies
            $post_lastquotepos = strrpos($post_text, '[/quote');
            if ($post_lastquotepos !== FALSE) {
                $post_text  = substr($post_text, $post_lastquotepos+strlen('[/quote:')+strlen($post['bbcode_uid'])+3);
            }
            
            // convert bbcode and strip HTML from post
            $post_text = generate_text_for_display($post_text, $post['bbcode_uid'], $post['bbcode_bitfield'], 1);
            $post_text = strip_tags($post_text, '<br>');
            
            // if longer than maxpostchars, we do a little magic to shorten the post
            if (strlen($post_text) > $maxPostChars) {
                // search for first punctuation after maxPostChars
                $post_endpos1 = strpos($post_text, '.', $maxPostChars);
                $post_endpos2 = strpos($post_text, ',', $maxPostChars);                

                // if punctuation found
                if ($post_endpos1 !== FALSE || $post_endpos2 !== FALSE) {
                
                    // avoid  min() below enterpreting FALSE as 0 (lowest)
                    if ($post_endpos1 === FALSE) $post_endpos1 = 9999;
                    if ($post_endpos2 === FALSE) $post_endpos2 = 9999;
                    
                    // cut string at first found punctuation
                    $post_text = substr($post_text, 0, min(array($post_endpos1, $post_endpos2))+1) . ' [...]';
                } else {
                    // else reverse search for punctuation from maxPostChars position (and pray)
                    $post_endpos1r = strrpos(substr($post_text, 0, $maxPostChars), '.');
                    $post_endpos2r = strrpos(substr($post_text, 0, $maxPostChars), ',');     

                    $post_text = substr($post_text, 0, max(array($post_endpos1r, $post_endpos2r))+1) . ' [...]';
                }
            }            

            $topic_url = $GLOBALS['DX_SITE_URL'] . $GLOBALS['DX_PHPBB3_URL'] . 'viewtopic.php?'
                       . 'f=' . $post['forum_id'] . '&amp;t=' . $post['topic_id'] . '&amp;p=' . $post['topic_last_post_id'] . '#p' . $post['topic_last_post_id'];

            $HTML .= <<<EOP
                <div class="dxforumteaser">
                <a href="{$topic_url}"><h3>{$topic_title}</h3></a>
                <p>{$post_text}</p>
                <div class="small">by {$post['topic_last_poster_name']}, {$post_time}</div>
                </div>
                
EOP;
        }

        return $HTML;
    }

?>