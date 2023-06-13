using System.Text.Json.Serialization;

namespace Saas.Models
{
    public class Ticket
    {
        public string? Locale { get; set; }
        public string Description { get; set; }
        public Poly BoundingPoly { get; set; }
    }
}