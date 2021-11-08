The API should reply to the resource request /Listings that takes the number of passengers as a
parameter.
The code should call the search endpoint
https://jayridechallengeapi.azurewebsites.net/api/QuoteRequest to get the search data. Utilising the
listings array, filter out listings that don’t support the number of passengers. With the remaining listings,
calculate the total price and return the results sorted by total price
