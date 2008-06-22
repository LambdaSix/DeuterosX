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

    function getDBConnection($host, $db, $user, $pass) {

        $db_link = mysql_connect($host, $user, $pass);
        if (!$db_link) {
            die('Could not connect: ' . mysql_error());
        }
        // make foo the current db
        $db_selected = mysql_select_db($db, $db_link);
        if (!$db_selected) {
            die ("Can't use " . $db . " : " . mysql_error());
        }

        return $db_link;
    }


    function closeDBConnection($db_link) {
         @mysql_close($db_link);
    }


    function getPhpBB3Posts($BoardID) {

        $sql = 'SELECT p.*, u.username '
             . 'FROM phpbb3_posts p, phpbb3_users u '
             . 'WHERE p.forum_id = ' . $BoardID . ' '
             . 'AND p.post_approved = 1 '
             . 'AND p.post_reported = 0 '
             . 'AND p.post_time < UNIX_TIMESTAMP() '
             . 'AND p.poster_id = u.user_id '
             . 'ORDER BY p.topic_id DESC, p.post_id ASC ';

        $result = mysql_query($sql);
        if (!$result) {
            die('Invalid query: ' . mysql_error());
        }

        while ($row = mysql_fetch_assoc($result)) {
            $postsArray[] = $row;
        }

        return $postsArray;
    }

?>