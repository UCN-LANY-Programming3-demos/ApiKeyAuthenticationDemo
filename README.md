# Securing Your ASP.NET Core REST API Using Key Authentication

If your Web API provides confidential data, the common practice is to authenticate users, when they send HTTP requests to the API. 
That way, the server will respond with a status 401 (Unauthorized), if the user send a wrong username or password.
However, this is not always practical when services or applications communicates with each other (e.g., in distributed systems or in a microservice application).



This project contains two examples that demonstrates how to authenticate using a key. 


In both examples the key is used to authenticate the client against the service by including it as a HTTP header in each request.

(should be unique for each client) 
