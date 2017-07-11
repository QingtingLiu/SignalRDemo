<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DrawingBoard.aspx.cs" Inherits="DrawingBoard.DrawingBoard" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>在线画板</title>
    <style>
        div {
            margin: 3px;
        }

        canvas {
            border: 2px solid #808080;
            cursor: default;
        }
    </style>
</head>
<body>
    <div>
        <div>
            <label for="color">Color:</label>
            <select id="color"></select>
        </div>
        <canvas id="canvas" width="300" height="300"></canvas>
        <div>
            <button id="clear">Clear Canvas</button>
        </div>
    </div>

    <script src="Scripts/jquery-1.6.4.min.js"></script>
    <script src="Scripts/jquery.signalR-2.2.2.min.js"></script>
    <script src="/signalr/js"></script>
    <script src="Scripts/drawingboard.js"></script>
</body>
</html>
