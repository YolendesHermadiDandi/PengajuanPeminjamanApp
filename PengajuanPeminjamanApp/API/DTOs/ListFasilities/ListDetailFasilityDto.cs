namespace API.DTOs.ListFasilities
{
    public class ListDetailFasilityDto
    {
        public Guid Guid { get; set; }
        public Guid FasilityGuid { get; set; }
        public string Name { get; set; }
        public int TotalFasility { get; set; }
    }
}
