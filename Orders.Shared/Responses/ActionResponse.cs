namespace Orders.Shared.Responses
{
    public class ActionResponse<T>
    {
        public bool WasSuccess { get; set; }

        public string? Messages { get; set; }

        public T? Result { get; set; }
    }
}
