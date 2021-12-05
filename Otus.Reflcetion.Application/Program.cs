using System;
using System.Diagnostics;
using System.Text.Json;
using Otus.Reflection.CsvSerializer;

namespace Otus.Reflcetion.Application
{
    class Program
    {
        private const string Separator = ";";

        static void Main()
        {
            var f = F.Get();

            var resultSerializeMilliseconds = SerializeData(f, out var resultSerialize);

            WriteResult(resultSerialize, resultSerializeMilliseconds);

            var resultToJsonMilliseconds = SerializeDataToJson(f, out var resultToJson);

            WriteResult(resultToJson, resultToJsonMilliseconds);

            var resultDeserializeMilliseconds = DeserializeData(resultSerialize, out var resultDeserialize);

            SerializeData(resultDeserialize, out var result);

            WriteResult(result, resultDeserializeMilliseconds);
        }

        private static long SerializeData(object data, out string result)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            result = string.Empty;
            for (var i = 0; i < 9999; i++)
            {
                result = CsvSerializer.Serialize(data, Separator);
            }

            return stopwatch.ElapsedMilliseconds;
        }

        private static long DeserializeData(string csv, out object resultDeserialize)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            resultDeserialize = string.Empty;
            for (var i = 0; i < 9999; i++)
            {
                resultDeserialize = CsvSerializer.Deserialize<F>(csv, Separator);
            }

            return stopwatch.ElapsedMilliseconds;
        }

        private static void WriteResult(string result, long resultMilliseconds)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine(result);
            Console.WriteLine(resultMilliseconds);

            stopwatch.Stop();

            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }

        private static long SerializeDataToJson(object data, out string result)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            result = JsonSerializer.Serialize(data);

            return stopwatch.ElapsedMilliseconds;
        }
    }
}
