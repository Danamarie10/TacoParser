﻿using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            // TODO:  Find the two Taco Bells that are the furthest from one another.
            // HINT:  You'll need two nested forloops ---------------------------

            logger.LogInfo("Log initialized");

            // use File.ReadAllLines(path) to grab all the lines from your csv file
            // Log and error if you get 0 lines and a warning if you get 1 line
            var lines = File.ReadAllLines(csvPath);

            logger.LogInfo($"Lines: {lines[0]}");

            // Create a new instance of your TacoParser class
            var parser = new TacoParser();

            // Grab an IEnumerable of locations using the Select command: var locations = lines.Select(parser.Parse);
            var locations = lines.Select(parser.Parse).ToArray();

            // DON'T FORGET TO LOG YOUR STEPS
            logger.LogInfo($"{locations[0]}");

            // Now that your Parse method is completed, START BELOW ----------

            // TODO: Create two `ITrackable` variables with initial values of `null`. These will be used to store your two taco bells that are the farthest from each other.
            // Create a `double` variable to store the distance

            ITrackable tacoPlace = null;
            ITrackable tacoPlace2 = null;
            double distance = 0;

            // Include the Geolocation toolbox, so you can compare locations: `using GeoCoordinatePortable;`

            //HINT NESTED LOOPS SECTION---------------------
            // Do a loop for your locations to grab each location as the origin (perhaps: `locA`)
            for ( int i = 0; i < locations.Length; i++ )
            {
                var locA = locations[i];
                GeoCoordinate cordA = new GeoCoordinate(locA.Location.Latitude, locA.Location.Longitude);
                for (int x = 0; x < locations.Length; x++)
                {
                    var locB = locations[x];
                    GeoCoordinate cordB = new GeoCoordinate(locB.Location.Latitude, locB.Location.Longitude);

                    double distanceBetween = cordA.GetDistanceTo(cordB);

                    if (distanceBetween > distance)
                    {
                        distance = distanceBetween;
                        tacoPlace = locA;
                        tacoPlace2 = locB;
                    }
                }
            }




            // Create a new corA Coordinate with your locA's lat and long

            // Now, do another loop on the locations with the scope of your first loop, so you can grab the "destination" location (perhaps: `locB`)

            // Create a new Coordinate with your locB's lat and long

            // Now, compare the two using `.GetDistanceTo()`, which returns a double
            // If the distance is greater than the currently saved distance, update the distance and the two `ITrackable` variables you set above

            // Once you've looped through everything, you've found the two Taco Bells farthest away from each other.

            logger.LogInfo($"{tacoPlace.Name} and {tacoPlace2.Name} have the greatest distance that amounts to {distance} meters, which is roughly {Math.Round(distance * 0.00062)} miles in between.");

            
        }
    }
}
