
namespace Index {

    function nextPage() {

        let page = parseInt($(".active")[0].innerText);

        sendPage(page + 1);
    }

    function backPage() {

        let page = parseInt($(".active")[0].innerText);

        sendPage(page - 1);
    }

    function changePage(event: JQueryEventObject) {

        let page = parseInt(event.target.textContent);

        sendPage(page);
    }

    function sendPage(page: number) {

        window.location.replace(`/Home/Index?SelectedPage=${page}`);
    }

    window.onload = () => {

        $("#sortSelect").change(event => {

            let pageActive = $(".active")[0];
            let page = parseInt(pageActive.innerText);

            sendPage(page);
        });

        $(".pageNumber").click((event) => {
            changePage(event);
        });

        $("#backPage").click((event) => {
            backPage();
        });

        $("#forwardPage").click((event) => {
            nextPage();
        });
    };
}
