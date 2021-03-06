﻿$(function () {
    // 画布定义开始 
    var colors = ["black", "red", "green", "blue", "yellow", "magenta", "white"];
    var canvas = $("#canvas");
    var colorElement = $("#color");
    for (var i = 0; i < colors.length; i++) {
        colorElement.append("<option value='" + (i + 1) + "'>" + colors[i] + "</option>");
    }
    var buttonPressed = false;
    canvas
        .mousedown(function () {
            buttonPressed = true;
        })
        .mouseup(function () {
            buttonPressed = false;
        })
        .mousemove(function (e) {
            if (buttonPressed) {
                setPoint(e.offsetX, e.offsetY, colorElement.val());
            }
        });

    var ctx = canvas[0].getContext("2d");

    function setPoint(x, y, color) {
        ctx.fillStyle = colors[color - 1];
        ctx.beginPath();
        ctx.arc(x, y, 2, 0, Math.PI * 2);
        ctx.fill();
    }

    function clearPoints() {
        ctx.clearRect(0, 0, canvas.width(), canvas.height());
    }

    $("#clear").click(function () {
        clearPoints();
    });
    // 画布定义结束

    // SignalR 客户端代码 
    var hub = $.connection.drawingBoardHub;
    hub.state.color = colorElement.val();
    var connected = false;

    colorElement.change(function () {
        hub.state.color = $(this).val();
    });

    canvas.mousemove(function (e) {
        if (buttonPressed && connected) {
            hub.server.drawPoint(Math.round(e.offsetX), Math.round(e.offsetY));
        }
    });

    $("#clear").click(function () {
        if (connected) {
            hub.server.clear();
        }
    });

    // 定义客户端方法 
    hub.client.clear = function () {
        clearPoints();
    };

    hub.client.drawPoint = function (x, y, color) {
        setPoint(x, y, color);
    };

    hub.client.update = function (points) {
        if (!points) return;
        var width = canvas.width();
        var height = canvas.height();
        for (var x = 0; x < width; x++) {
            for (var y = 0; y < height; y++) {
                var color = points[x][y];
                if (color > 0) {
                    setPoint(x, y, color);
                }
            }
        }
    };

    $.connection.hub.start().done(function () {
        connected = true;
    });
})
