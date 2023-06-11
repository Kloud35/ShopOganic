document.addEventListener("DOMContentLoaded", function () {
    var currentController = "@(ViewContext.RouteData.Values["controller"].ToString())";
    var currentAction = "@(ViewContext.RouteData.Values["action"].ToString())";

    // Find the active menu item and open its parent dropdown
    var menuItems = document.querySelectorAll(".side-nav-menu .dropdown-menu li");
    menuItems.forEach(function (item) {
        var itemLink = item.querySelector("a");
        var itemController = itemLink.getAttribute("asp-controller");
        var itemAction = itemLink.getAttribute("asp-action");

        if (itemController === currentController && itemAction === currentAction) {
            item.classList.add("active");
            var parentDropdown = item.closest(".nav-item.dropdown");
            if (parentDropdown) {
                parentDropdown.classList.add("open");
            }
        }
    });
});
