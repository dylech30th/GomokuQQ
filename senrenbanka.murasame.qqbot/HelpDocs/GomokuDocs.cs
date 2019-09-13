using System.Text;

namespace senrenbanka.murasame.qqbot.HelpDocs
{
    public class GomokuDocs : IDocsBase
    {
        public GomokuDocs() { }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Gomoku/五子棋指令:");
            sb.AppendLine("    /gj 加入五子棋");
            sb.AppendLine("    /ge 退出五子棋");
            sb.AppendLine("    /gs 结束五子棋");
            sb.AppendLine("    x坐标y坐标(先后顺序不固定,0a和a0效果等同) 落子");
            sb.AppendLine("    /gf 投降");
            sb.AppendLine("    /gc 查看自己的gomoku credit");
            return sb.ToString();
        }
    }
}