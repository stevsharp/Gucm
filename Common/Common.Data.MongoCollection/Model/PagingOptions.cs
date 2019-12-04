using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using System;

namespace Common.Data.MongoCollection
{

    public class PagingOptions
    {
        public PagingOptions(int pageSize, int offset)
        {
            this.PageSize = pageSize;
            this.Offset = offset;
        }

        public int PageSize { get; private set; }
        public int Offset { get; private set; }
    }
}
