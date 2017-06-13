using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebService.Models;
using WebService.Models.ViewModels;

namespace WebService.Controllers
{
    public class CityController : ApiController
    {

        private ModelContext db = new ModelContext();


        // GET: api/City/?name=string&state=int
        [Route("api/City/{name?}/{state?}")]
        public List<CityViewModel> GetCities(string name = null, int? state = null)
        {
            var Cities = db.City.Where(
                c => (
                (!name.Equals("") && name != null) ? c.Name.ToUpper().Contains(name.ToUpper()) : true)
                && ((state != null && state != 0) ? c.IdState == state : true)
                );
            if (Cities.Count() == 0)
            {
                return null;
            }

            var CitiesMapped = AutoMapper.Mapper.Map<List<City>, List<CityViewModel>>(Cities.ToList());
            return CitiesMapped;
        }
        

        // GET: api/City/5
        [Route("api/city/{id}/")]
        public CityViewModel GetCity(int id)
        {
            City city = db.City.Find(id);
            if (city == null)
            {
                return null;
            }

            return AutoMapper.Mapper.Map<City, CityViewModel>(city);
        }
        
        


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}