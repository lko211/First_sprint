namespace first_sprint.DTO
{
    public class EventResponseDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = "";
        public string? Description { get; set; } = "";
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
    }

    public class CreateEventDTO
    {
        public string Title { get; set; } = "";
        public string? Description { get; set; } = "";
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
    }
    public class UpdateEventDTO
    {
        public string Title { get; set; } = "";
        public string? Description { get; set; } = "";
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
    }
}
