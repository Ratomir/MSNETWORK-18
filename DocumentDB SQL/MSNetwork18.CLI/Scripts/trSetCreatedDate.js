function setCreatedDate() {
    var document = getContext().getRequest().getBody();

    document.createdTime = new Date();
    getContext().getRequest().setBody(document);
}