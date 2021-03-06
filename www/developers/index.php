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
    require_once($GLOBALS['DX_SITE_PATH'] . 'inc/header.php');

?>

    <table>
        <tr>
            <td id="dxmaincolumn">
                <div style="width: 100%;">
                    <p>
                    Would you like to join the <a href="<?php echo $GLOBALS['DX_SITE_URL'];?>wiki/DeuterosX:About">Deuteros X team</a>? Want to know what they are up to right now? This is the page to be looking at!
                    </p>

                </div>

                <div style="width: 100%;">
                    <h2>IRC</h2>

                    <p>The developers gather in bi-weekly IRC meetings on the QuakeNet IRC servers in the #deuterosx.org channel.</p>
                    <ul>
                    <li><a href="chat/">IRC chat applet</a></li>
                    <li><a href="deuterosx.org.log">IRC chat log</a></li>
                    </ul>
                </div>

                <div style="width: 48%;">
                    <h2>Tickets</h2>

                    <ul><li><a href="http://sourceforge.net/apps/trac/deuterosx/">Browse Trac tickets</a></li></ul>

                    <ul>

<?php
    $tracfeed = new SimplePie();
    $tracfeed->set_feed_url('http://sourceforge.net/apps/trac/deuterosx/report/1?format=rss&USER=anonymous');
    $tracfeed->init();
    $tracfeed->handle_content_type();

    if ($tracfeed->data) {
        $tracitems = $tracfeed->get_items(0, 5);
        foreach ($tracitems as $item) {
            echo '<li>';
            echo '<a href="' . $item->get_permalink() . '">' . $item->get_title() . '</a>';
            echo '</li>';
        }
    }
?>
                    </ul>

                    <div class="dxfeedtext"><a href="http://sourceforge.net/apps/trac/deuterosx/report/1?format=rss&amp;USER=anonymous">RSS feed</a></div>
                </div>

                <div style="width: 100%;">
                    <p>
                    Project development is supported by<br/>
                        <a href="http://sourceforge.net/projects/deuterosx">
                            <img src="http://sflogo.sourceforge.net/sflogo.php?group_id=224652&amp;type=11" width="120" height="30" alt="Get Deuteros X at SourceForge.net. Fast, secure and Free Open Source software downloads" />
                        </a>
                    </p>

                </div>

            </td>

            <td id="dxrightcolumn">

                <div class="dxrightbox">

                    <h2>Wiki</h2>

                    <ul><li><a href="<?php echo $GLOBALS['DX_SITE_URL'];?>wiki/Special:RecentChanges">Browse changes</a></li></ul>

                    <ul>

<?php
    $wikifeed = new SimplePie();
    $wikifeed->set_feed_url('http://deuterosx.org/mediawiki/index.php?title=Special:RecentChanges&feed=rss');
    $wikifeed->init();
    $wikifeed->handle_content_type();

    if ($wikifeed->data) {
        $wikiitems = $wikifeed->get_items(0, 5);
        foreach ($wikiitems as $item) {
            $author = $item->get_author();

            echo '<li>';
            echo $item->get_date('j M Y');
            echo ' ';
            echo '<strong>' . $author->Get_name() . '</strong>';
            echo ' ';
            echo '<a href="' . $item->get_permalink() . '">' . $item->get_title() . '</a>';
            echo '</li>';
        }
    }
?>
                    </ul>

                    <div class="dxfeedtext"><a href="http://deuterosx.org/mediawiki/index.php?title=Special:RecentChanges&amp;feed=rss">RSS feed</a></div>

                </div>


                <div class="dxrightbox">
                    <h2>Subversion</h2>

                    <ul>
                        <li><a href="http://deuterosx.svn.sourceforge.net/viewvc/deuterosx/">Browse source</a></li>
                        <li><a href="http://sourceforge.net/mail/?group_id=224652">SVN commits mailing list</a></li>
                    </ul>

                    <ul>
<?php

    $svnfeed = new SimplePie();
    $svnfeed->set_feed_url('http://cia.vc/stats/project/DeuterosX/.rss');
    $svnfeed->init();
    $svnfeed->handle_content_type();

    if ($svnfeed->data) {
        $svnitems = $svnfeed->get_items(0, 5);
        foreach ($svnitems as $item) {
            $descArray = explode(' ', $item->get_description());
            $commiterName = $descArray[16];

            echo '<li>';
            echo $item->get_date('j M Y');
            echo ' ';
            echo $commiterName;
            echo ' ';
            echo '<a href="' . $item->get_permalink() . '">' . $item->get_title() . '</a>';
            echo '</li>';
        }
    }
?>
                    </ul>

                    <div class="dxfeedtext"><a href="http://cia.vc/stats/project/DeuterosX/.rss">RSS feed</a></div>
                </div>

            </td>
        </tr>
    </table>

<?php
    require_once($GLOBALS['DX_SITE_PATH'] . 'inc/footer.php');
?>