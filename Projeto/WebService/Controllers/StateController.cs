using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using WebService.Models;
using WebService.Models.ViewModels;

namespace WebService.Controllers
{
    public class StateController : ApiController
    {
        private ModelContext db = new ModelContext();

        // GET: api/State
        public List<StateViewItem> GetStates()
        {
            var States = db.State;
            var StatesMapped = AutoMapper.Mapper.Map<List<State>, List<StateViewItem>>(States.ToList());
            return StatesMapped;
        }

        // GET: api/State/5
        [ResponseType(typeof(State))]
        public StateViewItem GetState(int id)
        {
            State state = db.State.Find(id);
            if (state == null)
            {
                return null;
            }
            var stateMapped = AutoMapper.Mapper.Map<State, StateViewItem>(state);
            return stateMapped;
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