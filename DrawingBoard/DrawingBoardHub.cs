using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace DrawingBoard
{
    public class DrawingBoardHub : Hub
    {
        private const int BoardWidth = 300;
        private const int BoardHeight = 300;
        private static int[,] _pointBuffer = GetEmptyPointBuffer();

        public Task DrawPoint(int x, int y)
        {
            if (x < 0)
            {
                x = 0;
            }
            if (x >= BoardWidth)
            {
                x = BoardWidth - 1;
            }
            if (y < 0)
            {
                y = 0;
            }
            if (y >= BoardHeight)
            {
                y = BoardHeight - 1;
            }
            int color = 0;
            int.TryParse(Clients.Caller.color, out color);
            _pointBuffer[x, y] = color;
            return Clients.Others.drawPoint(x, y, Clients.Caller.color);
        }


        public Task Clear()
        {
            _pointBuffer = GetEmptyPointBuffer();
            return Clients.Others.clear();
        }

        public override Task OnConnected()
        {
            return Clients.Caller.update(_pointBuffer);
        }

        private static int[,] GetEmptyPointBuffer()
        {
            var buffer = new int[BoardWidth, BoardWidth];
            return buffer;
        }
    }
}