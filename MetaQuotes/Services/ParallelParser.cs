using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetaQuotes.Services
{
    public class ParallelParser<T>
    {
        private readonly int threadCount;
        private readonly int recordSize;
        private readonly Func<byte[], int, T> parseFunction;

        public ParallelParser(int threadCount, int recordSize, Func<byte[], int, T> parseFunction)
        {
            this.threadCount = threadCount;
            this.recordSize = recordSize;
            this.parseFunction = parseFunction;
        }

        public List<T> parse(byte[] buf, int offset, int recordsCount)
        {
            List<T> records = new List<T>(recordsCount);

            int count = recordsCount / threadCount;
            int pos = offset;

            var tasks = new List<Task<List<T>>>();
            for (int i = 0; i < threadCount - 1; i++)
            {
                tasks.Add(createTask(buf, pos, count));
                pos += recordSize * count;
            }

            tasks.Add(createTask(buf, pos, count + recordsCount % threadCount));

            Task.WaitAll(tasks.ToArray());

            foreach (var task in tasks)
            {
                records.AddRange(task.Result);
            }

            return records;
        }

        private Task<List<T>> createTask(byte[] buf, int pos, int count)
        {
            return Task.Factory.StartNew(() =>
            {
                return parseBatch(buf, pos, count);
            });
        }

        private List<T> parseBatch(byte[] buf, int pos, int count)
        {
            List<T> records = new List<T>(count);
            for (uint i = 0; i < count; i++)
            {
                records.Add(parseFunction(buf, pos));
                pos += this.recordSize;
            }
            return records;
        }
    }
}
