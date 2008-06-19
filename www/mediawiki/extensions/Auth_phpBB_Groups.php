<?php

$wgHooks['UserEffectiveGroups'][] = 'Auth_phpBB_Groups';

/**
 * This function exposes the user's phpBB groups to MediaWiki and hooks into the UserEffectiveGroups
 * function, meaning that the wiki will be able to 'see' the phpBB groups that the user belongs to.
 * It uses the config from the Auth_phpBB extension, but could easily be used with a custom config.
 *
 * Author:  Geddeth aka Achton N. Netherclift <achton AT netherclift DOT net>
 * Date:    May 14th 2008
 */
function Auth_phpBB_Groups($user, &$groups) {
    // import the Auth_phpBB db config and connect to db
    global $wgAuth_Config;
    $database       = $wgAuth_Config['MySQL_Database'];
    $userTable      = $wgAuth_Config['UserTB'];
    $userGroupTable = $wgAuth_Config['User_GroupTB'];
    $groupsTable    = $wgAuth_Config['GroupsTB'];
    $conn = @mysql_connect($wgAuth_Config['MySQL_Host'],
                           $wgAuth_Config['MySQL_Username'],
                           $wgAuth_Config['MySQL_Password'],
                           true);

    // array that will hold the user's groups
    $groupsArray = array();

    // select database
    $dbSelected = mysql_select_db($database, $conn);
    if (!$dbSelected) {
        die ('Can\'t select db "' . $database . '". MySQL error: ' . mysql_error());
    }

    // get userId from phpBB
    $dbUserQuery = 'SELECT @userId := `user_id` FROM `' . $userTable . '` '
                 . 'WHERE `username_clean` = \'' . strtolower($user->mName) . '\';';
    mysql_query($dbUserQuery, $conn) or die('Unable to get userID.');

    // get the user's groupIds from phpBB
    $dbGroupIdsQuery = 'SELECT @groupIds := `group_id` FROM `' . $userGroupTable . '` '
                     . 'WHERE `user_id` = @userId and `user_pending` = 0;';
    $dbGroupIdsResult = mysql_query($dbGroupIdsQuery, $conn);

    // get the group names for each groupId
    while ($dbGroupsResult = mysql_fetch_array($dbGroupIdsResult)) {
        // get groupNames
        $dbGroupsQuery = 'SELECT @groupNames := `group_name` FROM `' . $groupsTable . '` '
                       . 'WHERE `group_id` = \'' . intval($dbGroupsResult[0]) . '\';';
        $dbGroupsResult = mysql_query($dbGroupsQuery, $conn)
                    or die('Unable to get groupNames.');
        $result = mysql_fetch_row($dbGroupsResult);
        // insert groupName into array
        $groupsArray[] = $result[0];
    }

    // merge with user's existing wiki groups
    $groups = array_merge($groups, $groupsArray);

    // always return true
    return TRUE;
}

?>
