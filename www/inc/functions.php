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

    function getIRCusers($server_host, $server_port, $server_chan) {

        //First lets set the timeout limit to 0 so the page wont time out.
        set_time_limit(0);
        //Second lets grab our data from our form.
        $nickname = 'dxdevbot';

        //Ok, We have a nickname, now lets connect.
        $server = array(); //we will use an array to store all the server data.
        //Open the socket connection to the IRC server
        $server['SOCKET'] = fsockopen($server_host, $server_port, $errno, $errstr, 2);

        if ($server['SOCKET'])
        {

            //Ok, we have connected to the server, now we have to send the login commands.
              sendIRCCommand("PASS NOPASS\n\r"); //Sends the password not needed for most servers
              sendIRCCommand("NICK $nickname\n\r"); //sends the nickname
              sendIRCCommand("USER $nickname USING PHP IRC\n\r"); //sends the user must have 4 paramters
            while(!feof($server['SOCKET'])) //while we are connected to the server
            {
                $server['READ_BUFFER'] = fgets($server['SOCKET'], 1024); //get a line of data from the server
                echo "[RECEIVE] ".$server['READ_BUFFER']."<br>\n\r"; //display the recived data from the server

                /*
                IRC Sends a "PING" command to the client which must be anwsered with a "PONG"
                Or the client gets Disconnected
                */
                //Now lets check to see if we have joined the server
                if(strpos($server['READ_BUFFER'], "422")) //422 is the message number of the MOTD for the server (The last thing displayed after a successful connection)
                {
                    //If we have joined the server

                    sendIRCCommand("JOIN $server_chan\n\r"); //Join the chanel
                }
                if(substr($server['READ_BUFFER'], 0, 6) == "PING :") //If the server has sent the ping command
                {
                    sendIRCCommand("PONG :".substr($server['READ_BUFFER'], 6)."\n\r"); //Reply with pong
                    //As you can see i dont have it reply with just "PONG"
                    //It sends PONG and the data recived after the "PING" text on that recived line
                    //Reason being is some irc servers have a "No Spoof" feature that sends a key after the PING
                    //Command that must be replied with PONG and the same key sent.
                }
                flush(); //This flushes the output buffer forcing the text in the while loop to be displayed "On demand"
            }
        }
    }


    function sendIRCCommand ($cmd)
    {
        global $server; //Extends our $server array to this function
        @fwrite($server['SOCKET'], $cmd, strlen($cmd)); //sends the command to the server
        echo "[SEND] $cmd <br>"; //displays it on the screen
    }


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