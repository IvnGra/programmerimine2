using KooliProjekt.Data;
using KooliProjekt.Search;
using static NuGet.Packaging.PackagingConstants;

namespace KooliProjekt.Models
{
    public class PredictionsIndexModel
    {
        public PredictionsSearch Search { get; set; }
        public PagedResult<Prediction> Data { get; set; }
    }
}
