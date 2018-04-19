function spGetDistinctSenders() {
    var context = getContext();
    var coll = context.getCollection();
    var collLink = coll.getSelfLink();
    var response = context.getResponse();

    var query = {
        query: "SELECT email.id, email.sender FROM email order by email.sender"
    };

    var  isAccepted = coll.queryDocuments(collLink,
        query,
        function  (err, senders, options) {
            if  (err)  throw  err;

            var body = {
                length: 0,
                data: []
            };

            if  (!senders || !senders.length) {
                response.setBody(body);
            }
            else {
                for (var i = 0; i < senders.length; i++) {
                    var item = senders[i];

                    var j = i + 1;

                    for (; j < senders.length && senders[j].sender === item.sender; j++);

                    senders.splice(i + 1, j - 1);
                }

                body.length = senders.length;
                body.data = senders;

                response.setBody(body);
            }
        });

    if  (!isAccepted)  throw  new  Error('The query was not accepted by the server.');
}