function showMenu(show) {
    if (show) {
        $("#submenu").removeClass("hiddenSubmenu");
        $("#submenu").addClass("submenu");
    } else {
        $("#submenu").removeClass("submenu");
        $("#submenu").addClass("hiddenSubmenu");
    }
}