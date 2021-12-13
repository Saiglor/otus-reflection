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

            ExecuteCustomSerializer(f);

            ExecuteJsonSerializer(f);
        }

        private static void ExecuteCustomSerializer(F f)
        {
            var resultSerializeMilliseconds = SerializeData(f, out var resultSerialize);

            WriteResult(resultSerialize, resultSerializeMilliseconds);

            var resultDeserializeMilliseconds = DeserializeData(resultSerialize, out var resultDeserialize);

            WriteResult(resultDeserialize.ToString(), resultDeserializeMilliseconds);
        }

        private static void ExecuteJsonSerializer(F f)
        {
            var resultToJsonMilliseconds = SerializeDataToJson(f, out var resultSerialize);

            WriteResult(resultSerialize, resultToJsonMilliseconds);

            var resultFromJsonMilliseconds = DeserializeDataFromJson(resultSerialize, out var resultDeserialize);

            WriteResult(resultDeserialize.ToString(), resultFromJsonMilliseconds);
        }

        private static long SerializeData(object data, out string result)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            result = string.Empty;
            for (var i = 0; i < 10000; i++)
            {
                result = CsvSerializer.Serialize(data, Separator);
            }

            stopwatch.Stop();

            return stopwatch.ElapsedMilliseconds;
        }

        private static long DeserializeData(string csv, out object resultDeserialize)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            resultDeserialize = string.Empty;
            for (var i = 0; i < 10000; i++)
            {
                resultDeserialize = CsvSerializer.Deserialize<F>(csv, Separator);
            }

            stopwatch.Stop();

            return stopwatch.ElapsedMilliseconds;
        }

        private static void WriteResult(string result, long resultMilliseconds)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine(result);
            Console.WriteLine($"Время на получение результата: {resultMilliseconds}");

            stopwatch.Stop();

            Console.WriteLine($"Время на вывод: {stopwatch.ElapsedMilliseconds}");
        }

        private static long SerializeDataToJson(object data, out string result)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            result = JsonSerializer.Serialize(data);

            stopwatch.Stop();

            return stopwatch.ElapsedMilliseconds;
        }

        private static long DeserializeDataFromJson(string data, out object result)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            result = JsonSerializer.Deserialize<F>(data);

            stopwatch.Stop();

            return stopwatch.ElapsedMilliseconds;
        }
    }
}
