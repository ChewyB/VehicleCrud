using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using vehicleRESTapi.Models;

namespace vehicleRESTapi.Controllers
{

    public class VehiclesController : ApiController
    {

        public ArrayList Get()
        {
            return new VehiclePersistance().getAllVehicles();
        }


        public Vehicle Get(long id)
        {
            VehiclePersistance pp = new VehiclePersistance();
            Vehicle p = pp.getVehicle(id);

            return p;
        }


        public HttpResponseMessage Post([FromBody]Vehicle value)
        {
            VehiclePersistance pp = new VehiclePersistance();
            long id;
            id = pp.saveVehicle(value);
            value.Id = id;
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Request.RequestUri, String.Format("vehicles/{0}", id));
            return response;
        }


        public HttpResponseMessage Put([FromUri]long id, [FromBody]Vehicle p)
        {
            VehiclePersistance pp = new VehiclePersistance();
            bool recordExisted = false;
            recordExisted = pp.updateVehicle(id, p);

            HttpResponseMessage response;

            if (recordExisted)
            {
                //Send a response code
                response = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }

        public HttpResponseMessage Delete(long id)
        {
            VehiclePersistance pp = new VehiclePersistance();
            bool recordExisted = false;
            recordExisted = pp.deleteVehicle(id);

            HttpResponseMessage response;

            if (recordExisted)
            {
                //Send a response code
                response = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }

        //public HttpResponseMessage Options()
        //{
        //    var response = new HttpResponseMessage
        //    {
        //        StatusCode = HttpStatusCode.OK
        //    };
        //    return response;
        //}
    }
}
