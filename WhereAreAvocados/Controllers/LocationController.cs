using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WhereAreAvocados.Data.Models.Data;

namespace WhereAreAvocados.Controllers
{
    public class LocationController : ApiController
    {
        // GET: api/Location/Test
        [HttpGet, Route("api/location/test")]
        public string Test()
        {
            return "Hello, this is a test.";
        }

        // GET: api/Location/Events
        [HttpGet, Route("api/location/events")]
        public List<Event> GetAllEvents()
        {
            return _sampleEvents;
        }

        // GET: api/Location/FutureEvents
        [HttpGet, Route("api/location/futureevents")]
        public List<Event> GetFutureEvents()
        {
            return _sampleFutureEvents();
        }

        // POST: api/Location/
        public HttpResponseMessage Post([FromBody]Event model)
        {
            if (string.IsNullOrEmpty(model?.EventName))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            var maxId = 0;
            if (_sampleEvents.Count > 0)
            {
                maxId = _sampleEvents.Max(x => x.Id);
            }
            model.Id = maxId + 1;
            _sampleEvents.Add(model);
            return Request.CreateResponse(HttpStatusCode.Created, model);
        }

        public HttpResponseMessage Put(int id, [FromBody]Event model)
        {
            if (string.IsNullOrEmpty(model?.EventName))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            var item = _sampleEvents.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                // Update *all* of the item's properties
                item = model;

                return Request.CreateResponse(HttpStatusCode.OK, item);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        public HttpResponseMessage Delete(int id)
        {
            var item = _sampleEvents.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _sampleEvents.Remove(item);
                return Request.CreateResponse(HttpStatusCode.OK, item);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        #region Sample Data Methods

        private static List<Event> _sampleEvents { get; set; } = new List<Event>()
        {
            new Event()
                {
                    Id = 1,
                    EventName = "Event 1",
                    Website = "https://event1.com",
                    Location = "Anytown, USA",
                    Latitude = "lat 1",
                    Longitude = "long 1",
                    StartDate = new DateTime(2019,1,1),
                    EndDate = new DateTime(2019,1,2),
                    Advocates = new List<Advocate>()
                    {
                        new Advocate()
                        {
                            Id = 100,
                            Name = "Jane Doe",
                            TwitterHandle = "janedoe"
                        }
                    }
                },
            new Event()
            {
                Id = 2,
                EventName = "Event 2",
                Website = "https://event2.com",
                Location = "Anytown, Australia",
                Latitude = "lat 2",
                Longitude = "long 2",
                StartDate = new DateTime(2019,2,2),
                EndDate = new DateTime(2019,2,4),
                Advocates = new List<Advocate>()
                {
                    new Advocate()
                    {
                        Id = 200,
                        Name = "John Smith",
                        TwitterHandle = "johnsmith"
                    }
                }
            },
            new Event()
            {
                Id = 3,
                EventName = "Event 3",
                Website = "https://event3.com",
                Location = "Anytown, Canada",
                Latitude = "lat 3",
                Longitude = "long 3",
                StartDate = new DateTime(2019,3,3),
                EndDate = new DateTime(2019,3,7),
                Advocates = new List<Advocate>()
                {
                    new Advocate()
                    {
                        Id = 300,
                        Name = "Alex Brown",
                        TwitterHandle = "alexbrown"
                    },
                    new Advocate()
                    {
                        Id = 100,
                        Name = "Jane Doe",
                        TwitterHandle = "janedoe"
                    }
                }
            }
        };

        private static List<Event> _sampleFutureEvents()
        {
            return new List<Event>()
            {
                new Event()
                {
                    Id = 4,
                    EventName = "Event 4",
                    Website = "https://event4.com",
                    Location = "Anytown, Belgium",
                    Latitude = "lat 4",
                    Longitude = "long 4",
                    StartDate = new DateTime(2019,4,11),
                    EndDate = new DateTime(2019,4,12),
                    Advocates = new List<Advocate>()
                    {
                        new Advocate()
                        {
                            Id = 300,
                            Name = "Alex Brown",
                            TwitterHandle = "alexbrown"
                        },
                        new Advocate()
                        {
                            Id = 100,
                            Name = "Jane Doe",
                            TwitterHandle = "janedoe"
                        }
                    }
                },
                new Event()
                {
                    Id = 5,
                    EventName = "Event 5",
                    Website = "https://event5.com",
                    Location = "Anytown, Japan",
                    Latitude = "lat 5",
                    Longitude = "long 5",
                    StartDate = new DateTime(2019,5,4),
                    EndDate = new DateTime(2019,5,5),
                    Advocates = new List<Advocate>()
                    {
                        new Advocate()
                        {
                            Id = 200,
                            Name = "John Smith",
                            TwitterHandle = "johnsmith"
                        }
                    }
                }
            };
        }

        #endregion
    }
}
