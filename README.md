# Securing Your ASP.NET Core REST API Using Key Authentication

If your Web API provides confidential data, the common practice is to authenticate users, when they send HTTP requests to the API. 
That way, the server will respond with a status 401 (Unauthorized), if the user send a wrong username or password.
However, this is not practical when services or applications communicates with each other (e.g., in distributed systems or in a microservice application), since that would mean that the users password would be distributed on the network. 
It is also important to distinguish that the client in this scenario actually is the initiating application that performs the request to the receiving application, and not the user. 

The solution to this is to authenticate the application with a key that is sent from the client application to the service in every request, just like an authentication header is. 

Such a key can be aquired in different ways, but in these examples it is just a hardcoded value, which is not recommended for a live solution. 
The service should keep a record of unique keys provided to named applications, so it is possible to audit requests and to revoke keys as well.
This could be done in a file or a database depending on requirements.

In the examples here the key is used to authenticate the client against the service by including it as a HTTP header in each request, and the difference lies in how the authentication header is processed by the service.

## Attribute


## Middleware
