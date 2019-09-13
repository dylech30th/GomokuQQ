namespace senrenbanka.murasame.qqbot.Resources.CoolQ
{
    public class CqCode
    {
        public static string Image(string filename)
        {
            return $"[CQ:image,file={filename}]";
        }

        public static string At(string qq)
        {
            return $"[CQ:at,qq={qq}]";
        }
    }
}