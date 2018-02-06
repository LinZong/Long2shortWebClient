using MongoDB.Driver;
using Long2shortWebClient.Models;
namespace Long2shortWebClient
{
    class Insert
    {
        public bool NewShortId(IMongoCollection<Long2shortViewModel> CollectionHandler, Long2shortViewModel model)
        {
            bool oristat = false;
            if (model.uid == null)  { oristat = true; }
            model.uid = model.uid ?? CountList(CollectionHandler);
            var filter = Builders<Long2shortViewModel>.Filter.Eq("uid", model.uid);
            var ExistChecker = CollectionHandler.Find(filter);
            Base62Class b = new Base62Class();
            if (ExistChecker.Count() != 0)
            {
                if (oristat)
                {
                    model.uid++;
                    try
                    {
                        model.shortid = b.ReturnShortUrl((long)model.uid);
                        CollectionHandler.InsertOne(model);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            else
            {
                try
                {
                    model.shortid = b.ReturnShortUrl((long)model.uid);
                    CollectionHandler.InsertOne(model);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }
        public long CountList(IMongoCollection<Long2shortViewModel> CollectionHandler)
        {
            return CollectionHandler.Count(FilterDefinition<Long2shortViewModel>.Empty);
        }
    }
}
