namespace Application.Core.Responses {
   public class GetMediaResponse
    {
        public string Provider { get; set; }
        public bool Spoiler { get; set; }
        public string Url { get; set; }
        public string? Previsualizacion { get; set; }
    }
}