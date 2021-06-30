var Index;
(function (Index) {
    function nextPage() {
        var page = parseInt($(".active")[0].innerText);
        sendPage(page + 1);
    }
    function backPage() {
        var page = parseInt($(".active")[0].innerText);
        sendPage(page - 1);
    }
    function changePage(event) {
        var page = parseInt(event.target.textContent);
        sendPage(page);
    }
    function sendPage(page) {
        window.location.replace("/Home/Index?SelectedPage=" + page);
    }
    window.onload = function () {
        $("#sortSelect").change(function (event) {
            var pageActive = $(".active")[0];
            var page = parseInt(pageActive.innerText);
            sendPage(page);
        });
        $(".pageNumber").click(function (event) {
            changePage(event);
        });
        $("#backPage").click(function (event) {
            backPage();
        });
        $("#forwardPage").click(function (event) {
            nextPage();
        });
    };
})(Index || (Index = {}));
//# sourceMappingURL=Index.js.map