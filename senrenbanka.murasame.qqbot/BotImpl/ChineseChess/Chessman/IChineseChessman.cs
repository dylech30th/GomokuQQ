using System.Collections.Generic;
using System.Drawing;

namespace senrenbanka.murasame.qqbot.BotImpl.ChineseChess.Chessman
{
    public interface IChineseChessman
    {
        string Name { get; set; }

        ChessmanType ChessmanType { get; set; }
    }
}