﻿using System;
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
            return new VehiclePersistance().GetAllVehicles();
        }

        public Vehicle Get(int id)
        {
            VehiclePersistance pp = new VehiclePersistance();
            Vehicle p = pp.GetVehicle(id);
            return p;
        }



        public HttpResponseMessage Post([FromBody]Vehicle value)
        {
            VehiclePersistance pp = new VehiclePersistance();
            int id;
            id = pp.SaveVehicle(value);
            value.Id = id;
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Request.RequestUri, String.Format("/{0}", id));
            return response;
        }

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


        public HttpResponseMessage Delete(int id)
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
