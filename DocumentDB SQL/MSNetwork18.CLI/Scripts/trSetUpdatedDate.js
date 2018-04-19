function setUpdatedDate() {
    var document = getContext().getRequest().getBody();

    document.updatedDateTime = new Date();
    getContext().getRequest().setBody(document);
}