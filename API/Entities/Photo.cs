using System.ComponentModel.DataAnnotations.Schema;
using API.Entities.Conventions;

namespace API.Entities
{
    [Table("Photos")]
    //[ColumnPrefix("")]
    public class Photo
    {
        public int Id {get; set;}
        public string Url {get; set;}
        public bool IsMain {get; set;}
        public string PublicId {get; set;}
        public AppUser AppUser {get; set;}
        public int AppUserId {get; set;}
    }
}