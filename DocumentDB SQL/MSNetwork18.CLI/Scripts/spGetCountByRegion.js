function spGetCountByRegion(region) {
    var context = getContext();
    var coll = context.getCollection();
    var collLink = coll.getSelfLink();
    var response = context.getResponse();

    var query = {
        query: "select * from store where store.address.countryRegionName = @region",
        parameters: [{
            name: "@region",
            value: region
        }]
    };

    var ok = coll.queryDocuments(collLink, query, {}, function (err, results) {
        if (err) {
            throw new Error('Error querying for document' + err.message);
        }
        response.setBody(results.length);
    });
    if (!ok) {
        throw new Error('Timeout for querying document...');
    }
}