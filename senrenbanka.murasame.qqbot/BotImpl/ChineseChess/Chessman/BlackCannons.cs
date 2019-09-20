using System.Collections.Generic;
using System.Drawing;

namespace senrenbanka.murasame.qqbot.BotImpl.ChineseChess.Chessman
{
    public class BlackCannons : IChineseChessman
    {
        public string Name { get; set; } = "炮";

        public ChessmanType ChessmanType { get; set; } = ChessmanType.Black;
    }
}