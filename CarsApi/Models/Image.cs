using System.ComponentModel.DataAnnotations.Schema;

namespace CarsApi.Models
{
    public class Image
    {
        public Guid Id { get; set; }
        [NotMapped]
        public Guid File { get; set; }
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
        public string? FileExtension { get; set; }
        public long FileInBytes { get; set; }
        public string FilePath { get; set; }

    }
}
