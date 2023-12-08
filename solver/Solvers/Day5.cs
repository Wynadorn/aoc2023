using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2023.Solvers
{
    internal class Day5 : ISolver
    {
        public string SolvePartOne(string puzzleInput, string[] puzzleInputArray = null)
        {
            var split = puzzleInput.Split(new string[] { "\n\n" }, StringSplitOptions.None);

            string seedsString = split[0].Split(':', StringSplitOptions.TrimEntries).Last();
            long[] seedNumbers = Array.ConvertAll(seedsString.Split(' '), long.Parse);

            Dictionary<long, long> seedLocations = new Dictionary<long, long>();
            foreach(long seed in seedNumbers)
            {
                long soil, fertalizer, water, light, temperature, humidity, location;

                soil = ConvertNumber(seed, split[1]);
                fertalizer = ConvertNumber(soil, split[2]);
                water = ConvertNumber(fertalizer, split[3]);
                light = ConvertNumber(water, split[4]);
                temperature = ConvertNumber(light, split[5]);
                humidity = ConvertNumber(temperature, split[6]);
                location = ConvertNumber(humidity, split[7]);

                seedLocations.Add(seed, location);
            }

            long bestSeed = -1;
            long bestLocation = long.MaxValue;
            
            foreach(var entry in seedLocations)
            {
                if(entry.Value < bestLocation)
                {
                    bestSeed = entry.Key;
                    bestLocation = entry.Value;
                }
            }

            return $"The best seed is {bestSeed} at location {bestLocation}";
        }

        private long ConvertNumber(long number, string inputRaw)
        {
            string[] splitInput = inputRaw.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            for (int i = 0; i < splitInput.Length - 1; i++)
            {
                string line = splitInput[i + 1];
                var numberStrings = line.Split(' ');
                long[] numbers = Array.ConvertAll(numberStrings, long.Parse);

                long source = numbers[1];
                long destination = numbers[0];
                long length = numbers[2];

                if(number >= source && number <= source + length)
                    return destination + (number - source);
            }

            return number;
        }

        private long[][] foo(string x)
        {
            List<long[]> y = new List<long[]>();

            string[] splitInput = x.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            for (int i = 0; i < splitInput.Length - 1; i++)
            {
                string line = splitInput[i + 1];
                var numberStrings = line.Split(' ');
                long[] numbers = Array.ConvertAll(numberStrings, long.Parse);

                long source = numbers[1];
                long destination = numbers[0];
                long length = numbers[2];

                y.Add(new long[] { source, destination, length });
            }

            return y.ToArray();
        }
        private long ConvertNumber2(long number, long[][] inputRaw)
        {
            foreach (long[] x in inputRaw)
            {
                long source = x[0];
                long destination = x[1];
                long length = x[2];

                if (number >= source && number <= source + length)
                    return destination + (number - source);
            }

            return number;
        }

        private string BadSolution(string puzzleInput, string[] puzzleInputArray = null)
        {
            var split = puzzleInput.Split(new string[] { "\n\n" }, StringSplitOptions.None);

            string seedsString = split[0].Split(':', StringSplitOptions.TrimEntries).Last();
            long[] seedNumbers = Array.ConvertAll(seedsString.Split(' '), long.Parse);

            var seed2soilMap = CreateSectionMap(split[1]);
            var soil2fertalizerMap = CreateSectionMap(split[2]);
            var fertalizer2waterMap = CreateSectionMap(split[3]);
            var water2lightMap = CreateSectionMap(split[4]);
            var light2temperatureMap = CreateSectionMap(split[5]);
            var temperature2humidityMap = CreateSectionMap(split[6]);
            var humidity2locationMap = CreateSectionMap(split[7]);

            Dictionary<long, long> seedLocations = new Dictionary<long, long>();
            foreach (long seed in seedNumbers)
            {
                long soil, fertalizer, water, light, temperature, humidity, location;

                soil = seed2soilMap.ContainsKey(seed) ? seed2soilMap[seed] : seed;
                fertalizer = soil2fertalizerMap.ContainsKey(soil) ? soil2fertalizerMap[soil] : soil;
                water = fertalizer2waterMap.ContainsKey(fertalizer) ? fertalizer2waterMap[fertalizer] : fertalizer;
                light = water2lightMap.ContainsKey(water) ? water2lightMap[water] : water;
                temperature = light2temperatureMap.ContainsKey(light) ? light2temperatureMap[light] : light;
                humidity = temperature2humidityMap.ContainsKey(temperature) ? temperature2humidityMap[temperature] : temperature;
                location = humidity2locationMap.ContainsKey(humidity) ? humidity2locationMap[humidity] : humidity;

                seedLocations.Add(seed, location);
            }

            long bestSeed = -1;
            long bestLocation = long.MaxValue;

            foreach (var entry in seedLocations)
            {
                if (entry.Value < bestLocation)
                {
                    bestSeed = entry.Key;
                    bestLocation = entry.Value;
                }
            }

            return $"The best seed is {bestSeed} at location {bestLocation}";
        }

        private Dictionary<long, long> CreateSectionMap(string inputRaw)
        {
            Dictionary<long, long> map = new Dictionary<long, long>();

            string[] splitInput = inputRaw.Split('\n');
            for (int i = 0; i < splitInput.Length-1; i++)
            {
                string line = splitInput[i+1];
                var numberStrings = line.Split(' ');
                long[] numbers = Array.ConvertAll(numberStrings, long.Parse);
                var lineMap = CreateLineMap(numbers[0], numbers[1], numbers[2]);

                foreach(var entry in lineMap)
                    map.TryAdd(entry.Key, entry.Value);
            }

            return map;
        }

        public string SolvePartTwo(string puzzleInput, string[] puzzleInputArray = null)
        {
            var split = puzzleInput.Split(new string[] { "\n\n" }, StringSplitOptions.None);

            string seedsString = split[0].Split(':', StringSplitOptions.TrimEntries).Last();
            long[] seedNumbers = Array.ConvertAll(seedsString.Split(' '), long.Parse);

            long bestSeed = -1;
            long bestLocation = long.MaxValue;

            ReaderWriterLock rwLock = new ReaderWriterLock();

            var a = foo(split[1]);
            var b = foo(split[2]);
            var c = foo(split[3]);
            var d = foo(split[4]);
            var e = foo(split[5]);
            var f = foo(split[6]);
            var g = foo(split[7]);

            Parallel.For(0, seedNumbers.Length / 2, i =>
            {
                Console.WriteLine($"{DateTime.Now:T}.{DateTime.Now:fff}: Starting with seed entry {i+1} out of {seedNumbers.Length / 2}");

                for (long seed = seedNumbers[i*2]; i < seedNumbers[i*2] + seedNumbers[i*2+1]; seed++)
                {
                    long soil, fertalizer, water, light, temperature, humidity, location;

                    soil = ConvertNumber2(seed, a);
                    fertalizer = ConvertNumber2(soil, b);
                    water = ConvertNumber2(fertalizer, c);
                    light = ConvertNumber2(water, d);
                    temperature = ConvertNumber2(light, e);
                    humidity = ConvertNumber2(temperature, f);
                    location = ConvertNumber2(humidity, g);

                    rwLock.AcquireReaderLock(Timeout.Infinite);
                    long threadBestSeed = bestSeed;
                    long threadBestLocation = bestLocation;
                    rwLock.ReleaseReaderLock();

                    if (location < threadBestLocation)
                    {
                        rwLock.AcquireWriterLock(Timeout.Infinite);

                        if(location < bestLocation)
                        {
                            bestSeed = seed;
                            bestLocation = location;
                            Console.WriteLine($"{DateTime.Now:T}.{DateTime.Now:fff}: New best seed found! Seed: '{bestSeed}' Location: '{bestLocation}'");
                        }
                        rwLock.ReleaseWriterLock();
                    }
                }
                Console.WriteLine($"{DateTime.Now:T}.{DateTime.Now:fff}: Done with seed entry {i/2} out of {seedNumbers.Length/2}");
            });

            return $"The best seed is {bestSeed} at location {bestLocation}";
        }

        public Dictionary<long, long> CreateLineMap(long destination, long source, long length)
        {
            Dictionary<long, long> map = new Dictionary<long, long>();

            for (long i = 0; i < length; i++)
                map.Add(source+i, destination+i);

            return map;
        }
    }
}
