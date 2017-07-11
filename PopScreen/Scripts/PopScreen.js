$(function () {
    var clickScreen = $("#click_screen");
    var screen = $(".screen");
    var userNameBox = $(".s_userName");
    var commentBox = $(".s_txt");
    var popArea = $(".s_show");
    var commentButton = $(".s_btn");
    var connection = null;

    clickScreen.click(function () {
        connection = $.connection("/popScreenConnection");
        connection.start().done(function () {
            screen.toggle(600);
        });
        connection.received(function (data) {
            var popObj = JSON.parse(data);
            // 接收服务器推送过来的弹幕消息 
            addPop(popObj.userName, popObj.comment);
        });
        return false;
    });

    //点击关闭弹屏
    $(".s_close").click(function () {
        if (connection != null) {
            connection.stop(true, true);
            connection = null;
        }
        screen.toggle(600);
        return false;
    });

    //发表评论
    commentButton.click(function () {
        var userName = userNameBox.val();
        var comment = commentBox.val();
        if (userName == "" && comment == "") {
            alert("请输入用户名和评论");
            return false;
        }
        addPop(userName, comment);
        connection.send({ userName: userName, comment: comment });
    });

    //回车发送评论
    commentBox.keydown(function () {
        var code = window.event.keyCode;
        if (code == 13) {
            commentButton.click();
        }
    });

    function addPop(userName, comment) {
        popArea.append("<div>" + userName + ":" + comment + "</div>");
        refreshPops();
    }

    function refreshPops() {
        var _top = 0;
        popArea.find("div").show().each(function () {
            var pop = $(this);
            var _left = $(window).width() - pop.width();
            var _height = $(window).height();
            _top = _top + 80;
            if (_top > _height - 100) {
                _top = 80;
            }

            var time = 100000;
            if (pop.index() % 2 == 0) {
                time = 20000;
            }
            //设定文字初始位置
            pop.css({ left: _left, top: _top, color: getRandomColor() });
            pop.animate({ left: "-" + _left + "px" },
                time,
                function () {
                    pop.remove();
                });
        });

        //随机获取颜色值 
        function getRandomColor() {
            return '#' + (function (h) {
                return new Array(7 - h.length).join("0") + h
            })((Math.random() * 0x1000000 << 0).toString(16))
        }
    }
});