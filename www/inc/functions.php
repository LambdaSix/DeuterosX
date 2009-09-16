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

    function getDBConnection($host, $db, $user, $pass) {

        $db_link = mysql_connect($host, $user, $pass);
        if (!$db_link) {
            die("Could not connect: " . mysql_error());
        }
        // make foo the current db
        $db_selected = mysql_select_db($db, $db_link);
        if (!$db_selected) {
            die ("Can't use " . $db . ": " . mysql_error());
        }

        return $db_link;
    }


    function closeDBConnection($db_link) {
         @mysql_close($db_link);
    }


    function getPhpBB3Posts (&$db, $num_topics, $forum_id_ary=FALSE, $forum_id=FALSE) {

        if (empty($db) || $num_topics <= 0) {
            return FALSE;
        }
        
        if ($forum_id && (empty($forum_id_ary) || ($forum_id_ary && array_key_exists($forum_id, $forum_id_ary)))) {
            $sql = 'SELECT t.topic_id, t.topic_title, t.forum_id, t.topic_replies, t.topic_last_post_id, t.topic_time, '
                 . '  p.bbcode_uid, p.bbcode_bitfield, p.post_id, p.post_text, '
                 . '  u.username '
                 . 'FROM ' . TOPICS_TABLE . ' t, ' . POSTS_TABLE . ' p, ' . USERS_TABLE . ' u '
                 . 'WHERE t.topic_id = p.topic_id '
                 . 'AND p.post_time = t.topic_time '
                 . 'AND t.topic_poster = u.user_id '
                 . 'AND t.topic_approved = 1 '
                 . 'AND t.topic_reported = 0 '
                 . 'AND t.topic_time < UNIX_TIMESTAMP() '
                 . 'AND t.forum_id = ' . $forum_id . ' '
                 . 'ORDER BY t.topic_time DESC';
        } elseif ($forum_id_ary) {
            $sql = 'SELECT topic_id, topic_title, forum_id, topic_last_post_id, topic_last_poster_name, topic_last_post_time '
                 . 'FROM ' . TOPICS_TABLE . ' '
                 . 'WHERE topic_type <> 3 '
                 . 'AND topic_approved = 1 '
                 . 'AND ' . $db->sql_in_set('forum_id', $forum_id_ary)
                 . $sql_where . ' '
                 . 'ORDER BY topic_last_post_time DESC';
        } else {
            return FALSE;
        }

        $result = $db->sql_query_limit($sql, $num_topics);
        $row = $db->sql_fetchrowset($result);
        $db->sql_freeresult($result);

        return $row;
    }


?>