
using System;
using System.Web;
using System.Web.Services;
using System.Collections.Generic;

using Orbit;
using TrainPlan;
using BasicStop = TrainPlan.BusinessLayer.BasicStop;
using GPSPoint = TrainPlan.GPSPoint;

using PerfectSeat.Entities;

namespace PerfectSeat
{
	public class PerfectSeat : System.Web.Services.WebService
	{
		/// <summary>
		/// Computes the perfect seat
		/// </summary>
		/// <param name="date">
		/// A <see cref="String"/>
		/// </param>
		/// <param name="time">
		/// A <see cref="String"/>
		/// </param>
		/// <param name="externalID">
		/// A <see cref="System.Int32"/>
		/// </param>
		/// <param name="trainID">
		/// A <see cref="System.Int32"/>
		/// </param>
		/// <param name="puic">
		/// A <see cref="System.Int32"/>
		/// </param>
		/// <returns>
		/// A <see cref="Seat"/>
		/// </returns>
		public Seat Compute (String date, String time, int externalID, int trainID)
		{
			List<Sunside> theSunsides = new List<Sunside> ();
			// get journey
			TrainPlan.TrainPlan theTrain = new TrainPlan.TrainPlan ();
			TrainPlan.BusinessLayer.Journey theJourney = theTrain.journey (date, time, externalID, trainID, 80);
			
			// get all stops
			List<BasicStop> basicStops = theJourney.passList;
			
			// iterate through stops
			for (int i = 0; i < basicStops.Count - 1; i++)
			{
				Sunside tempSunside = this.checkSunPosition (basicStops[i], basicStops[i + 1]);
				theSunsides.Add(tempSunside);
			}
			
			return new Seat();
		}
		
		/// <summary>
		/// Checks on an journey interval on which side the sun breaks in
		/// </summary>
		/// <param name="stopA">
		/// A <see cref="BasicStop"/>
		/// </param>
		/// <param name="stopB">
		/// A <see cref="BasicStop"/>
		/// </param>
		/// <returns>
		/// A <see cref="Sunside"/>
		/// </returns>
		private Sunside checkSunPosition (BasicStop stopA, BasicStop stopB)
		{
			// get stations
			GPSPoint stationA = stopA.station.position;
			GPSPoint stationB = stopB.station.position;
			
			// pitch
			double m = (stationB.lon - stationA.lon) / (stationB.lat - stationA.lat);
			
			// directions
			double rEW = (stationA.lat - stationB.lat);
			double rNS = (stationA.lon - stationB.lon);
			
			return new Sunside();
		}
	}
}

