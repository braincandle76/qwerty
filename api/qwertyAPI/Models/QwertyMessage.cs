namespace QwertyAPI.Models
{
    public class QwertyMessage
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public int QwertyProfileId { get; set; }

        public virtual QwertyProfile QwertyProfile { get; set; }

    }
}