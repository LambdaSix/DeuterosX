<?php

    require_once('../../inc/config.php');

    require_once($GLOBALS['DX_SITE_PATH'] . 'inc/header.php');

    if (isset($_REQUEST['nick'])) {
?>
    <applet
        name="PJirc"
        id="PJirc"
        codebase="<?php echo $GLOBALS['DX_SITE_URL']; ?>inc/pjirc/"
        code="IRCApplet.class"
        archive="irc.jar,pixx.jar">

        <param name="nick" value="<?php echo $_REQUEST['nick']; ?>">
        <param name="alternatenick" value="<?php echo $_REQUEST['nick']; ?>???">
        <param name="name" value="Java user">
        <param name="host" value="irc.quakenet.org">
        <param name="gui" value="pixx">
        <param name="command1" value="join #deuteros.org">
        <param name="pixx:showconnect" value="false">
        <param name="pixx:showchanlist" value="false">
        <param name="pixx:showclose" value="false">
        <param name="pixx:displayentertexthere" value="true">
        <param name="fileparameter" value="deuteros.ini">
    </applet>
<?php
    } else {
?>
    <form>
        <h2>Nickname</h2>
        <input type="text" name="nick">
        <br>
        <input type="submit" value="Log on!">
    </form>

<?php
    }

    require_once($GLOBALS['DX_SITE_PATH'] . 'inc/footer.php');
?>