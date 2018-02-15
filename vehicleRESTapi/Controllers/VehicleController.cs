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

    /// <summary>
    /// The vehicle controller class used to handle Rest service calls 
    /// </summary>
    public class VehiclesController : ApiController
    {
        /// <summary>
        /// Get All Vehicles
        /// </summary>
        /// <returns></returns>
        public ArrayList GetAllVehicles()
        {
            return new VehicleActions().GetAllVehicles();
        }

        /// <summary>
        /// Get all Vehicles by id
        /// </summary>
        /// <param name="id">Used to get the vehicle by Id from the table</param>
        /// <returns></returns>
        public Vehicle GetVehicleById(int id)
        {
            VehicleActions v_action = new VehicleActions();
            Vehicle vehicleToGet = v_action.GetVehicle(id);
            return vehicleToGet;
        }


        /// <summary>
        /// Save the vehicle to the database
        /// </summary>
        /// <param name="vehicleToSave">The vehicle that is going to be save to the databse</param>
        /// <returns></returns>
        public HttpResponseMessage Post_Save_Vehicle([FromBody]Vehicle vehicleToSave)
        {
            VehicleActions v_action = new VehicleActions();
            HttpResponseMessage response;
            int id = v_action.SaveVehicle(vehicleToSave);

            //Check that values for year, make and model are valid
            if(id == -1)
            {
                response = Request.CreateResponse(HttpStatusCode.NotAcceptable);
                return response;
            }

            vehicleToSave.Id = id;
            response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Request.RequestUri, String.Format("/{0}", id));
            return response;
        }


        /// <summary>
        /// Update a vehicle by id
        /// </summary>
        /// <param name="id">The id is used in the Uri to know which vehicle to update</param>
        /// <param name="vehicleToUpdate">The vehicle that is going to be updated</param>
        /// <returns></returns>
        [HttpPatch]
        [HttpPut]
        public HttpResponseMessage PUT(int id, [FromBody]Vehicle vehicleToUpdate)
        {
            

            VehicleActions pp = new VehicleActions();
            vehicleToUpdate.Id = id;
            bool recordExisted = false;
            recordExisted = pp.UpdateVehicle(id, vehicleToUpdate);

            HttpResponseMessage response;
            if (recordExisted)
            {
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
        /// <param name="id">Used to specify which vehicle to delete from the database</param>
        /// <returns></returns>
        public HttpResponseMessage DeleteVehicle(int id)
        {
            VehicleActions pp = new VehicleActions();
            bool recordExisted = false;
            recordExisted = pp.DeleteVehicle(id);

            HttpResponseMessage response;

            if (recordExisted)
            {
                response = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }


        /// <summary>
        /// Used to handle the Options call 
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage Options()
        {
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK
            };
            return response;
        }
    }
}
