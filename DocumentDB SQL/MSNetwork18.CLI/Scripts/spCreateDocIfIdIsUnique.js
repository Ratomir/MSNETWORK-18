function spCreateDocIfIdIsUnique(doc) {
	var context = getContext();
	var coll = context.getCollection();
	var collLink = coll.getSelfLink();
	var response = context.getResponse();
	CheckIdAndCreateDoc();

	function createDoc() {
		coll.createDocument(collLink, doc, {}, function (err, doc) {
			if (err) throw new Error('Error creating document: ' + err.message);
			var body = {
				status: "Done",
				document: doc
			};
			response.setBody(body);
		});
	}

	function CheckIdAndCreateDoc() {
		var query = {
			query: 'SELECT VALUE coll.id FROM coll WHERE coll.name = @name',
			parameters: [{
				name: '@name',
				value: doc.name
			}]
		};
		var ok = coll.queryDocuments(collLink, query, {}, function (err, results) {
			if (err) {
				throw new Error('Error querying for document' + err.message);
			}
			if (results.length === 0) {
				createDoc();
			} else {
                var body = {
                    exception: {
                        message: 'Document ' + doc.name + ' already exists.',
                        line: 32
                    }
                };
				response.setBody(body);
			}
		});
		if (!ok) {
			throw new Error('Timeout for querying document...');
		}
    }
}