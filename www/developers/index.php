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

    require_once('../inc/config.php');

    require_once($GLOBALS['DX_SITE_PATH'] . 'inc/simplepie/simplepie.inc');
    require_once($GLOBALS['DX_SITE_PATH'] . 'inc/SmartIRC.php');
    require_once($GLOBALS['DX_SITE_PATH'] . 'inc/header.php');

    // get users in IRC channel
    $irc = &new Net_SmartIRC();
    $irc->setUseSockets(TRUE);
    $irc->connect('no.quakenet.org', 6667);
    $irc->login('Net_SmartIRC', 'Net_SmartIRC Client '.SMARTIRC_VERSION.' (example2.php)', 0, 'Net_SmartIRC');
    $irc->getList('#deuterosx.org');
    $irc_result = $irc->listenFor(SMARTIRC_TYPE_LIST);
    $irc->disconnect();

    if (is_array($irc_result)) {
        $ircdata = $irc_result[0];
        $irc_count = $ircdata->rawmessageex[4];
    }
?>

    <div style="float:right;width:50%;padding:2px;">
        <h1>SVN Commit log</h1>
<?php

    $feed = new SimplePie();
    $feed->set_feed_url('http://cia.vc/stats/project/DeuterosX/.rss');
    $feed->init();
    $feed->handle_content_type();

    if ($feed->data) {
        $items = $feed->get_items(0, 5);
        echo '<p><span style="padding:2px;background-color:#47c;">Displaying ' . $feed->get_item_quantity(5) . ' most recent entries.</span></p>';
        foreach ($items as $item) {
            echo '<div class="chunk" style="padding:0 5px;">'
            . '<h4 style="padding:2px;background-color:#024;"><a href="' . $item->get_permalink() . '">' . $item->get_title() . '</a> ' . $item->get_date('j M Y') . '</h4>';
            echo $item->get_content();
            if ($enclosure = $item->get_enclosure(0))
                echo '<p><a href="' . $enclosure->get_link() . '" class="download"><img src="./for_the_demo/mini_podcast.png" alt="Podcast" title="Download the Podcast" border="0" /></a></p>';

            echo '</div>';
        }

    }
?>
    </div>

    <div style="width: 100%;">
    <p>
    This is the developers' page.
    </p>

    <ul>
    <li><a href="chat/">IRC chat</a> (<?php echo  $irc_count; ?> users)</li>
    <li><a href="http://cia.vc/stats/project/DeuterosX">SVN commits</a></li>
    <li><a href="http://sourceforge.net/mail/?group_id=224652">SVN commits mailing list</a></li>
    </ul>
    </div>

    <br />
<?php
    require_once($GLOBALS['DX_SITE_PATH'] . 'inc/footer.php');
?>