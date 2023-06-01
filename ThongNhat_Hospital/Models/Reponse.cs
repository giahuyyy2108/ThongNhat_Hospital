namespace ThongNhat_Hospital.Models
{
    public class Reponse<T> where T : class
    {
        public int errorCode { get; set; }

        public T Obj { get; set; }
    }
}
