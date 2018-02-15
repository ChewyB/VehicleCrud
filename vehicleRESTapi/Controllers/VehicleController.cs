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
        /// <summary>
        /// Get All Vehicles
        /// </summary>
        /// <returns></returns>
        public ArrayList GetAllVehicles()
        {
            return new VehiclePersistance().GetAllVehicles();
        }

        /// <summary>
        /// Get all Vehicles by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Vehicle GetVehicleById(int id)
        {
            VehiclePersistance pp = new VehiclePersistance();
            Vehicle p = pp.GetVehicle(id);
            return p;
        }


        /// <summary>
        /// Save the vehicle to the database
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public HttpResponseMessage Post_Save_Vehicle([FromBody]Vehicle v)
        {
            VehiclePersistance pp = new VehiclePersistance();
            HttpResponseMessage response;
            int id;
            id = pp.SaveVehicle(v);

            if(id == -1)
            {
                response = Request.CreateResponse(HttpStatusCode.NotAcceptable);
                return response;
            }

            v.Id = id;
            response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Request.RequestUri, String.Format("/{0}", id));
            return response;
        }


        /// <summary>
        /// Update a vehicle by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        [HttpPatch]
        [HttpPut]
        public HttpResponseMessage PUT(int id, [FromBody]Vehicle p)
        {
            

            VehiclePersistance pp = new VehiclePersistance();
            p.Id = id;
            bool recordExisted = false;
            recordExisted = pp.UpdateVehicle(id, p);

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

        /// <summary>
        /// Delete a vehicle by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage DeleteVehicle(int id)
        {
            VehiclePersistance pp = new VehiclePersistance();
            bool recordExisted = false;
            recordExisted = pp.DeleteVehicle(id);

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


        public HttpResponseMessage Options()
        {

            //using (var client = new HttpClient())
            //{
            //    // Create object of Contact and set properties
            //    Vehicle contact = new Vehicle();
            //    client.BaseAddress = new Uri("https://localhost:44391/");
            //    var r = client.PutAsJsonAsync("api/vehicles/3", contact).Result;
            //    if (r.IsSuccessStatusCode)
            //    {
            //        string responseString = r.Content.ReadAsStringAsync().Result;
            //    }
            //}

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK
            };
            return response;
        }
    }
}
