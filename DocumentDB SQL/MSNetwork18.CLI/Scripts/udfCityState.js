function udfCityState(document) {
    var city = document.city;
    var state = document.state;

    if (!city && !state) {
        return '';
    }

    if (city && !state) {
        return city;
    }

    var result = city + ', ' + state;

    return result.trim();
}
